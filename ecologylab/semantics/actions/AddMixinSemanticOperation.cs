using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simpl.Serialization.Attributes;
using ecologylab.collections;
using ecologylab.semantics.actions;
using ecologylab.semantics.metadata;

namespace ecologylabSemantics.ecologylab.semantics.actions
{
    [SimplInherit]
    [SimplTag("add_mixin")]
    class AddMixinSemanticOperation : SemanticOperation
    {
        [SimplScalar]
        private String	mixin;

        public override string GetOperationName()
        {
            return SemanticOperationStandardMethods.AddMixin;
        }

        public override void HandleError()
        {
        }

        public override object Perform(object obj)
        {
            Metadata target = (Metadata) obj;
            Scope<object> vars = semanticOperationHandler.SemanticOperationVariableMap;
            Metadata mixinMetadata = (Metadata) vars.Get(mixin);
            target.AddMixin(mixinMetadata);
            return null;
        }
    }
}
