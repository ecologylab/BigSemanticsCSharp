using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ecologylab.collections;

namespace ecologylab.semantics.actions.exceptions
{
    public class ForLoopException : SemanticOperationExecutionException
    {


	    public ForLoopException(Exception e, ForEachSemanticOperation operation,
			    Scope<Object> semanticActionReturnValueMap)
	        : base(e,operation,semanticActionReturnValueMap)
        {
		    if(e is IndexOutOfRangeException)//edit
            {
			    Console.WriteLine("Invalid bounds for FOR LOOP:: start ="+operation.Start+"\t end = "+operation.End);
		    }
		    else
		    {	
			    Console.WriteLine(((ForEachSemanticOperation)operation).Collection+" :: is NULL or does not exists");
		    }
			    StackTrace(semanticActionReturnValueMap);
	    }


}
}
