using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simpl.Serialization.Attributes;
using ecologylab.semantics.metadata.builtins.declarations;
using ecologylab.semantics.metametadata;

namespace ecologylab.semantics.metadata.builtins
{
    [SimplInherit]
    public class CompoundDocument : CompoundDocumentDeclaration
    {

        public CompoundDocument() : base() { }

        public CompoundDocument(MetaMetadataCompositeField metaMetadata) : base(metaMetadata) { }

    }
}
