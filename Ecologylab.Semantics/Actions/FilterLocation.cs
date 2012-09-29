using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Simpl.Fundamental.Net;
using Simpl.Serialization.Attributes;
using Ecologylab.Semantics.Actions;
using Ecologylab.Semantics.Collecting;
using Ecologylab.Semantics.MetadataNS.Builtins;
using Ecologylab.Semantics.MetadataNS.Scalar;

namespace Ecologylab.Semantics.Actions
{
    /**
     * transform_location semantic action, with set_param, strip_param semantic actions inside it, 
     * for managing variability in Document location ParsedURL arguments.
     * 
     * Operations include set_param & strip_param.
     * @author andruid
     *
     */
    [SimplInherit]
    [SimplTag("filter_location")]
    public class FilterLocation : SemanticOperation
    {
	    [SimplClasses( new[] {typeof(SetParam), typeof(StripParam)})]
	    [SimplNoWrap]
	    [SimplCollection]
	    List<ParamOp> paramOps;

	    [SimplNoWrap]
	    [SimplCollection("alternative_host")]
	    List<String> alternativeHosts;

	    [SimplComposite]
	    Regex regex;
	
	    [SimplScalar]
	    String stripPrefix;

	    public override String GetOperationName()
	    {
		    return SemanticOperationStandardMethods.FilterLocation;
	    }

	    public override void HandleError()
	    {
	    }

        ///<summary>
        /// Extract location ParsedURL parameters into a HashMap.
        /// Let ParamOps operate on this Map.
        /// Derive a new ParsedURL using the base of the original location's ParsedURL and the transformed parameter map.
        /// If the new ParsedURL is different than the old one, make the old one an additional location for this,
        /// and add the transformed ParsedURL to the DocumentLocationMap.
        /// <p/>
        /// If the location changes, then reconnect to the new one.
        ///</summary>
	    public override Object Perform(Object obj)// throws IOException
	    {
		    bool usingThisDoc = true; // if we are changing this document's location, or its child field's location
		    Document document = (Document) SemanticOperationHandler.SemanticOperationVariableMap.Get(SemanticOperationKeyWords.Metadata);
		    if (ObjectStr != null)
		    {
			    Object o = SemanticOperationHandler.SemanticOperationVariableMap.Get(ObjectStr);
			    if (o != null && o is Document)
			    {
				    document = (Document) o;
				    usingThisDoc = false;
			    }
		    }
		
		    ParsedUri origLocation = document.Location.value;
		    if (origLocation.IsFile)
		    {
			    Debug.WriteLine("Not doing <filter_location> because this is a file: " + origLocation);
			    return null;
		    }
		
		    SemanticsGlobalCollection<Document> globalCollection = SemanticOperationHandler.SemanticsScope.GlobalDocumentCollection;
		    bool locationChanged = false;
		    if (paramOps != null && paramOps.Count > 0)
		    {
			    Dictionary<String, String> parametersMap = new Dictionary<string, string>();

		        foreach (String param in origLocation.Query.Split('&'))
		        {
		            parametersMap.Add(param.Split('=')[0], param.Split('=')[1]);
		        }

			    if (parametersMap.Count == 0)
				    parametersMap = new Dictionary<string, string>(paramOps.Count);
			    foreach (ParamOp paramOp in paramOps)
			    {
				    paramOp.SemanticHandler = SemanticOperationHandler;
				    paramOp.TransformParams(parametersMap);
			    }

		        String str = origLocation.AbsolutePath + "?";
                foreach (KeyValuePair<string, string> pair in parametersMap)
                {
                    str += pair.Key + "=" + pair.Value + "&";
                }
                if (str[str.Length - 1] == '&')
                    str = str.Substring(0, str.Length - 1);

			    ParsedUri transformedLocation = new ParsedUri(str);
			    
                if (origLocation != transformedLocation)
			    {
				    document.Location.Value = transformedLocation;
				
				    locationChanged			= true;
			    }
		    }
		    if (alternativeHosts != null)
		    {
			    String origHost = origLocation.Host;
			    foreach (String alternativeHost in alternativeHosts)
			    {
				    if (!origHost.Equals(alternativeHost))
				    {
				        ParsedUri newLocation = new ParsedUri(origLocation.AbsoluteUri.Replace(origLocation.Host, alternativeHost));
					    document.AddAdditionalLocation(new MetadataParsedURL(newLocation));
					    globalCollection.AddDocument(document, newLocation);
				    }
			    }
		    }
		    if (regex != null)
		    {
			    ParsedUri location = document.Location.Value;
			    ParsedUri regexURL = regex.Perform(location);
			    
                document.ChangeLocation(regexURL);
			    
                
                locationChanged	= true;
		    }
		    if (stripPrefix != null)
		    {
			    String origlLocationString	= origLocation.ToString();
			    int index	= origlLocationString.IndexOf(stripPrefix);
			    if (index > 6)
			    {
				    String newLocationString = origlLocationString.Substring(0, index);
				    ParsedUri newLocation = new ParsedUri(newLocationString);
				    if (newLocation != null)
				    {
					    document.ChangeLocation(newLocation);
				    }
			    }
		    }
            if (locationChanged && usingThisDoc) // if we are just changing the location of a field, we don't have to reconnect.
            {
                if (documentParser != null)
                {
                    //documentParser.reConnect(); // changed the location, so we better connect again!
                }
                else
                {
                    //imageClosure.RequestMetadata();
                }
            }
            return null;
	    }
    }
}
