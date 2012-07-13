using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simpl.Fundamental.Net;
using Simpl.OODSS.Distributed.Client;
using Simpl.OODSS.Messages;
using Simpl.Serialization;
using ecologylab.collections;
using ecologylab.semantics.metadata.builtins;
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
            //_metadataClient.AddRequest();
            //_metadataClient.AddRequest(new MetadataRequest("http://www.airbnb.com/rooms/36769"));
            
            _metadataClient.Start();
        }

        /// <summary>
        /// 
        /// Encapsulate the oodss client requst in a simpler GetMetadata call.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<Document> GetMetadata(string url)
        {
            Console.WriteLine("Performing asynchronous call");
            ResponseMessage metadataResponse = await _metadataClient.RequestAsync(new MetadataRequest(url));
            Console.WriteLine("Received asynchronous request ");

            Document result = null;
            if(metadataResponse != null && metadataResponse is MetadataResponse)
            {
                result = (metadataResponse as MetadataResponse).Metadata;
            }
            return result;
        }
    }
}
