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
using ecologylab.semantics.services;
using ecologylab.semantics.collecting;

namespace ecologylab.semantics.services
{
    /// <summary>
    /// This class encapsulates the comunication and the process to get the metadata from 
    /// a Semantic Service using a OODSSClient
    /// </summary>
    public class MetadataServicesClient
    {
        private readonly OODSSClient _metadataClient;

        public MetadataServicesClient(SimplTypesScope metadatascope, SemanticsSessionScope semanticSessionScope)
        {
            SimplTypesScope typesScope = SimplTypesScope.Get("MetadataServicesTranslationScope",
                                                        metadatascope,
                                                        typeof (MetadataRequest),
                                                        typeof (MetadataResponse),
                                                        typeof (SemanticServiceError));

            _metadataClient = new OODSSClient("127.0.0.1", 2107, typesScope, semanticSessionScope);            
            _metadataClient.Start();
        }

        /// <summary>
        /// Encapsulate the oodss client requst in a simpler RequestMetadata call.
        /// </summary>
        /// <param name="uri">the uri fot which to get the metadata</param>
        /// <returns>the metadata received from the Semantic Service </returns>
        public async Task<Document> RequestMetadata(ParsedUri uri)
        {
            Document result = null;

            Console.WriteLine("Performing asynchronous call");
            ResponseMessage metadataResponse = await _metadataClient.RequestAsync(new MetadataRequest(uri.ToString()));
            Console.WriteLine("Received asynchronous request ");

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
