using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simpl.Serialization.Attributes;
using Ecologylab.BigSemantics.Actions;
//using Ecologylab.Semantics.Documentparsers;
using Ecologylab.BigSemantics.MetadataNS.Builtins;
using Ecologylab.BigSemantics.MetaMetadataNS;

namespace Ecologylab.BigSemantics.Actions
{
    [SimplInherit]
    [SimplTag("reselect_meta_metadata_and_extract")]
    public class ReselectAndExtractMetadataSemanticOperation : SemanticOperation
    {

	    public override String GetOperationName()
	    {
		    return SemanticOperationStandardMethods.ReselectMetametadataAndExtract;
	    }

	    public override void HandleError()
	    {
	    }

	    public override Object Perform(Object obj)// throws IOException
	    {
		    RichDocument doc = (RichDocument) obj;
		    MetaMetadata mmd = (MetaMetadata) doc.MetaMetadata;
/*		    Dictionary<MetaMetadataSelector, MetaMetadata> reselectMap = mmd.ReselectMap;
		    if (reselectMap != null)
		    {
			    foreach (MetaMetadataSelector selector in reselectMap.Keys)
			    {
				    if (selector.reselect(doc))
				    {
					    MetaMetadata newMmd = reselectMap[selector];
					    DocumentParser newParser = DocumentParser.get(newMmd, sessionScope);
					    newParser.fillValues(documentParser.purlConnection(), doc.getOrConstructClosure(), sessionScope);
					    if (documentParser instanceof ParserBase && newParser instanceof ParserBase)
					    {
						    CompoundDocument newDoc = (CompoundDocument) newMmd.constructMetadata();
						    newDoc.setLocation(doc.getLocation());
						    ParserBase newParserBase = (ParserBase) newParser;
						    org.w3c.dom.Document dom = ((ParserBase) documentParser).getDom();
						    newParserBase.parse(newDoc, newMmd, dom);

						    DocumentClosure closure = doc.getOrConstructClosure();
						    closure.changeDocument(newDoc);

						    return newDoc;
					    }
				    }
			    }
		    }*/
		    return null;
	    }

    }
    
}
