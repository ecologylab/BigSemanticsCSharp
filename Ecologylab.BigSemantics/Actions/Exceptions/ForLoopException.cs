using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Ecologylab.Collections;

namespace Ecologylab.BigSemantics.Actions.Exceptions
{
    public class ForLoopException : SemanticOperationExecutionException
    {


	    public ForLoopException(Exception e, ForEachSemanticOperation operation,
			    Scope<Object> semanticActionReturnValueMap)
	        : base(e,operation,semanticActionReturnValueMap)
        {
		    if(e is IndexOutOfRangeException)//edit
            {
			    Debug.WriteLine("Invalid bounds for FOR LOOP:: start ="+operation.Start+"\t end = "+operation.End);
		    }
		    else
		    {	
			    Debug.WriteLine(((ForEachSemanticOperation)operation).Collection+" :: is NULL or does not exists");
		    }
			    StackTrace(semanticActionReturnValueMap);
	    }


}
}
