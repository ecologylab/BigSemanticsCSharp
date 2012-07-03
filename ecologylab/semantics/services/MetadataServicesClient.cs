using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simpl.OODSS.Distributed.Client;
using Simpl.Serialization;
using ecologylab.collections;
using ecologylab.semantics.services.messages;

namespace ecologylab.semantics.services
{
    public class MetadataServicesClient
    {
        private readonly OODSSClient _metadataClient;

        public MetadataServicesClient()
        {
            SimplTypesScope typesScope = SimplTypesScope.Get("MetadataServicesTranslationScope", 
                                                        typeof (MetadataRequest),
                                                        typeof (MetadataResponse));
            Scope<object> objectScope = new Scope<object>();

            _metadataClient = new OODSSClient("127.0.0.1", 2107, typesScope, objectScope);
            _metadataClient.Start();
        }
    }
}
