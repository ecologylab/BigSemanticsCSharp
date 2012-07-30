using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Simpl.Serialization;
using ecologylab.collections;
using ecologylab.semantics.metadata.builtins;
using ecologylab.semantics.namesandnums;

namespace ecologylab.semantics.metametadata
{
    public class MetaMetadataRepositoryInit : Scope<Object>
    {

        public static String DEFAULT_REPOSITORY_LOCATION                        = @"..\..\..\..\..\MetaMetadataRepository\MmdRepository\mmdrepository";

        public static Format DEFAULT_REPOSITORY_FORMAT                          = Format.Xml;

        public static MetaMetadataRepositoryLoader DEFAULT_REPOSITORY_LOADER    = new MetaMetadataRepositoryLoader();

        public static readonly String SEMANTICS                                 = "semantics/";

        protected FileInfo METAMETADATA_REPOSITORY_DIR_FILE;

        protected FileInfo METAMETADATA_SITES_FILE;

        /**
         * 
         * The repository has the metaMetadatas of the document types. The repository is populated as the
         * documents are processed.
         */
        protected static MetaMetadataRepository META_METADATA_REPOSITORY;

        public MetaMetadata DOCUMENT_META_METADATA;
        public MetaMetadata PDF_META_METADATA;
        public MetaMetadata SEARCH_META_METADATA;
        public MetaMetadata IMAGE_META_METADATA;
        public MetaMetadata DEBUG_META_METADATA;
        public MetaMetadata IMAGE_CLIPPING_META_METADATA;

        static MetaMetadataRepositoryInit()
        {
            SimplTypesScope.graphSwitch = SimplTypesScope.GRAPH_SWITCH.ON;
            MetaMetadataRepository.InitializeTypes();
        }

        public static MetaMetadataRepository getRepository()
        {
            return META_METADATA_REPOSITORY;
        }

        private readonly MetaMetadataRepository _metaMetadataRepository;

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
        public MetaMetadataRepositoryInit(SimplTypesScope metadataTranslationScope, string repoLocation)
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
            {
                METAMETADATA_REPOSITORY_DIR_FILE = new FileInfo(repoLocation);
            }

            this._metadataTranslationScope = metadataTranslationScope;
            Debug.WriteLine("\t\t-- Reading meta_metadata from " + METAMETADATA_REPOSITORY_DIR_FILE);

            META_METADATA_REPOSITORY = MetaMetadataRepositoryLoader.ReadDirectoryRecursively(
                repoLocation,
                MetaMetadataTranslationScope.Get(),
                metadataTranslationScope
                );

            DOCUMENT_META_METADATA = META_METADATA_REPOSITORY.GetMMByName(DocumentParserTagNames.DocumentTag);
            PDF_META_METADATA = META_METADATA_REPOSITORY.GetMMByName(DocumentParserTagNames.PdfTag);
            SEARCH_META_METADATA = META_METADATA_REPOSITORY.GetMMByName(DocumentParserTagNames.SearchTag);
            IMAGE_META_METADATA = META_METADATA_REPOSITORY.GetMMByName(DocumentParserTagNames.ImageTag);
            DEBUG_META_METADATA = META_METADATA_REPOSITORY.GetMMByName(DocumentParserTagNames.DebugTag);
            IMAGE_CLIPPING_META_METADATA = META_METADATA_REPOSITORY.GetMMByName(DocumentParserTagNames.ImageClippingTag);

            _metaMetadataRepository          = META_METADATA_REPOSITORY;

            _generatedDocumentTranslations   = metadataTranslationScope.GetAssignableSubset(
                                                SemanticNames.RepositoryDocumentTranslations,
                                                typeof (Document));
            _generatedMediaTranslations      = metadataTranslationScope.GetAssignableSubset(
                                                SemanticNames.RepositoryMediaTranslations,
                                                typeof (ClippableDocument<>));
            _repositoryClippingTranslations  = metadataTranslationScope.GetAssignableSubset(
                                                SemanticNames.RepositoryClippingTranslations,
                                                typeof (Clipping));

            _noAnnotationsScope              = metadataTranslationScope.GetSubtractedSubset(
                                                SemanticNames.RepositoryNoAnnotationsTypeScope,
                                                typeof(Annotation));

            _generatedMediaTranslations.AddTranslation(typeof(Clipping));
            _generatedMediaTranslations.AddTranslation(typeof(Annotation));

            META_METADATA_REPOSITORY.BindMetadataClassDescriptorsToMetaMetadata(metadataTranslationScope);
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
