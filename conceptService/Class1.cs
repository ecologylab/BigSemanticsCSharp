using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net.Sockets;
using System.Threading;

using System;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using wikxplorer.messages;
using ecologylab.serialization;
using ecologylab.oodss.messages;

namespace conceptService
{
    class Class1
    {
        static int n = 1;
        private static System.Text.StringBuilder output;
        private static Socket ConnectSocket(string server, int port)
        {
            Socket s = null;
            IPHostEntry hostEntry = null;

            // Get host related information.
            hostEntry = Dns.GetHostEntry(server);

            // Loop through the AddressList to obtain the supported AddressFamily. This is to avoid
            // an exception that occurs when the host IP Address is not compatible with the address family
            // (typical in the IPv6 case).
            foreach (IPAddress address in hostEntry.AddressList)
            {
                IPEndPoint ipe = new IPEndPoint(address, port);
                Socket tempSocket =
                    new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                tempSocket.Connect(ipe);

                if (tempSocket.Connected)
                {
                    s = tempSocket;
                    break;
                }
                else
                {
                    continue;
                }
            }
            return s;
        }

        // This method requests the home page content for the specified server.
        private static string SocketSendReceive(string server, int port)
        {
            Socket s = new Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);
            
            s.Connect("127.0.0.1", port);
            if (s == null)
                return ("Connection failed");


            SendData(s, "<init_connection_request/>");
            Byte[] bytesReceived  = SendData(s, "<ping/>");

            // Receive the server home page content.
            int bytes = 0;
            string page = "Default HTML page on " + server + ":\r\n";


            // The following will block until te page is transmitted.
            do
            {
                bytes = s.Receive(bytesReceived, bytesReceived.Length, 0);
                page = page + Encoding.ASCII.GetString(bytesReceived, 0, bytes);
            }
            while (bytes > 0);

            return page;
        }

        private static Byte[] SendData(Socket s, string dat)
        {
            string request = message_for_xml(dat);//"GET / HTTP/1.1\r\nHost: " + server + "\r\nConnection: Close\r\n\r\n";
            Byte[] bytesSent = Encoding.ASCII.GetBytes(request);
            Byte[] bytesReceived = new Byte[256];
            // Send request to the server.
            s.Send(bytesSent, bytesSent.Length, 0);
            return bytesReceived;
        }

        public static string message_for_xml(string x)
        {
            string ret = "content-length:" + x.Length + "\r\nuid:" + n + "\r\n\r\n" + x;
            n++;
            return ret;
        }


        //7833
        public static void Main()
        {
            System.Console.WriteLine("Here be the start");
            
            //string host;
            //int port = 7833;

            //if (args.Length == 0)
                // If no server name is passed as argument to this program, 
                // use the current host name as the default.
                //host = Dns.GetHostName();
            //else
                //host = args[0];

            //string result = SocketSendReceive("127.0.0.1", port);
            //Console.WriteLine(result);

            AsynchronousClient.StartClient();
            AsynchronousClient.Send("<init_connection_request/>");
            Console.ReadLine();
            SuggestionRequest sq = new SuggestionRequest();
            ElementState s = new ElementState();

            RequestMessage<RelatednessRequest> rq = new RequestMessage<RelatednessRequest>();
            ServiceMessage<RequestMessage<RelatednessRequest>> qqq = new ServiceMessage<RequestMessage<RelatednessRequest>>();
            RelatednessRequest r = new RelatednessRequest();
            
            output = new System.Text.StringBuilder();

            //r.Source = "hereisawikititle";
            //r.serializeToXML(output);
            //Console.WriteLine(output);
            Console.ReadLine();
            
            

            

            //AsynchronousClient.Send(output.ToString());
            //Console.ReadLine();
            //Console.ReadLine();
            //AsynchronousClient.Send("<ping/>");
            Console.ReadLine();
            //AsynchronousClient.Send("<ping/>");
            //Console.ReadLine();
            //AsynchronousClient.Send("<ping/>");
            //Console.ReadLine();
        }
    }
}
