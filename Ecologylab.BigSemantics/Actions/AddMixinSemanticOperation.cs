using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ecologylab.BigSemantics.MetadataNS;
using Simpl.Serialization.Attributes;
using Ecologylab.Collections;
using Ecologylab.BigSemantics.Actions;

namespace Ecologylab.BigSemantics.Actions
{
    [SimplInherit]
    [SimplTag("add_mixin")]
    class AddMixinSemanticOperation : SemanticOperation
    {
	    [SimplScalar]
	    private String	mixin;

	    public override String GetOperationName()
	    {
		    return SemanticOperationStandardMethods.AddMixin;
	    }

	    public override void HandleError()
	    {
	    }

	    public override Object Perform(Object obj)
	    {
            Metadata target = (Metadata) obj;
		    Scope<Object> vars = semanticOperationHandler.SemanticOperationVariableMap;
            Metadata mixinMetadata = (Metadata)vars.Get(mixin);
		    target.AddMixin(mixinMetadata);
		    return null;
	    }
    }
}
