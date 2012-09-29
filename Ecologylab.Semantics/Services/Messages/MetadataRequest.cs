using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ecologylab.Collections;
using Simpl.Fundamental.Net;
using Simpl.OODSS.Messages;
using Simpl.Serialization.Attributes;

namespace Ecologylab.Semantics.Services.Messages
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
