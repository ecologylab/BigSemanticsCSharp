using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using AwesomiumSharp;
using DomExtraction.Properties;
using Simpl.Fundamental.Net;
using Simpl.Serialization;
using Simpl.Serialization.Context;
using ecologylab.semantics.generated.library;
using ecologylab.semantics.metadata;
using ecologylab.semantics.metadata.builtins;
using ecologylab.serialization;

using ecologylab.semantics.metametadata;
using ecologylab.semantics.metadata.scalar.types;

using System.Threading.Tasks;
using System.IO;
using ecologylab.semantics.generated.library.wikipedia;


namespace DomExtraction
{
    public class MMDExtractionBrowser
    {

        SimplTypesScope metadataTScope;
        //String mmdJson;
        String mmdDomHelperJSString;

        static readonly string appPath = System.AppDomain.CurrentDomain.BaseDirectory;

        static readonly String workspace = appPath + @"..\..\..\";
        private String jsPath = workspace + @"DomExtraction\javascript\";

        private static readonly string wikiCacheLocation = appPath;
        private const String wikiPuriPrefix = "http://en.wikipedia.org/wiki/";
        MetaMetadataRepository repo;

        readonly Dictionary<MetaMetadata, String> mmdJSONCache = new Dictionary<MetaMetadata, String>();
        readonly Dictionary<ParsedUri, Metadata> metadataCache = new Dictionary<ParsedUri, Metadata>();

        private List<String> articleTitlesCached = new List<string>();

        const String BLANK_PAGE = "about:blank";

        private WebView webView;

        /// <summary>
        /// 
        /// </summary>
        public MMDExtractionBrowser()
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
                HomeURL = @"http:\\www.google.com",
                // ...Se comments for UserDataPath.
                // Let's gather some extra info for this sample.
                LogLevel = LogLevel.Verbose
            };

            WebCore.Initialize(config, false);
            
            InitRepo();
        }

        /// <summary>
        /// Does the actual Initialization of the Repository
        /// </summary>
        public void InitRepo()
        {
            MetadataScalarType.init();
            SimplTypesScope mmdTScope = MetaMetadataTranslationScope.Get();
            metadataTScope = RepositoryMetadataTranslationScope.Get();

            
            const string testFile = @"MetaMetadataRepository\";
            Console.WriteLine("Initting repository");
            MetaMetadataRepository.stopTheConsoleDumping = true;
            repo = MetaMetadataRepositoryLoader.ReadDirectoryRecursively(workspace + testFile, mmdTScope, metadataTScope);

            mmdDomHelperJSString = File.ReadAllText(jsPath + "mmdDomHelper.js");
            
//            DirectoryInfo di = new DirectoryInfo(wikiCacheLocation);
//            FileInfo[] files = di.GetFiles("*.xml");
//            foreach(var file in files)
//            {
//                string title = file.Name.Substring(0, file.Name.IndexOf("."));
//
//                ParsedUri pur = GetPuriForWikiArticleTitle(title);
//
//                Document elementState = (Document) metadataTScope.Deserialize(file.FullName, StringFormat.Xml);
//                metadataCache.Add(pur, elementState);
//            }
        }

        public async Task<WikipediaPage> GetWikipediaPageForTitle(string title)
        {
            WikipediaPage result = (WikipediaPage) await ExtractMetadata(GetPuriForWikiArticleTitle(title));
            return result;
        }

        public ParsedUri GetPuriForWikiArticleTitle(string title)
        {
            return new ParsedUri(wikiPuriPrefix + title.Replace(' ', '_'));
        }

        public String GetJsonMMD(ParsedUri puri)
        {
            MetaMetadata mmd = repo.GetDocumentMM(puri);
            String result = null;
            if (mmd == null)        //Should bring up the browser !
                return result;
            
            mmdJSONCache.TryGetValue(mmd, out result);

            if (result == null)
            {
                StringBuilder mmdJSON = new StringBuilder();
                mmdJSON.Append("mmd = ");
                SimplTypesScope.Serialize(mmd, StringFormat.Json, null);
                //mmd.serialize(mmdJSON, null);
                mmdJSON.Append(";");
                result = mmdJSON.ToString();
                mmdJSONCache.Add(mmd, result);
            }
            return result;
        }

        /// <summary>
        /// An asynchronous method that returns the metadata of the Uri if available.
        /// Using C# Async CTP from http://msdn.microsoft.com/en-us/vstudio/async.aspx
        /// We would like to not have the caller create delegates for OnCompletion of this metadata extraction,
        /// but instead just use an await and continue control flow.
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public async Task<ElementState> ExtractMetadata(ParsedUri puri=null, String uri = null)
        {
            
            if (puri == null && uri == null)
                return null;
            if (uri == null)
                uri = puri.ToString();
            else
                puri = new ParsedUri(uri);

            Metadata result;

            TaskCompletionSource<ElementState> tcs = new TaskCompletionSource<ElementState>();
            if (metadataCache.TryGetValue(puri, out result))
            {
                tcs.TrySetResult(result);
            }
            else
            {
                Console.WriteLine("Cache Miss. Parsing webpage: " + uri);
                webView = WebCore.CreateWebView(1024, 768);
                //We need webView to be instantiated correctly.
                webView.ClearAllURLFilters();
                //Only accept requests for this particular uri
                
                webView.AddURLFilter(uri);
                //TODO: At a later date, when we want to allow javascript requests, this must change.
                //webView.AddURLFilter("*.js");
                Console.WriteLine("Setting Source");
                webView.Source = uri;
                webView.DomReady += delegate
                {
                    //if (Source == null || BLANK_PAGE.Equals(Source))// || loadingComplete)
                    //   return;
                    webView.Stop(); // Stopping further requests.
                    Console.WriteLine("Finished loading. Executing javascript. -- " + System.DateTime.Now);
                    String jsonMMD = GetJsonMMD(puri);
                    //Console.WriteLine("json:\n" + jsonMMD + "\n");
                    webView.ExecuteJavascript(jsonMMD);
                    webView.ExecuteJavascript(mmdDomHelperJSString);
                    Console.WriteLine("Done js code execution, calling function. --" + System.DateTime.Now);
                    //TODO: Currently executes asynchronously. Can we make this asynchronous?
                    JSValue value = webView.ExecuteJavascriptWithResult("extractMetadata(mmd);");
                    String metadataJSON = value.ToString();
                    //Console.WriteLine(metadataJSON);
                    Console.WriteLine("Done getting value. Serializing JSON string to ElementState. --" + System.DateTime.Now);
                    ElementState myShinyNewMetadata = (ElementState) metadataTScope.Deserialize(metadataJSON, StringFormat.Json);
                    Console.WriteLine("Metadata ElementState object created. " + System.DateTime.Now);

                    //Clean metadata
                    WikipediaPage wikiPage = myShinyNewMetadata as WikipediaPage;
                    Debug.Assert(wikiPage != null, "wikiPage is null");

                    wikiPage.HypertextParas = wikiPage.HypertextParas.Where(p => p.Runs != null).ToList();
                    wikiPage.Thumbinners = wikiPage.Thumbinners.Where(thumb => thumb.ThumbImgSrc != null).ToList();

                    //DEBUGGING only, save the last translated Metadata object as json.
                    String XMLFilePath = wikiCacheLocation + wikiPage.Title.Value.Replace(' ', '_') + ".xml";
                    Console.WriteLine("Writing out the elementstate into " + XMLFilePath);
                    StringBuilder buffy = new StringBuilder();
                    //TODO FIXME Use class descriptor for serialization
                    //wikiPage.serializeToXML(buffy, null);
                    TextWriter tw = new StreamWriter(XMLFilePath);
                    tw.Write(buffy);
                    tw.Close();

                    /*String JSONFilePath = wikiCacheLocation + wikiPage.Title.Value.Replace(' ', '_') + ".json";
                    Console.WriteLine("Writing out the elementstate into " + JSONFilePath);
                    buffy = new StringBuilder();
                    myShinyNewMetadata.serializeToJSON(buffy);
                    tw = new StreamWriter(JSONFilePath);
                    tw.Write(buffy);
                    tw.Close();*/

                    
                    tcs.TrySetResult(myShinyNewMetadata);
                };
            }
            return await tcs.Task;
        }
    }
}
