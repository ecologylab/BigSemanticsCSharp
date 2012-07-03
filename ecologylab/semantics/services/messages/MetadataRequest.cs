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
        [SimplScalar] private ParsedUri url;

        public MetadataRequest()
        {
            
        }

        public override ResponseMessage PerformService(Scope<object> clientSessionScope)
        {
            throw new NotImplementedException();
        }
    }
}
