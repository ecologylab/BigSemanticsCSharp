using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Simpl.Fundamental.Net;
using Simpl.Serialization;
using Simpl.Serialization.Attributes;

namespace Ecologylab.Semantics.Actions
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
                    Debug.WriteLine(matcher.Groups[0].Value + " " + matcher.Groups[1].Value);
			        String rez = input.ToString().Replace(matcher.Groups[0].Value, replace);
                    for (int i = 1; i < matcher.Groups.Count; i++)
                        rez = rez.Replace("$" + i, matcher.Groups[i].Value);
				    
				    result = new ParsedUri(rez);
			    }
		    }
		    return result;
	    }
    }

}
