using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simpl.Fundamental.Net;
using Simpl.OODSS.Distributed.Client;
using Simpl.OODSS.Messages;
using Simpl.Serialization;
using ecologylab.collections;
using ecologylab.semantics.services.messages;

namespace ecologylab.semantics.services
{
    public class MetadataServicesClient
    {
        private readonly OODSSClient _metadataClient;

        private readonly Type[] TRANSLATIONS = new Type[] { typeof(RequestMessage), typeof(ResponseMessage),
			typeof(CloseMessage), typeof(OkResponse), typeof(BadSemanticContentResponse), typeof(ErrorResponse),
			/*Prologue.class, Epilogue.class, LogOps.class, SendEpilogue.class, SendPrologue.class,*/
			typeof(HttpRequest), typeof(HttpGetRequest), typeof(PingRequest), typeof(Ping), typeof(Pong), typeof(UrlMessage),
			typeof(CfCollaborationGetSurrogate), typeof(ContinuedHTTPGetRequest), typeof(IgnoreRequest),
			typeof(InitConnectionRequest), typeof(InitConnectionResponse), typeof(DisconnectRequest),
			typeof(ServiceMessage), typeof(UrlMessage), typeof(UpdateMessage) };

        public MetadataServicesClient()
        {
            SimplTypesScope typesScope = SimplTypesScope.Get("MetadataServicesTranslationScope", SimplTypesScope.Get("base_oodss", TRANSLATIONS),
                                                        typeof (MetadataRequest),
                                                        typeof (MetadataResponse));
            Scope<object> objectScope = new Scope<object>();

            _metadataClient = new OODSSClient("127.0.0.1", 2107, typesScope, objectScope);

            _metadataClient.AddRequest(new MetadataRequest("http://www.airbnb.com/rooms/36769"));
            _metadataClient.AddRequest(new MetadataRequest("http://www.airbnb.com/rooms/36769"));
            
            _metadataClient.Start();

            
        }



    }
}
