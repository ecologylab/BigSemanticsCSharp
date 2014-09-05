using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simpl.Serialization.Attributes;
using Ecologylab.BigSemantics.Actions;

namespace Ecologylab.BigSemantics.Actions
{
    ///<summary>
    ///Operation specifies removing the param with this name from the location ParsedURL.
    /// 
    /// @author andruid
    ///</summary>
    [SimplInherit]
    public class StripParam : ParamOp
    {

	    public StripParam()
	    {
		}

	    ///<summary>
	    /// Remove the parameter of the name from this from the parametersMap.
        ///</summary>
	    public override void TransformParams(Dictionary<String, String> parametersMap)
	    {
		    String name	= this.Name;
		
		    if (name != null && name.Length > 0)
		    {
			    parametersMap.Remove(name);
		    }
	    }

    }
}
