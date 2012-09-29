using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecologylab.Semantics.Collecting;
using Ecologylab.Semantics.MetadataNS.Builtins;
using Ecologylab.Semantics.Services.Messages;
using Simpl.Fundamental.Net;
using Simpl.OODSS.Distributed.Client;
using Simpl.OODSS.Messages;
using Simpl.Serialization;
using Ecologylab.Collections;
using Ecologylab.Semantics.Services;

namespace Ecologylab.Semantics.Services
{
    public class MetadataServicesClient
    {
        private readonly WebSocketOODSSClient _metadataClient;

        public MetadataServicesClient(SimplTypesScope metadatascope, SemanticsSessionScope semanticSessionScope)
        {
            SimplTypesScope[] oodssAndMetadataScope = {metadatascope, DefaultServicesTranslations.Get()};

            SimplTypesScope typesScope = SimplTypesScope.Get("MetadataServicesTranslationScope",
                                                        oodssAndMetadataScope,
                                                        typeof (MetadataRequest),
                                                        typeof (MetadataResponse),
                                                        typeof (SemanticServiceError)
                                                        );

            _metadataClient = new WebSocketOODSSClient("127.0.0.1", 2018, typesScope, semanticSessionScope);            
            _metadataClient.StartAsync();
        }

        /// <summary>
        /// 
        /// Encapsulate the oodss client requst in a simpler GetMetadata call.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<Document> RequestMetadata(ParsedUri uri)
        {
            Document result = null;

            Debug.WriteLine("Performing asynchronous call");
            ResponseMessage metadataResponse = _metadataClient.RequestAsync(new MetadataRequest(uri.ToString())).Result;
            Debug.WriteLine("Received asynchronous request ");

            if (metadataResponse != null && metadataResponse is MetadataResponse)
            {
                result = (metadataResponse as MetadataResponse).Metadata;
            }
            else if (metadataResponse != null && metadataResponse is SemanticServiceError)
            {
                (metadataResponse as SemanticServiceError).Perform();
            }
            else 
                throw new Exception();

            return result;
        }
    }
}
