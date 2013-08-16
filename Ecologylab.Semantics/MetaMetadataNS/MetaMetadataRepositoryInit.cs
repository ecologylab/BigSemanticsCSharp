using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Ecologylab.Semantics.MetadataNS;
using Ecologylab.Semantics.MetadataNS.Builtins;
using Ecologylab.Semantics.Namesandnums;
using Ecologylab.Collections;
using Simpl.Fundamental.Collections;
using Simpl.Fundamental.Net;
using Simpl.Fundamental.PlatformSpecifics;
using Simpl.Serialization;

namespace Ecologylab.Semantics.MetaMetadataNS
{
    public class MetaMetadataRepositoryInit : Scope<Object>
    {

        public static String DefaultRepositoryLocation                        = @"..\..\..\..\..\MetaMetadataRepository\MmdRepository\mmdrepository";

        public static Format DefaultRepositoryFormat                          = Format.Xml;

        public static MetaMetadataRepositoryLoader DefaultRepositoryLoader    = new MetaMetadataRepositoryLoader();

        public static readonly String Semantics                                 = "semantics/";

        protected object MetametadataRepositoryDirFile;

        protected object MetametadataSitesFile;

        public event EventHandler<EventArgs> RepositoryLoaded;

        /**
         * 
         * The repository has the metaMetadatas of the document types. The repository is populated as the
         * documents are processed.
         */
        protected static MetaMetadataRepository META_METADATA_REPOSITORY;

        public MetaMetadata DocumentMetaMetadata;
        public MetaMetadata PdfMetaMetadata;
        public MetaMetadata SearchMetaMetadata;
        public MetaMetadata ImageMetaMetadata;
        public MetaMetadata DebugMetaMetadata;
        public MetaMetadata ImageClippingMetaMetadata;

        static MetaMetadataRepositoryInit()
        {
            SimplTypesScope.graphSwitch = SimplTypesScope.GRAPH_SWITCH.ON;
            MetaMetadataRepository.InitializeTypes();
        }

        public static MetaMetadataRepository getRepository()
        {
            return META_METADATA_REPOSITORY;
        }

        private MetaMetadataRepository _metaMetadataRepository;

        private readonly SimplTypesScope _metadataTranslationScope;

        private readonly SimplTypesScope _generatedDocumentTranslations;

        private readonly SimplTypesScope _generatedMediaTranslations;

        private readonly SimplTypesScope _repositoryClippingTranslations;

        private readonly SimplTypesScope _noAnnotationsScope;

        /**
         * This constructor should only be called from SemanticsScope's constructor!
         * 
         * @param _metadataTranslationScope
         */
        public MetaMetadataRepositoryInit(SimplTypesScope metadataTranslationScope, string repoLocation, EventHandler<EventArgs> onCompleted)
        {
            //		    if (SingletonApplicationEnvironment.isInUse() && !SingletonApplicationEnvironment.runningInEclipse())
            //		    {
            //			    AssetsRoot mmAssetsRoot = new AssetsRoot(
            //					    EnvironmentGeneric.configDir().getRelative(SEMANTICS), 
            //					    Files.newFile(PropertiesAndDirectories.thisApplicationDir(), SEMANTICS + "/repository")
            //					    );
            //	
            //			    METAMETADATA_REPOSITORY_DIR_FILE 	= Assets.getAsset(mmAssetsRoot, null, "repository", null, !USE_ASSETS_CACHE, SemanticsAssetVersions.METAMETADATA_ASSET_VERSION);
            //		    }
            //		    else

            MetametadataRepositoryDirFile = repoLocation;

            this._metadataTranslationScope = metadataTranslationScope; 
            Debug.WriteLine("\t\t-- Reading meta_metadata from " + MetametadataRepositoryDirFile);

            if (onCompleted != null)
                this.RepositoryLoaded += onCompleted;

            
            _generatedDocumentTranslations = metadataTranslationScope.GetAssignableSubset(
                                                SemanticNames.RepositoryDocumentTranslations,
                                                typeof(Document));
            _generatedMediaTranslations = metadataTranslationScope.GetAssignableSubset(
                                                SemanticNames.RepositoryMediaTranslations,
                                                typeof(ClippableDocument));
            _repositoryClippingTranslations = metadataTranslationScope.GetAssignableSubset(
                                                SemanticNames.RepositoryClippingTranslations,
                                                typeof(Clipping<>));

            _noAnnotationsScope = metadataTranslationScope.GetSubtractedSubset(
                                                SemanticNames.RepositoryNoAnnotationsTypeScope,
                                                typeof(TextSelfmade));

            

            _generatedMediaTranslations.AddTranslation(typeof(Clipping<>));
            _generatedMediaTranslations.AddTranslation(typeof(TextSelfmade));
               
        }

        public async void LoadRepositoryAsync()
        {
            META_METADATA_REPOSITORY = new MetaMetadataRepository
            {
                RepositoryByName = new Dictionary<string, MetaMetadata>(),
                PackageMmdScopes = new Dictionary<string, MmdScope>()
            };

            _metaMetadataRepository = META_METADATA_REPOSITORY;

            SetDefaultMetaMetadatas();

            MetaMetadataRepository mainRepo = await MetaMetadataRepositoryLoader.ReadDirectoryRecursively(
                META_METADATA_REPOSITORY,
                MetametadataRepositoryDirFile as string,
                MetaMetadataTranslationScope.Get(),
                _metadataTranslationScope
                );

            

            BindAndCallback(mainRepo);
        }


        private void SetDefaultMetaMetadatas()
        {
            DocumentMetaMetadata = META_METADATA_REPOSITORY.GetMMByName(DocumentParserTagNames.DocumentTag);
            PdfMetaMetadata = META_METADATA_REPOSITORY.GetMMByName(DocumentParserTagNames.PdfTag);
            SearchMetaMetadata = META_METADATA_REPOSITORY.GetMMByName(DocumentParserTagNames.SearchTag);
            ImageMetaMetadata = META_METADATA_REPOSITORY.GetMMByName(DocumentParserTagNames.ImageTag);
            DebugMetaMetadata = META_METADATA_REPOSITORY.GetMMByName(DocumentParserTagNames.DebugTag);
            ImageClippingMetaMetadata = META_METADATA_REPOSITORY.GetMMByName(DocumentParserTagNames.ImageClippingTag);
        }

        private void BindAndCallback(MetaMetadataRepository repository)
        {
            repository.BindMetadataClassDescriptorsToMetaMetadata(_metadataTranslationScope);
            if (RepositoryLoaded != null)
                RepositoryLoaded(repository, new EventArgs());
        }

        public static async Task<MetaMetadataRepository> RequestMetaMetadataRepository(ParsedUri requestUri)
        {
            return await MetaMetadataTranslationScope.Get().DeserializeUri(requestUri) as MetaMetadataRepository;
        }

        public async Task<MetaMetadataRepository> LoadRepositoryFromCache(object file)
        {
            _metaMetadataRepository =
                await MetaMetadataTranslationScope.Get().Deserialize(file, Format.Xml) as MetaMetadataRepository;

            InitializeRepositoryAndPerformBinding();

            return _metaMetadataRepository;
        }

        public async Task<MetaMetadataRepository> LoadRepositoryFromServiceAsync(ParsedUri serviceUri, object cacheFile = null)
        {
            _metaMetadataRepository     = await RequestMetaMetadataRepository(serviceUri);

            if (cacheFile != null)
                SimplTypesScope.Serialize(_metaMetadataRepository, cacheFile, Format.Xml);

            InitializeRepositoryAndPerformBinding();

            return _metaMetadataRepository;
        }

        private void InitializeRepositoryAndPerformBinding()
        {
            META_METADATA_REPOSITORY = _metaMetadataRepository;

            SetDefaultMetaMetadatas();

            BindAndCallback(META_METADATA_REPOSITORY);
        }

        #region Properties

        public SimplTypesScope MetadataTranslationScope
        {
            get { return _metadataTranslationScope; }
        }

        public MetaMetadataRepository MetaMetadataRepository
        {
            get { return _metaMetadataRepository; }
        }

        #endregion
    }
}
