using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simpl.Serialization.Attributes;
using ecologylab.semantics.generated.library;
using ecologylab.semantics.metadata;

namespace ecologylab.semantics.generated.library
{
    [SimplDescriptorClasses(new Type[] { typeof(MetadataClassDescriptor), typeof(MetadataFieldDescriptor) })]
    [SimplInherit]
    public class CompoundDocument : Document
    {
    // dummy class for compilation
    }
}
