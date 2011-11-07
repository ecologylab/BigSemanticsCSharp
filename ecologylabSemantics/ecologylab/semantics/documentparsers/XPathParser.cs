using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using AwesomiumSharp;
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

        private static readonly string appPath = System.AppDomain.CurrentDomain.BaseDirectory;
        private static readonly string workspace = appPath + @"..\..\..\";
        private static readonly string jsPath = workspace + @"DomExtraction\javascript\";
        private static readonly string mmdDomHelperJSString;
        private static readonly Dictionary<MetaMetadata, String> mmdJSONCache = new Dictionary<MetaMetadata, String>();
        private static readonly Dictionary<ParsedUri, Metadata> metadataCache = new Dictionary<ParsedUri, Metadata>();

        static XPathParser()
        {
            //Init Awesomium Webview correctly.
            //Note: Do we need special settings for WebCoreConfig?
            //var webCoreConfig = new WebCoreConfig();
            WebCoreConfig config = new WebCoreConfig
            {
                // !THERE CAN ONLY BE A SINGLE WebCore RUNNING PER PROCESS!
                // We have ensured that our application is single instance,
                // with the use of the WPFSingleInstance utility.
                // We can now safely enable cache and cookies.
                SaveCacheAndCookies = true,
                // In case our application is installed in ProgramFiles,
                // we wouldn't want the WebCore to attempt to create folders
                // and files in there. We do not have the required privileges.
                // Furthermore, it is important to allow each user account
                // have its own cache and cookies. So, there's no better place
                // than the Application User Data Path.
                EnablePlugins = true,
                // ...See comments for UserDataPath.
                // Let's gather some extra info for this sample.
                LogLevel = LogLevel.Verbose
            };
            WebCore.Initialize(config, false);

            mmdDomHelperJSString = File.ReadAllText(jsPath + "mmdDomHelper.js");
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
                    Console.WriteLine("Finished loading. Executing javascript. -- " + System.DateTime.Now);
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
                    //Console.WriteLine("json:\n" + jsonMMD + "\n");
                    //jsonMMD = jsonMMD.Replace("\\", "\\\\");

                    webView.ExecuteJavascript(jsonMMD);
                    webView.ExecuteJavascript(mmdDomHelperJSString);
                    Console.WriteLine("Done js code execution, calling function. --" + System.DateTime.Now);
                    //TODO: Currently executes asynchronously. Can we make this asynchronous?
                    JSValue value = webView.ExecuteJavascriptWithResult("extractMetadata(mmd);");
                    String metadataJSON = value.ToString();
                    Console.WriteLine(metadataJSON);
                    Console.WriteLine("Done getting value. Serializing JSON string to ElementState. --" + System.DateTime.Now);
                    //.
                    //metadataTScope.Deserialize()
                    TranslationContext c = new TranslationContext();
                    c.SetUriContext(puri);

                    Document myShinyNewMetadata = (Document)metadataTScope.Deserialize(metadataJSON, c, null, StringFormat.Json);
                    Console.WriteLine("Metadata ElementState object created. " + System.DateTime.Now);

                    //Clean metadata
//                    WikipediaPage wikiPage = myShinyNewMetadata as WikipediaPage;
//                    Debug.Assert(wikiPage != null, "wikiPage is null");
//
//                    wikiPage.HypertextParas = wikiPage.HypertextParas.Where(p => p.Runs != null).ToList();
//                    wikiPage.Thumbinners = wikiPage.Thumbinners.Where(thumb => thumb.ThumbImgSrc != null).ToList();
//
                    //DEBUGGING only, save the last translated Metadata object as json.
//                    String XMLFilePath = wikiCacheLocation + wikiPage.Title.Value.Replace(' ', '_') + ".xml";
//                    Console.WriteLine("Writing out the elementstate into " + XMLFilePath);
//                    StringBuilder buffy = new StringBuilder();
                    //TODO FIXME Use class descriptor for serialization
                    //wikiPage.serializeToXML(buffy, null);
//                    TextWriter tw = new StreamWriter(XMLFilePath);
//                    tw.Write(buffy);
//                    tw.Close();

                    /*String JSONFilePath = wikiCacheLocation + wikiPage.Title.Value.Replace(' ', '_') + ".json";
                    Console.WriteLine("Writing out the elementstate into " + JSONFilePath);
                    buffy = new StringBuilder();
                    myShinyNewMetadata.serializeToJSON(buffy);
                    tw = new StreamWriter(JSONFilePath);
                    tw.Write(buffy);
                    tw.Close();*/


                    tcs.TrySetResult(myShinyNewMetadata);
                    webView.Close();
                };
                webView.Source = uri;
            }
            return await tcs.Task;
        }

    }
}
