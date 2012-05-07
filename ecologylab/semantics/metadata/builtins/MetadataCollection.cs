using System.Collections.Generic;
using Simpl.Serialization.Attributes;
using ecologylab.semantics.collecting;
using ecologylab.semantics.metadata;
using ecologylab.semantics.namesandnums;

namespace ecologylab.semantics.metadata.builtins
{
    [SimplInherit]
    public class MetadataCollection : Metadata
    {
        [SimplCollection(SemanticNames.REPOSITORY_METADATA_TRANSLATIONS)] 
        private List<Metadata> collection;

        public MetadataCollection(List<Metadata> metadatas) : base()
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
