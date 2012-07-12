using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simpl.Fundamental.Net;
using Simpl.OODSS.Messages;
using Simpl.Serialization.Attributes;
using ecologylab.collections;

namespace ecologylab.semantics.services.messages
{
    public class MetadataRequest : RequestMessage
    {
        [SimplScalar] private String url;

        public MetadataRequest()
        {
            
        }

        public MetadataRequest(String url)
        {
            this.url = url;
        }
        
        public override ResponseMessage PerformService(Scope<object> clientSessionScope)
        {
            throw new NotImplementedException();
        }
    }
}
