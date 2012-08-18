using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simpl.Serialization.Library.Configuration;
using ecologylab.semantics.actions;
using ecologylab.semantics.metadata;
using ecologylab.semantics.metadata.builtins;

namespace ecologylabSemantics.ecologylab.semantics.actions
{

    /// <summary>
    /// @author andruid, rhema
    /// This semantic action visualizes clippings of a compound document.
    /// For now, we visualize one clipping.
    /// </summary>
    public class VisualizeClippings : SemanticOperation
    {

        public VisualizeClippings()
        {

        }

        public override string GetOperationName()
        {
            return SemanticOperationStandardMethods.VisualizeClippings;
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
