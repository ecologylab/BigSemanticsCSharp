using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simpl.Serialization.Attributes;
using ecologylab.semantics.metadata;
using ecologylab.semantics.metametadata;

namespace ecologylab.semantics.metadata.builtins
{
    [SimplDescriptorClasses(new Type[] { typeof(MetadataClassDescriptor), typeof(MetadataFieldDescriptor) })]
    [SimplInherit]
    public class CompoundDocument : Document
    {
        // dummy class

        public CompoundDocument() : base() { }

        public CompoundDocument(MetaMetadataCompositeField metaMetadata) : base(metaMetadata) { }
    }
}
