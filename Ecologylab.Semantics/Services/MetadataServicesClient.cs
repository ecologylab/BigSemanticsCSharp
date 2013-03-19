using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
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
        private readonly ParsedUri _serviceBaseUri;
        private readonly SimplTypesScope _metadataTypeScope;

        public MetadataServicesClient(SimplTypesScope metadatascope, SemanticsSessionScope semanticSessionScope, ParsedUri serviceUri, bool useWebSockets = false)
        {
            SimplTypesScope[] oodssAndMetadataScope = {metadatascope, DefaultServicesTranslations.Get()};

            _metadataTypeScope = SimplTypesScope.Get("MetadataServicesTranslationScope",
                                                        oodssAndMetadataScope,
                                                        typeof (MetadataRequest),
                                                        typeof (MetadataResponse),
                                                        typeof (SemanticServiceError)
                                                    );

            _serviceBaseUri = serviceUri;

            if (useWebSockets)
            {
                _metadataClient = new WebSocketOODSSClient("127.0.0.1", 2018, _metadataTypeScope, semanticSessionScope);
                _metadataClient.StartAsync();
            }
            
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

            if (_metadataClient != null)
            {
                Debug.WriteLine("Performing asynchronous call");
                ResponseMessage metadataResponse =
                    _metadataClient.RequestAsync(new MetadataRequest(uri.ToString())).Result;
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
            }
            else
            {
                var requestUri = new ParsedUri(_serviceBaseUri, "?url=" + uri.AbsoluteUri);
                result = await _metadataTypeScope.DeserializeUri(requestUri, Format.Xml) as Document;
            }

            return result;
        }
    }
}
