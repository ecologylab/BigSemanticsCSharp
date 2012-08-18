using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simpl.Serialization;
using Simpl.Serialization.Attributes;
using ecologylab.semantics.actions;

namespace ecologylabSemantics.ecologylab.semantics.actions
{
    public abstract class ParamOp : ElementState
    {
        [SimplScalar]
        private String name;

        public String Name
        {
            get { return name; }
            set { name = value; }
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
