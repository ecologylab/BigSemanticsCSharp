using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simpl.Serialization.Attributes;

namespace Ecologylab.Semantics.Actions
{
    ///<summary>
    /// Operation specifies adding param with this name and value to the query string for the location ParsedURL.
    /// 
    /// @author andruid
    ///
    [SimplInherit]
    public class SetParam : ParamOp
    {
	
	    [SimplScalar]
	    private String			value;
	
	    [SimplScalar]
	    private String			valueFrom;
	
	    [SimplScalar]
	    private String			valueFromCollection;
	
	    [SimplScalar]
	    private String			collectionIndex;
	
	    public SetParam()
	    {
	    }


	    public override void TransformParams(Dictionary<String, String> parametersMap)
	    {
		    if (value != null)
			    parametersMap.Add(Name, value);
		    else if (valueFrom != null && handler != null)
		    {
			    parametersMap.Add(Name, handler.SemanticOperationVariableMap.Get(valueFrom).ToString());
		    }
		    else if (valueFromCollection != null && collectionIndex != null && handler != null)
		    {
			    Object idx = handler.SemanticOperationVariableMap.Get(collectionIndex);
			    if (idx is int)
			    {
				    int i = (int) idx;
				    IList theCollection = (IList) handler.SemanticOperationVariableMap.Get(valueFromCollection);
                    if (i >= 0 && i < theCollection.Count)
					    parametersMap.Add(Name, theCollection[i].ToString());
			    }
		    }
	    }

    }
}