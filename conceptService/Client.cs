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
using System.Threading.Tasks;

namespace conceptService
{

    class Client
    {
        private static System.Text.StringBuilder output;
        
         async public static Task<int> Foo(int a)
         {
                         
           try{
               Thread.Sleep(1000);            
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            //return 5;
          //return 0;
           return 5;
        }
        /*
         async public static Task<object> GetResponse(ElementState request)
         {

             try
             {
                 Thread.Sleep(1000);
             }
             catch (Exception e)
             {
                 Console.WriteLine(e.ToString());
             }

             //return 5;
             //return 0;
             return 5;
         }
        */

        async public static void Main()
        {
            
            System.Console.WriteLine("Here be the start");

            //AsynchronousClient client = new AsynchronousClient("127.0.0.1",7833);
            OODSSClient client = new OODSSClient("achilles.cse.tamu.edu", 11355);

            ElementState s = null;// client.GetResponse(new InitConnectionRequest()).Result;
            //Console.WriteLine(s.GetType());
            Console.WriteLine("Press enter to update context ");
            Console.ReadLine();

            TranslationScope ts = WikxplorerMessageTranslationScope.Get();

            UpdateContextRequest uc = new UpdateContextRequest();
            uc.Action = 1;//add
            uc.Title = "Creativity";
            s = client.GetResponse(uc,ts).Result;
            Console.WriteLine(s.GetType());

            Console.WriteLine("Press enter to get a related request");
            Console.ReadLine();

            RelatednessRequest rr = new RelatednessRequest();
            rr.Source = "Cognitive science";
            s = client.GetResponse(rr, ts).Result;
            Console.WriteLine(s.GetType());


            Console.WriteLine("Press enter to get a suggestion request");
            Console.ReadLine();

			SuggestionRequest sr = new SuggestionRequest();
            sr.Source = "Information visualization";
            s = client.GetResponse(sr, ts).Result;
            Console.WriteLine(s.GetType());

			

            //something = something
            
            /*
            SuggestionRequest sq = new SuggestionRequest();
            ElementState s = new ElementState();

            RequestMessage<RelatednessRequest> rq = new RequestMessage<RelatednessRequest>();
            ServiceMessage<RequestMessage<RelatednessRequest>> qqq = new ServiceMessage<RequestMessage<RelatednessRequest>>();
            RelatednessRequest r = new RelatednessRequest();
            
            output = new System.Text.StringBuilder();
            Console.ReadLine();
            AsynchronousClient.Receive();
            UpdateContextRequest ucr = new UpdateContextRequest();
            ucr.Action = 1;
            ucr.Title = "Creativity";
            output = new StringBuilder();
            ucr.serializeToXML(output);
            AsynchronousClient.Send(output.ToString());

            AsynchronousClient.Receive();
            SuggestionRequest sr = new SuggestionRequest();
            sr.Source = "Information visualization";
            output = new StringBuilder();
            sr.serializeToXML(output);
            Console.ReadLine();
            AsynchronousClient.Send(output.ToString());

            */

            Console.ReadLine();
            Console.ReadLine();
            
            /*
            Console.WriteLine("Calling foo");

            
            int b;
            Task<int> c = Foo(4);            
            Console.WriteLine("Got"+ c.Result);
            
            Console.WriteLine("Continuing...");            
            Console.ReadLine();

            Console.WriteLine("Starting");
         */
        }
         
    }
}
