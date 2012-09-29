using System;
using System.Threading;
using System.Windows.Threading;
using Awesomium.Core;
using Ecologylab.Semantics.Collecting;
using Ecologylab.Semantics.Metadata;
using Ecologylab.Semantics.Metadata.Builtins;
using Ecologylab.Semantics.Metametadata;
using Simpl.Fundamental.Net;
using Simpl.Serialization;
using Simpl.Serialization.Context;


namespace Ecologylab.Semantics.Documentparsers
{
    public class WebViewParser
    {
        private readonly WebView _webView;
        private readonly ParsedUri _puri;
        private SemanticsSessionScope SemanticsSessionScope { get; set; }
        private readonly DocumentClosure _closure;
        
        private TimeSpan EXTRACTION_TIMEOUT = TimeSpan.FromSeconds(20);
        private DispatcherTimer _requestTimedOut;
        
        private DateTime timeStart;
        private DateTime parseStart;

        public WebViewParser(DocumentClosure closure)
        {
            _closure = closure;
            SemanticsSessionScope = closure.SemanticsSessionScope;
            _webView = SemanticsSessionScope.WebBrowserPool.Acquire();
            _puri = closure.PURLConnection.ResponsePURL;
            _requestTimedOut = new DispatcherTimer() { Interval = EXTRACTION_TIMEOUT };
            _requestTimedOut.Tick += ExtractionRequestTimedOut;
        }

        void ExtractionRequestTimedOut(object sender, EventArgs e)
        {
            Console.WriteLine("##### Aborting extraction for: " + _puri);
            Console.WriteLine("##### Waited : " + EXTRACTION_TIMEOUT + " for extraction to complete.");
            SemanticsSessionScope.WebBrowserPool.Release(_webView);

            _closure.TaskCompletionSource.SetException(new TimeoutException("Extraction took too long, symptom of something going wrong. Please fix !!!"));
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public void Parse()
        {
            Console.WriteLine("Extraction In Thread Name: " + Thread.CurrentThread.ManagedThreadId);
            //ParsedUri puri, MetaMetadataRepository repository, SimplTypesScope metadataTScope
            
            string uri = _puri.AbsoluteUri;

            Console.WriteLine("Parsing Webpage: " + uri);
            //We need webView to be instantiated correctly.
            _webView.ClearAllURLFilters();

            //Only accept requests for this particular uri
            _webView.SetURLFilteringMode(URLFilteringMode.Whitelist);
            _webView.AddURLFilter(uri);
            //TODO: At a later date, when we want to allow javascript requests, this must change.
            //webView.AddURLFilter("*.js");

            timeStart = DateTime.Now;
            _webView.LoadCompleted += WebView_LoadCompleted;
            _webView.Source = _puri;
        }

        private void WebView_LoadCompleted(object sender, EventArgs e)
        {
            //if (Source == null || BLANK_PAGE.Equals(Source))// || loadingComplete)
            //   return;
            WebView webView = sender as WebView;
            if (webView == null)
                return;
            webView.Stop(); // Stopping further requests.
            Console.WriteLine("Finished loading in " + DateTime.Now.Subtract(timeStart).TotalMilliseconds + "ms. Executing javascript. -- ");
            Console.WriteLine("======================================= Time To Load : " + DateTime.Now.Subtract(timeStart).TotalMilliseconds );
            parseStart = DateTime.Now;
            MetaMetadataRepository repository = SemanticsSessionScope.MetaMetadataRepository;
            MetaMetadata mmd = repository.GetDocumentMM(_puri);
            Console.WriteLine("Got MMD: " + mmd.Name);
            String jsonMMD = WebBrowserPool.GetJsonMMD(mmd);
            //Console.WriteLine("json:\n" + jsonMMD + "\n");
            //jsonMMD = jsonMMD.Replace("\\", "\\\\");

            webView.JSConsoleMessageAdded += new JSConsoleMessageAddedEventHandler(JSConsoleEvent);

            webView.ExecuteJavascript(jsonMMD);
            webView.ExecuteJavascript(WebBrowserPool.MmdDomHelperJsString);
            Console.WriteLine("Done js code execution, calling function. --" + DateTime.Now + " : " + DateTime.Now.Millisecond);
            //TODO: Currently executes asynchronously. Can we make this asynchronous?

            webView.CreateObject("CallBack");
            webView.SetObjectCallback("CallBack", "MetadataExtracted", OnMetadataExtracted);
            
            //webView.ExecuteJavascript("CallBack.MetadataExtracted('some value');");
            //Return happens through the task, on completion of the method via the callback
            webView.ExecuteJavascript("extractMetadataWithCallback(mmd);");

            _requestTimedOut.Start();
        }

        public void OnMetadataExtracted(object sender, JSCallbackEventArgs args)
        {
            if (args.Arguments.Length == 0)
            {
                Console.WriteLine("No value returned");
                return;
            }
            JSValue value = args.Arguments[0];

            String metadataJSON = value.ToString();
            //Console.WriteLine(metadataJSON);
            Console.WriteLine("Extraction time : " + DateTime.Now.Subtract(parseStart).TotalMilliseconds);
            
            Console.WriteLine("Done getting value. Serializing JSON string to ElementState.");
            TranslationContext context = new TranslationContext();
            context.SetUriContext(_puri);
            SimplTypesScope metadataTScope = SemanticsSessionScope.MetadataTranslationScope;
            Document myShinyNewMetadata = (Document)metadataTScope.Deserialize(metadataJSON, context,  new MetadataDeserializationHookStrategy(SemanticsSessionScope), StringFormat.Json);
            Console.WriteLine("Extraction time including page load and deserialization: " + DateTime.Now.Subtract(timeStart).TotalMilliseconds);
            _webView.LoadCompleted -= WebView_LoadCompleted;

            SemanticsSessionScope.GlobalDocumentCollection.AddDocument(myShinyNewMetadata, _puri);

            SemanticsSessionScope.WebBrowserPool.Release(_webView);
            _requestTimedOut.Stop();

            _closure.TaskCompletionSource.TrySetResult(myShinyNewMetadata);
        }

        public void JSConsoleEvent(object sender, JSConsoleMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
