using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ecologylab.Semantics.MetadataNS.Builtins;

namespace Ecologylab.Semantics.Collecting
{
    public class DownloadMonitor
    {
        private readonly Task _downloaderThread;
        

        private readonly BlockingCollection<DocumentClosure> _toDownload;

        public DownloadMonitor()
        {
            _toDownload = new BlockingCollection<DocumentClosure>();
//            _downloaderThread = new Thread(RunDownloadLoop) { Name = "DownloadRequestHandler", IsBackground = true };
//            _downloaderThread.Start();

            _downloaderThread = Task.Factory.StartNew(RunDownloadLoop);
        }

        public void RunDownloadLoop()
        {
            //Todo: Make this a weighted list of some sort.
            //The order of downloads can change after being added into _toDownload.
            foreach (DocumentClosure closure in _toDownload.GetConsumingEnumerable())
            {
                Debug.WriteLine("Performing Download on closure: " + closure.Document.Location);
                Task.Run(() => closure.PerformDownload());
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
