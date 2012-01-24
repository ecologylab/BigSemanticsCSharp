using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using Awesomium.Core;
using ecologylab.semantics.metadata.builtins;

namespace ecologylab.semantics.collecting
{
    public class DownloadMonitor
    {
        private Thread _downloaderThread;

        private BlockingCollection<DocumentClosure> _toDownload = new BlockingCollection<DocumentClosure>();
        public WebBrowserPool WebBrowserPool { get; set; }
        private Thread _awesomiumThread;
        private WebBrowserPool.DispatcherDelegate extractionDelegate;
        public DownloadMonitor(SemanticsSessionScope semanticsSessionScope)
        {

            WebBrowserPool = new WebBrowserPool(semanticsSessionScope);

            _downloaderThread = new Thread(RunDownloadLoop) { Name = "DownloadRequestHandler", IsBackground = true };
            _downloaderThread.Start();
            _awesomiumThread = new Thread(WebBrowserPool.InitializeWebCore) { Name = "Singleton Awesomium Thread", IsBackground = true };
            _awesomiumThread.Start();
            extractionDelegate = WebBrowserPool.ExtractMetadata;
        }

        public void RunDownloadLoop()
        {
            foreach (DocumentClosure closure in _toDownload.GetConsumingEnumerable())
            {
                if (closure == null) continue;

                Console.WriteLine("Performing ExtractionRequest on: " + closure.PURLConnection.ResponsePURL);

                var dispatcher = Dispatcher.FromThread(_awesomiumThread);
                if (dispatcher != null)
                {
                    DispatcherOperation op = dispatcher.BeginInvoke(DispatcherPriority.Send, extractionDelegate, closure);
                    DispatcherOperationStatus status = op.Status;
                    while (status != DispatcherOperationStatus.Completed)
                    {
                        Console.WriteLine("Op Status: " + op.Status);
                        status = op.Wait(TimeSpan.FromMilliseconds(500));    
                    }
                    
                }

                Console.WriteLine("Dispatch Complete");
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="documentClosure"> </param>
        /// <returns></returns>
        public async Task<Document> QueueExtractionRequest(DocumentClosure documentClosure)
        {
            TaskCompletionSource<Document> tcs = new TaskCompletionSource<Document>();
            documentClosure.TaskCompletionSource = tcs;

            _toDownload.TryAdd(documentClosure);

            return await tcs.Task;
        }
    }
}
