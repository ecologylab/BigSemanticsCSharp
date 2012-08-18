using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simpl.Serialization.Attributes;
using ecologylab.semantics.actions;
using ecologylab.semantics.metadata;
using ecologylab.semantics.metametadata;

namespace ecologylabSemantics.ecologylab.semantics.actions
{

    [SimplInherit]
    [SimplTag("get_linked_metadata")]
    class GetLinkedMetadataSemanticOperation : SemanticOperation
    {

        public override string GetOperationName()
        {
            return SemanticOperationStandardMethods.GetLinkedMetadata;
        }

        public override void HandleError()
        {
        }

        public override object Perform(object obj)
        {
            return null;
        }

    }
}
