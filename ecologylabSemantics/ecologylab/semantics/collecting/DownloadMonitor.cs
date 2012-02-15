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
        

        private readonly BlockingCollection<DocumentClosure> _toDownload;

        public DownloadMonitor()
        {
            _toDownload = new BlockingCollection<DocumentClosure>();
            _downloaderThread = new Thread(RunDownloadLoop) { Name = "DownloadRequestHandler", IsBackground = true };
            _downloaderThread.Start();
        }

        public void RunDownloadLoop()
        {
            //Todo: Make this a weighted list of some sort.
            //The order of downloads can change after being added into _toDownload.
            foreach (DocumentClosure closure in _toDownload.GetConsumingEnumerable())
            {
                Console.WriteLine("Performing Download on closure: " + closure.Document.Location);
                TaskEx.Run(() => closure.PerformDownload());
                
            }
        }

        /// <summary>
        /// Passes the DocumentClosure through to the RunDownloadLoop.
        /// Once the resulting extraction is completed, the call site is notified through the await mechanism.
        /// </summary>
        /// <param name="documentClosure"> </param>
        /// <returns></returns>
        public void QueueExtractionRequest(DocumentClosure documentClosure)
        {
            _toDownload.TryAdd(documentClosure);
        }
    }
}
