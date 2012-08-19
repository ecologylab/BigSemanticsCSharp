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
    /// <summary>
    /// The metadata that is serialized and sent to the semantic service
    /// </summary>
    public class MetadataRequest : RequestMessage
    {
        /// <summary>
        /// The url for which is wanted the metadata extracted by the semantic service and sent back to the client
        /// </summary>
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
