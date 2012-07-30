using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using Awesomium.Core;
using Simpl.Fundamental.Net;
using Simpl.Serialization;
using ecologylab.semantics.documentparsers;
using ecologylab.semantics.metadata.builtins;
using ecologylab.semantics.metadata.scalar.types;
using ecologylab.semantics.metametadata;
using Simpl.Fundamental.Collections;
using System.Net;
using ecologylab.semantics.services;

namespace ecologylab.semantics.collecting
{
    public class SemanticsSessionScope : SemanticsGlobalScope
    {

        private readonly Thread _awesomiumThread;

        private readonly DispatcherDelegate _extractionDelegate;
        private Dispatcher dispatcher;
        public WebBrowserPool WebBrowserPool { get; set; }

        public MetadataServicesClient MetadataServicesClient { get; set; }

        public delegate void DispatcherDelegate(DocumentClosure closure);

        public SemanticsSessionScope(SimplTypesScope metadataTranslationScope, string repoLocation)
            : base(metadataTranslationScope, repoLocation)
        {
            //This has an asynchronous call to WebCore.Initialize
            //We might be able to pass in a TCS here, and set completion
            DownloadMonitor = new DownloadMonitor();

//            WebBrowserPool = new WebBrowserPool(this);
//            _extractionDelegate = WebBrowserPool.ExtractMetadata;
//
//            _awesomiumThread = new Thread(WebBrowserPool.InitializeWebCore) { Name = "Singleton Awesomium Thread", IsBackground = true };
//            _awesomiumThread.Start();

            MetadataServicesClient = new MetadataServicesClient(metadataTranslationScope);

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
                Console.WriteLine("Error: empty URL provided.");
                return null;
            }

            Document doc = GetOrConstructDocument(puri);
            DocumentClosure closure = new DocumentClosure(this, doc)
                                          {TaskCompletionSource = new TaskCompletionSource<Document>()};

            DownloadMonitor.QueueExtractionRequest(closure);
            Document documentResult = null;
            try
            {
                documentResult = await closure.TaskCompletionSource.Task;
            
            }
            catch (Exception e)
            {
                Console.WriteLine("Extraction Exception Caught: " + e.Message);
            }

            return documentResult;
        }

        public async static Task<SemanticsSessionScope> InitAsync(SimplTypesScope metadataTranslationScope, string repoLocation)
        {

            var scope = await TaskEx.Run(() => new SemanticsSessionScope(metadataTranslationScope, repoLocation));

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

        public void DispatchClosureToWebViewParser(DocumentClosure closure)
        {
            dispatcher = Dispatcher.FromThread(_awesomiumThread);
            if (dispatcher != null)
                dispatcher.BeginInvoke(DispatcherPriority.Send, _extractionDelegate, closure);
        }

        public static SemanticsSessionScope Get
        {
            get; set;
        }
    }
}
