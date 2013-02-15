using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ecologylab.Semantics.MetadataNS.Builtins;
using Ecologylab.Semantics.Services;
using Simpl.Fundamental.Net;
using Simpl.Serialization;
using Ecologylab.Semantics.Documentparsers;
using Ecologylab.Semantics.MetadataNS.Scalar.Types;
using Ecologylab.Semantics.MetaMetadataNS;
using Simpl.Fundamental.Collections;
using System.Net;

namespace Ecologylab.Semantics.Collecting
{
    public class SemanticsSessionScope : SemanticsGlobalScope
    {

        private readonly DispatcherDelegate _extractionDelegate;

        public MetadataServicesClient MetadataServicesClient { get; set; }

        public delegate void DispatcherDelegate(DocumentClosure closure);

        public SemanticsSessionScope(SimplTypesScope metadataTranslationScope, string repoLocation, EventHandler<EventArgs> onCompleted)
            : base(metadataTranslationScope, repoLocation, onCompleted)
        {
            DownloadMonitor = new DownloadMonitor();

            MetadataServicesClient = new MetadataServicesClient(metadataTranslationScope, this);

            SemanticsSessionScope.Get = this;
            
        }

        public DownloadMonitor DownloadMonitor { get; private set; }

        public override Document GetOrConstructDocument(ParsedUri location)
        {
            Document doc = base.GetOrConstructDocument(location);
            doc.SemanticsSessionScope = this;
            return doc;
        }

        public async Task<Document> GetDocument(ParsedUri puri)
        {
            if (puri == null)
            {
                Debug.WriteLine("Error: empty URL provided.");
                return null;
            }

            Document doc = GetOrConstructDocument(puri);
            DocumentClosure closure = new DocumentClosure(this, doc)
                                          {TaskCompletionSource = new TaskCompletionSource<Document>()};

            //DownloadMonitor.QueueExtractionRequest(closure);
            Document documentResult = await closure.PerformDownload();
//            try
//            {
//                documentResult = await closure.TaskCompletionSource.Task;
//            
//            }
//            catch (Exception e)
//            {
//                Debug.WriteLine("Extraction Exception Caught: " + e.Message);
//            }

            return documentResult;
        }

        public async static Task<SemanticsSessionScope> InitAsync(SimplTypesScope metadataTranslationScope, string repoLocation)
        {
//            var scope =
//                await Task.Factory.StartNew(() => new SemanticsSessionScope(metadataTranslationScope, repoLocation));

            var scope = await Task.Run(() => new SemanticsSessionScope(metadataTranslationScope, repoLocation, null));
            //This can be improved to pass the TaskCompletionSource, but a little synchronization logic is required.
//            while (!WebCore.IsRunning)
//            {
//                Console.WriteLine("Waiting for WebCore to Initialize");
//                //NOTE: This isn't a Thread.sleep. TaskEx.delay notifies the CPU to return after the timespan,
//                // It doesn't block the thread.
//                await TaskEx.Delay(TimeSpan.FromMilliseconds(300));
//            }

            return scope;
        }

        public static SemanticsSessionScope Get
        {
            get; set;
        }
    }
}
