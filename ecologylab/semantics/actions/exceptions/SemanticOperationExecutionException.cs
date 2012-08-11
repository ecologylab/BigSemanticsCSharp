using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ecologylab.collections;
using ecologylab.semantics.actions;

namespace ecologylab.semantics.actions.exceptions
{
    public class SemanticOperationExecutionException : SystemException
    {
        protected static String	ErrorString	= "###########################POSSIBLE CAUSES OF ERROR##############################";

	    public SemanticOperationExecutionException(SemanticOperation action)
	    {
		    Console.WriteLine("\n########################### ERROR " + action.GetOperationName()
				    + " FAILED ###########################");
		    //Console.WriteLine(ERROR_STRING);
	    }

	    public SemanticOperationExecutionException(SemanticOperation operation, String message)
            : this(operation)
	    {
		    Console.WriteLine(message);
		    SemanticOperationHandler semanticOperationHandler = operation.SemanticOperationHandler;
            if (semanticOperationHandler != null)
		    {
                StackTrace(semanticOperationHandler.SemanticOperationVariableMap);
		    }
	    }

	    public SemanticOperationExecutionException(Exception e, SemanticOperation operation,
			    Scope<Object> semanticActionReturnValueMap)
	        : this(operation)
        {
		    StringBuilder buffy = new StringBuilder(); //edit StringBuilderUtils.acquire();
		    buffy.Append("Action Object:: ").Append(operation.ObjectStr)
				    .Append("  :: is NULL or DOES NOT EXIST\n");
		    buffy.Append("Action ReturnValue:: ").Append(operation.Name)
				    .Append(" ::  is NULL or DOES NOT EXIST FOR SPECIFIED OBJECT");
		
		    String errorMessage = buffy.ToString();
		    //StringBuilderUtils.release(buffy);
		    Console.WriteLine(errorMessage);
		    StackTrace(semanticActionReturnValueMap);
		    //System.out.println("######## POSSIBLE CAUSE:");
		    //e.printStackTrace();
		    Console.WriteLine("############################################################################################");
	    }

	    public void StackTrace(Scope<Object> map)
	    {
		    StringBuilder sb = new StringBuilder();
		    sb.Append("--------------Meta-Metadata Trace--------------\n");
		    //edit map.dumpThis(sb, "");
		    Console.WriteLine(sb);
            Console.WriteLine("----------------------------------------");
	    }

    }
}
