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
    /// <summary>
    /// The metadata response that is received from the semantic service after a request message from the client.
    /// </summary>
    public class MetadataResponse : ResponseMessage
    {
        /// <summary>
        /// The metadata received from the semantic service for the requested uri
        /// </summary>
        [SimplComposite]
        [SimplScope(SemanticNames.RepositoryMetadataTranslations/*"meta-metadata-compiler-tscope"*/)]
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
