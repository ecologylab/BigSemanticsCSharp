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
        private readonly Thread _downloaderThread;
        
        public delegate void DispatcherDelegate(DocumentClosure closure);

        private readonly BlockingCollection<DocumentClosure> _toDownload;
        public WebBrowserPool WebBrowserPool { get; set; }
        private readonly Thread _awesomiumThread;
        
        private readonly DispatcherDelegate _extractionDelegate;

        
        
        public DownloadMonitor(SemanticsSessionScope semanticsSessionScope)
        {
            _toDownload = new BlockingCollection<DocumentClosure>();

            WebBrowserPool = new WebBrowserPool(semanticsSessionScope);

            _downloaderThread = new Thread(RunDownloadLoop) { Name = "DownloadRequestHandler", IsBackground = true };
            _downloaderThread.Start();
            _awesomiumThread = new Thread(WebBrowserPool.InitializeWebCore) { Name = "Singleton Awesomium Thread", IsBackground = true };
            _awesomiumThread.Start();
            _extractionDelegate = WebBrowserPool.ExtractMetadata;
        }

        public void RunDownloadLoop()
        {
            foreach (DocumentClosure closure in _toDownload.GetConsumingEnumerable())
            {
                Console.WriteLine("Performing ExtractionRequest on: " + closure.PURLConnection.ResponsePURL);

                var dispatcher = Dispatcher.FromThread(_awesomiumThread);
                if (dispatcher != null)
                    dispatcher.BeginInvoke(DispatcherPriority.Send, _extractionDelegate, closure);
            }
        }

        /// <summary>
        /// Passes the DocumentClosure through to the RunDownloadLoop, which in turn dispatches it to the awesomium thread.
        /// 
        /// This stores a TaskCompletionSource object in the document closure.
        /// 
        /// Once the resulting extraction is completed, the call site is notified through the await mechanism.
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
