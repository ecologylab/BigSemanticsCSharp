using System.Collections.Generic;
using Ecologylab.Semantics.Collecting;
using Ecologylab.Semantics.Namesandnums;
using Simpl.Serialization.Attributes;
using Ecologylab.Semantics.MetadataNS;

namespace Ecologylab.Semantics.MetadataNS.Builtins
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
