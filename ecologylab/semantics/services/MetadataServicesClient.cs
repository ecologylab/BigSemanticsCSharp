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
    public class MetadataServicesClient
    {
        private readonly OODSSClient _metadataClient;

        private SemanticsGlobalCollection<Document> globalColection; 

        public event EventHandler<MetadataEventArgs> metadataDownloadComplete;

        public MetadataServicesClient(SimplTypesScope metadatascope)
        {
            SimplTypesScope typesScope = SimplTypesScope.Get("MetadataServicesTranslationScope",
                                                        metadatascope,
                                                        typeof (MetadataRequest),
                                                        typeof (MetadataResponse));
            Scope<object> objectScope = new Scope<object>();

            globalColection = new SemanticsGlobalCollection<Document>();

            _metadataClient = new OODSSClient("127.0.0.1", 2107, typesScope, objectScope);            
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
            ParsedUri puri = new ParsedUri(url);

            Document result = null;
            globalColection.TryGetDocument(puri, out result);

            if (result == null)
            {
                Console.WriteLine("Performing asynchronous call");
                ResponseMessage metadataResponse = await _metadataClient.RequestAsync(new MetadataRequest(url));
                Console.WriteLine("Received asynchronous request ");

                if (metadataResponse != null && metadataResponse is MetadataResponse)
                {
                    result = (metadataResponse as MetadataResponse).Metadata;

                    globalColection.AddDocument(result, puri);
                }
            }

            this.metadataDownloadComplete(this, new MetadataEventArgs(result));

            return result;
        }

        public class MetadataEventArgs : EventArgs
        {
            private Document metadata;

            public MetadataEventArgs(Document metadata)
            {
                this.metadata = metadata;
            }

            public Document Metadata
            {
                get { return metadata; }
                set { metadata = value; }
            }
        }
    }
}
