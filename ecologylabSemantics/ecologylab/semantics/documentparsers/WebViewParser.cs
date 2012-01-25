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
using Simpl.Serialization.Context;
using ecologylab.semantics.collecting;
using ecologylab.semantics.metadata.builtins;
using ecologylab.semantics.metametadata;


namespace ecologylab.semantics.documentparsers
{
    public class WebViewParser
    {
        private readonly WebView _webView;
        private readonly ParsedUri _puri;
        private SemanticsSessionScope SemanticsSessionScope { get; set; }
        private TaskCompletionSource<Document> _tcs;
        private TimeSpan EXTRACTION_TIMEOUT = TimeSpan.FromSeconds(200);
        private DispatcherTimer _requestTimedOut;
        public WebViewParser(WebView webView, SemanticsSessionScope scope, ParsedUri puri)
        {
            _webView = webView;
            _puri = puri;
            SemanticsSessionScope = scope;

            _tcs = new TaskCompletionSource<Document>();

            _requestTimedOut = new DispatcherTimer() { Interval = EXTRACTION_TIMEOUT };
            _requestTimedOut.Tick += ExtractionRequestTimedOut;
        }

        void ExtractionRequestTimedOut(object sender, EventArgs e)
        {
            Console.WriteLine("##### Aborting extraction for: " + _puri);
            Console.WriteLine("##### Waited : " + EXTRACTION_TIMEOUT + " for extraction to complete.");
            _tcs.SetException(new TimeoutException("Extraction took too long, symptom of something going wrong. Please fix !!!"));
        }

        /// <summary>
        /// An asynchronous method that returns the metadata of the Uri if available.
        /// Using C# Async CTP from http://msdn.microsoft.com/en-us/vstudio/async.aspx
        /// We would like to not have the caller create delegates for OnCompletion of this metadata extraction,
        /// but instead just use an await and continue control flow.
        /// </summary>
        /// <returns></returns>
        public async Task<Document> ExtractMetadata()
        {
            Console.WriteLine("Extraction In Thread Name: " + Thread.CurrentThread.ManagedThreadId);
            //ParsedUri puri, MetaMetadataRepository repository, SimplTypesScope metadataTScope

            string uri = _puri.AbsoluteUri;
            Document result = null;

            //Document gets added as a dummy by now.
            //SemanticsSessionScope.GlobalDocumentCollection.TryGetDocument(_puri, out result);
            if (result != null)
            {
                _tcs.TrySetResult(result);
            }
            else
            {
                Console.WriteLine("Cache Miss. Parsing Webpage: " + uri);
//                //We need webView to be instantiated correctly.
                _webView.ClearAllURLFilters();
//                //Only accept requests for this particular uri
                _webView.AddURLFilter(uri);
                //TODO: At a later date, when we want to allow javascript requests, this must change.
                //webView.AddURLFilter("*.js");

                Console.WriteLine("Setting Source : " + DateTime.Now + " : " + DateTime.Now.Millisecond);
                _webView.LoadCompleted += webView_LoadCompleted;
                _webView.Source = _puri;

            }
            return await _tcs.Task;
        }

        private void webView_LoadCompleted(object sender, EventArgs e)
        {
            //if (Source == null || BLANK_PAGE.Equals(Source))// || loadingComplete)
            //   return;
            WebView webView = sender as WebView;
            if (webView == null)
                return;
            webView.Stop(); // Stopping further requests.
            Console.WriteLine("Finished loading. Executing javascript. -- " + DateTime.Now + " : " + DateTime.Now.Millisecond);
            MetaMetadataRepository repository = SemanticsSessionScope.MetaMetadataRepository;
            MetaMetadata mmd = repository.GetDocumentMM(_puri);
            Console.WriteLine("Got MMD: " + mmd.Name);
            String jsonMMD = WebBrowserPool.GetJsonMMD(mmd);
            //Console.WriteLine("json:\n" + jsonMMD + "\n");
            //jsonMMD = jsonMMD.Replace("\\", "\\\\");

            webView.ExecuteJavascript(jsonMMD);
            webView.ExecuteJavascript(WebBrowserPool.MmdDomHelperJsString);
            Console.WriteLine("Done js code execution, calling function. --" + DateTime.Now + " : " + DateTime.Now.Millisecond);
            //TODO: Currently executes asynchronously. Can we make this asynchronous?

            webView.CreateObject("CallBack");
            webView.SetObjectCallback("CallBack", "MetadataExtracted", OnMetadataExtracted);
            
            //webView.ExecuteJavascript("CallBack.MetadataExtracted('some value');");
            //Return happens through the task, on completion of the method via the callback
            webView.ExecuteJavascript("extractMetadata(mmd);");

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
            Console.WriteLine(metadataJSON);
            Console.WriteLine("Done getting value. Serializing JSON string to ElementState. --" + DateTime.Now + " : " + DateTime.Now.Millisecond);
            TranslationContext context = new TranslationContext();
            context.SetUriContext(_puri);
            SimplTypesScope metadataTScope = SemanticsSessionScope.MetadataTranslationScope;
            Document myShinyNewMetadata = (Document)metadataTScope.Deserialize(metadataJSON, context, null, StringFormat.Json);
            Console.WriteLine("Metadata ElementState object created. " + DateTime.Now + " : " + DateTime.Now.Millisecond);
            _webView.LoadCompleted -= webView_LoadCompleted;

            SemanticsSessionScope.GlobalDocumentCollection.AddDocument(myShinyNewMetadata, _puri);

            SemanticsSessionScope.DownloadMonitor.WebBrowserPool.Release(_webView);
            _requestTimedOut.Stop();
            _tcs.TrySetResult(myShinyNewMetadata);
        }
    }
}
