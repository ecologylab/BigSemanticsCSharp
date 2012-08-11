using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Simpl.Fundamental.Net;
using Simpl.Serialization;
using Simpl.Serialization.Attributes;

namespace ecologylabSemantics.ecologylab.semantics.actions
{
    public class Regex : ElementState
    {
	    [SimplScalar]
        System.Text.RegularExpressions.Regex match;
	
	    [SimplScalar]
	    String replace;
	
	    public ParsedUri Perform(ParsedUri input)
	    {
		    ParsedUri result = null;
		    if (input != null && match != null)
		    {
			    String str	= input.ToString();
			    Match matcher	= match.Match(str);
			    if (matcher.Success)
			    {
				    if (replace == null)
					    replace	= "";
				    //String resultString = matcher..replaceAll(replace);
				    //result = ParsedUri.GetAbsolute(resultString);
			    }
		    }
		    return result;
	    }
    }

}
