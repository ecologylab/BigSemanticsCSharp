using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Ecologylab.Semantics.Actions;

namespace Ecologylab.Semantics.Actions
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
		    if(NullMethodError.Equals(errorCode))
		    {
			    Debug.WriteLine("");
		    }
		
	    }
	
    }
}
