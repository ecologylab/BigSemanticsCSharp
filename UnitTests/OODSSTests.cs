using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ecologylab.semantics.generated.library;
using ecologylab.semantics.metadata.builtins;
using ecologylab.semantics.services;
using System.Net;
using System.Net.Sockets;

namespace UnitTests
{
    using Simpl.Fundamental.Net;
    using Simpl.OODSS.Messages;
    using Simpl.Serialization;

    using ecologylab.semantics.services.messages;

    /// <summary>
    /// Test the comunication with the Semantic Service using OODSS and the functionality of
    /// MetadataServicesClient class. 
    /// </summary>
    [TestClass]
    public class OODSSTests
    {
        [TestMethod]
        public void TestMetadataServicesComunication()
        {
            string Host = "127.0.0.1";
            int Port = 2107;
            Socket _clientSocket;

            byte[] _response = new byte[2048];

            IPHostEntry ipHostInfo = Dns.GetHostEntry(Host);//"achilles.cse.tamu.edu");
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, Port);
            _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _clientSocket.Connect(Host, Port);

            Console.WriteLine("Client socket with host ({0}) connected? {1}.", Host, _clientSocket.Connected);

            _clientSocket.Send(Encoding.ASCII.GetBytes("content-length:26\r\nuid:1\r\n\r\n<init_connection_request/>"));

            //_clientSocket.Receive(_response);
            //Console.WriteLine(_response.ToString());

            _clientSocket.Send(Encoding.ASCII.GetBytes("content-length:70\r\nuid:1\r\n\r\n<metadata_request url=\"http://www.amazon.com/gp/product/B0050SYS5A/\"/>"));

            //_clientSocket.Receive(_response);
            //Console.WriteLine(_response.ToString());
        }

        [TestMethod]
        public async void TestMetadataServicesClient()
        {
            Console.WriteLine("Initializing client");
            MetadataServicesClient mmdclient = new MetadataServicesClient(RepositoryMetadataTranslationScope.Get(), null);

            mmdclient.RequestMetadata(new ParsedUri("http://dl.acm.org/citation.cfm?id=1871437.1871580"));
            //Console.WriteLine("Got second metadata object: {0}", d );


            mmdclient.RequestMetadata(new ParsedUri("http://www.amazon.com/gp/product/B0050SYS5A/"));
            //Console.WriteLine("Got second metadata object: {0}", d2);

            Console.WriteLine("Terminating test cases");
        }

        [TestMethod]
        public void TestSemanticServiceError()
        {
            SimplTypesScope repositoryMetadataTranslationScope = RepositoryMetadataTranslationScope.Get();

            SimplTypesScope typesScope = SimplTypesScope.Get("MetadataServicesTranslationScope",
                                                        repositoryMetadataTranslationScope,
                                                        typeof(MetadataRequest),
                                                        typeof(MetadataResponse),
                                                        typeof(SemanticServiceError));
            String response = "<semantic_service_error code=\"2001\" error_message=\"Error\" />";

            ServiceMessage serviceMessage = (ServiceMessage) typesScope.Deserialize(response, StringFormat.Xml);

            try
            {
                (serviceMessage as SemanticServiceError).Perform();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.WriteLine(SimplTypesScope.Serialize(serviceMessage, StringFormat.Xml));
        }
    }
}
