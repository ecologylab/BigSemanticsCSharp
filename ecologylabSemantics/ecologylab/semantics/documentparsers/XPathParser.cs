using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Awesomium.Core;
using Simpl.Fundamental.Net;
using Simpl.Serialization;
using ecologylab.semantics.metadata;
using ecologylab.semantics.metadata.builtins;
using ecologylab.semantics.metametadata;
using ecologylab.semantics.metadata.scalar.types;

using System.Threading.Tasks;
using System.IO;
using Simpl.Serialization.Context;
using ecologylab.semantics.collecting;


namespace ecologylab.semantics.documentparsers
{
    public class XPathParser : DocumentParser
    {

        private static readonly string appPath = AppDomain.CurrentDomain.BaseDirectory;
        private static readonly string workspace = appPath + @"..\..\..\";
        private static readonly string jsPath = workspace + @"ecologylabSemantics\javascript\";
        private static readonly string _mmdDomHelperJSString;
        private static readonly Dictionary<MetaMetadata, String> mmdJSONCache = new Dictionary<MetaMetadata, String>();
        private static readonly Dictionary<ParsedUri, Metadata> metadataCache = new Dictionary<ParsedUri, Metadata>();

        public XPathParser()
        {
            
        }

        static XPathParser()
        {
            _mmdDomHelperJSString = File.ReadAllText(jsPath + "mmdDomHelper.js");
        }

        public static string GetJsonMMD(MetaMetadata mmd)
        {
            if (mmd == null)
                return null;

            string result = null;
            lock (mmdJSONCache)
            {
                mmdJSONCache.TryGetValue(mmd, out result);
            }
            if (result == null)
            {
                StringBuilder mmdJSON = new StringBuilder();
                mmdJSON.Append("mmd = ");
                mmdJSON.Append(SimplTypesScope.Serialize(mmd, StringFormat.Json));
                mmdJSON.Append(";");
                result = mmdJSON.ToString();
                lock (mmdJSONCache)
                {
                    mmdJSONCache.Add(mmd, result);
                }
            }
            return result;
        }

        private WebView webView;

        public override async void Parse()
        {
            ParsedUri puri = PURLConnection.ResponsePURL;
            MetaMetadata mmd = MetaMetadata as MetaMetadata;
            Document parsedDoc = await ExtractMetadata(puri, SemanticsSessionScope.MetaMetadataRepository, SemanticsSessionScope.MetadataTranslationScope) as Document;
            DocumentParsingDoneHandler(parsedDoc);

            // post parse: regex filtering + field parser
        }

        /// <summary>
        /// An asynchronous method that returns the metadata of the Uri if available.
        /// Using C# Async CTP from http://msdn.microsoft.com/en-us/vstudio/async.aspx
        /// We would like to not have the caller create delegates for OnCompletion of this metadata extraction,
        /// but instead just use an await and continue control flow.
        /// </summary>
        /// <param name="puri"></param>
        /// <returns></returns>
        public async Task<Document> ExtractMetadata(ParsedUri puri, MetaMetadataRepository repository, SimplTypesScope metadataTScope)
        {
            if (puri == null)
                return null;
            string uri = puri.AbsoluteUri;
            Metadata result = null;

            TaskCompletionSource<Document> tcs = new TaskCompletionSource<Document>();
            if (metadataCache.TryGetValue(puri, out result))
            {
                tcs.TrySetResult(result as Document);
            }
            else
            {
                Console.WriteLine("Cache Miss. Parsing Webpage: " + uri);
                webView = WebCore.CreateWebView(1024, 768);
                //We need webView to be instantiated correctly.
                webView.ClearAllURLFilters();
                //Only accept requests for this particular uri
                webView.AddURLFilter(uri);
                //TODO: At a later date, when we want to allow javascript requests, this must change.
                //webView.AddURLFilter("*.js");

                Console.WriteLine("Setting Source");
                webView.LoadCompleted += delegate
                {
                    //if (Source == null || BLANK_PAGE.Equals(Source))// || loadingComplete)
                    //   return;
                    webView.Stop(); // Stopping further requests.
                    Console.WriteLine("Finished loading. Executing javascript. -- " + DateTime.Now);
                    MetaMetadata mmd = repository.GetDocumentMM(puri);

                    // this manipulation of metadata class descriptors is just a workaround.
                    // the correct way of doing this might be to use some hook strategy.
                    var mmdCD = mmd.MetadataClassDescriptor;
                    if (metadataTScope.GetClassDescriptorByTag(mmd.Name) == null)
                    {
                        lock (metadataTScope)
                        {
                            metadataTScope.EntriesByTag.Add(mmd.Name, mmdCD);
                        }
                    }

                    String jsonMMD = GetJsonMMD(mmd);
                    Console.WriteLine("json:\n" + jsonMMD + "\n");
                    //jsonMMD = jsonMMD.Replace("\\", "\\\\");

                    webView.ExecuteJavascript(jsonMMD);
                    webView.ExecuteJavascript(mmdDomHelperJSString);
                    Console.WriteLine("Done js code execution, calling function. --" + DateTime.Now);
                    //TODO: Currently executes asynchronously. Can we make this asynchronous?
                    JSValue value = webView.ExecuteJavascriptWithResult("extractMetadata(mmd);");
                    String metadataJSON = value.ToString();
                    Console.WriteLine(metadataJSON);
                    Console.WriteLine("Done getting value. Serializing JSON string to ElementState. --" + DateTime.Now);
                    TranslationContext c = new TranslationContext();
                    c.SetUriContext(puri);
                    Document myShinyNewMetadata = (Document)metadataTScope.Deserialize(metadataJSON, c, null, StringFormat.Json);
                    Console.WriteLine("Metadata ElementState object created. " + DateTime.Now);
                    tcs.TrySetResult(myShinyNewMetadata);
                    webView.Close();
                };
                webView.Source = puri;
            }
            return await tcs.Task;
        }

    }
}
