using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simpl.Serialization;
using Simpl.Serialization.Attributes;
using Ecologylab.Semantics.Actions;

namespace Ecologylab.Semantics.Actions
{
    public abstract class ParamOp : ElementState
    {
	    [SimplScalar]
	    private String name;
	
	    public String Name
	    {
            get { return name; }
	    }
	
	    protected SemanticOperationHandler handler;

        public SemanticOperationHandler SemanticHandler
	    {
            set { this.handler = value; }
	    }
	
	    public ParamOp()
	    {
	    }

	    public abstract void TransformParams(Dictionary<String, String> parametersMap);

    }
}
