using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Ecologylab.Semantics.MetadataNS.Builtins;
using Ecologylab.Semantics.Namesandnums;
using Ecologylab.Collections;
using Simpl.OODSS.Messages;
using Simpl.Serialization.Attributes;

namespace Ecologylab.Semantics.Services.Messages
{
    public class MetadataResponse : ResponseMessage
    {
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
            Debug.WriteLine("Process metadata");
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
