using System.Collections.Generic;
using Ecologylab.BigSemantics.Collecting;
using Ecologylab.BigSemantics.Namesandnums;
using Simpl.Serialization.Attributes;
using Ecologylab.BigSemantics.MetadataNS;

namespace Ecologylab.BigSemantics.MetadataNS.Builtins
{
    [SimplInherit]
    public class MetadataCollection : Metadata
    {
        [SimplCollection(SemanticNames.RepositoryMetadataTranslations)]
        private List<Metadata> collection;

        public MetadataCollection(List<Metadata> metadatas)
            : base()
        {
            this.collection = metadatas;
            this.MetaMetadata = SemanticsSessionScope.Get.MetaMetadataRepository.GetMMByName("metadata_collection");
        }

        public List<Metadata> CollectionElements
        {
            get { return collection; }
        }
    }
}
