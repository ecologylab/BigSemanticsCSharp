using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simpl.Serialization.Attributes;
using ecologylab.semantics.actions;
using ecologylab.semantics.metadata;
using ecologylab.semantics.metametadata;

namespace ecologylabSemantics.ecologylab.semantics.actions
{

    [SimplInherit]
    [SimplTag("get_linked_metadata")]
    class GetLinkedMetadataSemanticOperation : SemanticOperation
    {
	 
        public override String GetOperationName()
	    {
		    return SemanticOperationStandardMethods.GET_LINKED_METADATA;
	    }

	    public override void HandleError()
	    {
	    }

	    public override Object Perform(Object obj)
	    {
		    if (obj != null && obj is Metadata)
		    {
			    Metadata metadata = (Metadata) obj;
			    String name = GetReturnObjectName();
/*			    Metadata linkedMetadata = metadata.GetLinkedMetadata(name);
			    if (linkedMetadata == null)
			    {
				    if (metadata.MetaMetadata is MetaMetadata)
				    {
					    MetaMetadata thisMmd = (MetaMetadata) metadata.MetaMetadata;
					    LinkWith linkWith = thisMmd.LinkWiths.Get(name);
					    MetaMetadata linkedMmd = sessionScope.getMetaMetadataRepository().getMMByName(name);
					    if (linkedMmd != null)
					    {
						    String id = linkWith.getById();
						    ParsedURL purl = linkedMmd.generateUrl(id, metadata.getNaturalIdValue(id));
						    // the generated purl may not be associated with linkedMmd! e.g. linkedMmd is a
						    // citeseerx_summary, while generated purl is a citeseerx search.
						    Document linkedDocument	= (Document) sessionScope.getOrConstructDocument(purl);

						    linkedDocument.setSemanticsSessionScope(sessionScope);
						    if (linkedDocument != null)
						    {
							    linkedDocument.queueDownload();
							    metadata.pendingSemanticActionHandler = semanticActionHandler;
							    semanticActionHandler.requestWaiting = true;
						    }
					    }
				    }
			    }
			    return linkedMetadata;*/
		    }
		    return null;
	    }
	
    }
}
