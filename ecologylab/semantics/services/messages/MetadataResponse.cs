using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simpl.OODSS.Messages;
using Simpl.Serialization.Attributes;
using ecologylab.semantics.metadata.builtins;
using ecologylab.semantics.namesandnums;

namespace ecologylab.semantics.services.messages
{
    public class MetadataResponse : ResponseMessage
    {
        [SimplComposite] 
        [SimplScope(SemanticNames.REPOSITORY_METADATA_TRANSLATIONS)]
        private Document metadata;

        public MetadataResponse()
        {
            
        }

        public override bool IsOK()
        {
            return true;
        }

        public Document Metadata { get { return metadata; } }
    }
}
