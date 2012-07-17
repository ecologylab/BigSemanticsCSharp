using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simpl.OODSS.Messages;
using Simpl.Serialization.Attributes;
using ecologylab.semantics.metadata.builtins;
using ecologylab.semantics.namesandnums;
using ecologylab.collections;

namespace ecologylab.semantics.services.messages
{
    public class MetadataResponse : ResponseMessage
    {
        [SimplComposite]
        [SimplScope(/*SemanticNames.REPOSITORY_METADATA_TRANSLATIONS*/"meta-metadata-compiler-tscope")]
        private Document metadata;

        public MetadataResponse()
        {
            
        }

        public MetadataResponse(Document metadata)
	    {
		    this.metadata = metadata;
	    }
	
	    /*
	     * Called automatically by OODSS on client
         */
	    public override void ProcessResponse(Scope<object> appObjScope)
	    {
            Console.Out.WriteLine("Process metadata");
	    }

        public override bool IsOK()
        {
            return true;
        }

        public Document Metadata
        {
            get { return metadata; }
            set { metadata = value; }
        }
    }
}
