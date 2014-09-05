using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Ecologylab.Collections;
using Ecologylab.BigSemantics.Actions;

namespace Ecologylab.BigSemantics.Actions.Exceptions
{
    public class IfOperationException : SemanticOperationExecutionException
    {

	    public IfOperationException(Exception e, NestedSemanticOperation operation,
			    Scope<Object> semanticActionReturnValueMap)
	        : base(operation)
        {
		    
		    Debug.WriteLine(":::All the nested semantic actions might not execute properly:::");
		    List<SemanticOperation> nestedOperations= operation.NestedSemanticActionList;
		    if (nestedOperations != null && nestedOperations.Count > 0)
		    {
			    for(int i = 0; i < nestedOperations.Count; i++)
			    {
				    Debug.WriteLine("\t\t\t[" + nestedOperations[i].Name.ToUpper() + "] skipped");
			    }
		    }
		    StackTrace(semanticActionReturnValueMap);
	    }
    }
}
