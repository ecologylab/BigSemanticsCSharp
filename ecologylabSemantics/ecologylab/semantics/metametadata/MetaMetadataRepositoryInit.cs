using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Simpl.Serialization;
using ecologylab.semantics.metadata.builtins;
using ecologylab.semantics.namesandnums;

namespace ecologylab.semantics.metametadata
{
    public class MetaMetadataRepositoryInit : DocumentParserTagNames
    {

        public static String DEFAULT_REPOSITORY_LOCATION                        = @"..\..\..\..\MetaMetadataRepository\MmdRepository\mmdrepository";

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

        public static MetaMetadata DOCUMENT_META_METADATA;
        public static MetaMetadata PDF_META_METADATA;
        public static MetaMetadata SEARCH_META_METADATA;
        public static MetaMetadata IMAGE_META_METADATA;
        public static MetaMetadata DEBUG_META_METADATA;
        public static MetaMetadata IMAGE_CLIPPING_META_METADATA;

        static MetaMetadataRepositoryInit()
        {
            SimplTypesScope.graphSwitch = Simpl.Serialization.SimplTypesScope.GRAPH_SWITCH.ON;
            MetaMetadataRepository.InitializeTypes();
        }

        public static MetaMetadataRepository getRepository()
        {
            return META_METADATA_REPOSITORY;
        }

        private readonly MetaMetadataRepository metaMetadataRepository;

        private readonly SimplTypesScope metadataTranslationScope;

        private readonly SimplTypesScope generatedDocumentTranslations;

        private readonly SimplTypesScope generatedMediaTranslations;

        private readonly SimplTypesScope repositoryClippingTranslations;

        /**
         * This constructor should only be called from SemanticsScope's constructor!
         * 
         * @param metadataTranslationScope
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

            this.metadataTranslationScope = metadataTranslationScope;
            Debug.WriteLine("\t\t-- Reading meta_metadata from " + METAMETADATA_REPOSITORY_DIR_FILE);

            META_METADATA_REPOSITORY = MetaMetadataRepositoryLoader.ReadDirectoryRecursively(
                repoLocation,
                MetaMetadataTranslationScope.Get(),
                metadataTranslationScope
                );

            DOCUMENT_META_METADATA = META_METADATA_REPOSITORY.GetMMByName(DOCUMENT_TAG);
            PDF_META_METADATA = META_METADATA_REPOSITORY.GetMMByName(PDF_TAG);
            SEARCH_META_METADATA = META_METADATA_REPOSITORY.GetMMByName(SEARCH_TAG);
            IMAGE_META_METADATA = META_METADATA_REPOSITORY.GetMMByName(IMAGE_TAG);
            DEBUG_META_METADATA = META_METADATA_REPOSITORY.GetMMByName(DEBUG_TAG);
            IMAGE_CLIPPING_META_METADATA = META_METADATA_REPOSITORY.GetMMByName(IMAGE_CLIPPING_TAG);

            META_METADATA_REPOSITORY.BindMetadataClassDescriptorsToMetaMetadata(metadataTranslationScope);

            metaMetadataRepository = META_METADATA_REPOSITORY;

            generatedDocumentTranslations = metadataTranslationScope.GetAssignableSubset(
                SemanticNames.REPOSITORY_DOCUMENT_TRANSLATIONS,
                typeof (Document));
            generatedMediaTranslations = metadataTranslationScope.GetAssignableSubset(
                SemanticNames.REPOSITORY_MEDIA_TRANSLATIONS,
                typeof (ClippableDocument));
            repositoryClippingTranslations = metadataTranslationScope.GetAssignableSubset(
                SemanticNames.REPOSITORY_CLIPPING_TRANSLATIONS,
                typeof (Clipping));
        }

        #region Properties

        public SimplTypesScope MetadataTranslationScope
        {
            get { return metadataTranslationScope; }
        }

        public MetaMetadataRepository MetaMetadataRepository
        {
            get { return metaMetadataRepository; }
        }

        #endregion
    }
}
