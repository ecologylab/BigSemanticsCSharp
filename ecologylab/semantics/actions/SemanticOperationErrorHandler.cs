using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ecologylab.semantics.actions;

namespace ecologylabSemantics.ecologylab.semantics.actions
{
    public class SemanticOperationErrorHandler : SemanticOperationErrorCodes
    {

	    /**
	     * Handles the semantic action
	     * @param action
	     */
	    public void HandleError(SemanticOperation action)
	    {
		    action.HandleError();
	    }

	    public void HandleError(SemanticOperation action, String errorCode,
			    Type objectClass, String objectName)
	    {
		
		    // Print Error For NULL Method
		    if(NULL_METHOD_ERROR.Equals(errorCode))
		    {
			    Console.WriteLine("");
		    }
		
	    }
	
    }
}
