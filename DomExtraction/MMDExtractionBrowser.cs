using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cjc.ChromiumBrowser;
using ecologylab.semantics.generated.library;
using ecologylab.semantics.interactive.Controls;
using ecologylab.semantics.metadata;
using ecologylab.semantics.metadata.builtins;
using ecologylab.serialization;

using CjcAwesomiumWrapper;
using ecologylab.semantics.metametadata;
using ecologylab.semantics.metadata.scalar.types;

using System.Threading.Tasks;
using ecologylab.net;
using System.IO;


namespace DomExtraction
{
    public class MMDExtractionBrowser : WebBrowser
    {

        TranslationScope metadataTScope;
        //String mmdJson;
        String mmdDomHelperJSString;
        const String workspace = @"C:\Users\damaraju.m2icode\workspace\";
        String jsPath = workspace + @"cSharp\ecologylabSemantics\DomExtraction\javascript\";

        private const String wikiCacheLocation = @"S:\currentResearchers\sashikanth\wikiCache\";
        private const String wikiPuriPrefix = "http://en.wikipedia.org/wiki/";
        MetaMetadataRepository repo;

        Dictionary<MetaMetadata, String> mmdJSONCache = new Dictionary<MetaMetadata, String>();
        Dictionary<ParsedUri, Metadata> metadataCache = new Dictionary<ParsedUri, Metadata>();

        private List<String> articleTitlesCached = new List<string>();

        const String BLANK_PAGE = "about:blank";
        /// <summary>
        /// TODO: Fix instantiation of webview to not depend on overriding the ArrangeOverride method.
        /// </summary>
        public MMDExtractionBrowser()
            :base()
        {
            //Init Awesomium Webview correctly.
            Init();
        }

        /// <summary>
        /// Does the actual Initialization of the Repository
        /// </summary>
        public void InitRepo()
        {
            MetadataScalarScalarType.init();
            TranslationScope mmdTScope = MetaMetadataTranslationScope.get();

            metadataTScope = GeneratedMetadataTranslations.Get();


            string testFile = @"web\code\java\ecologylabSemantics\repository\";
            Console.WriteLine("Initting repository");
            MetaMetadataRepository.stopTheConsoleDumping = true;
            repo = MetaMetadataRepository.ReadDirectoryRecursively(workspace + testFile, mmdTScope, metadataTScope);

            mmdDomHelperJSString = File.ReadAllText(jsPath + "mmdDomHelper.js");
            
            DirectoryInfo di = new DirectoryInfo(wikiCacheLocation);
            FileInfo[] files = di.GetFiles("*.xml");
            foreach(var file in files)
            {
                string title = file.Name.Substring(0, file.Name.IndexOf("."));

                ParsedUri pur = GetPuriForWikiArticleTitle(title);

                Document elementState = (Document) metadataTScope.deserialize(file.FullName, Format.XML);
                metadataCache.Add(pur, elementState);
            }
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
                mmd.serializeToJSON(mmdJSON);
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
                //We need webView to be instantiated correctly.
                SetResourceInterceptor(new ResourceBanner(uri));
                Console.WriteLine("Setting Source");
                Source = uri;
                FinishLoading += delegate
                {
                    if (Source == null || BLANK_PAGE.Equals(Source))// || loadingComplete)
                        return;
                    Console.WriteLine("Finished loading. Executing javascript. -- " + System.DateTime.Now);
                    String jsonMMD = GetJsonMMD(puri);
                    //Console.WriteLine("json:\n" + jsonMMD + "\n");
                    ExecuteJavascript(jsonMMD);
                    ExecuteJavascript(mmdDomHelperJSString);
                    Console.WriteLine("Done js code execution, calling function. --" + System.DateTime.Now);
                    JSValue value = ExecuteJavascriptWithResult("extractMetadata(mmd);").Get();
                    String metadataJSON = (String)value.Value();
                    //Console.WriteLine(metadataJSON);
                    Console.WriteLine("Done getting value. Serializing JSON string to ElementState. --" + System.DateTime.Now);
                    ElementState myShinyNewMetadata = metadataTScope.deserializeString(metadataJSON, Format.JSON, new ParsedUri(uri));
                    Console.WriteLine("Metadata ElementState object created. " + System.DateTime.Now);

                    //Clean metadata
                    WikipediaPage wikiPage = myShinyNewMetadata as WikipediaPage;
                    wikiPage.HypertextParas = wikiPage.HypertextParas.Where(p => p.Runs != null).ToList();
                    wikiPage.Thumbinners = wikiPage.Thumbinners.Where(thumb => thumb.ThumbImgSrc != null).ToList();

                    //DEBUGGING only, save the last translated Metadata object as json.
                    String XMLFilePath = wikiCacheLocation + wikiPage.Title.Value.Replace(' ', '_') + ".xml";
                    Console.WriteLine("Writing out the elementstate into " + XMLFilePath);
                    StringBuilder buffy = new StringBuilder();
                    wikiPage.serializeToXML(buffy);
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

                    Source = BLANK_PAGE;
                    tcs.TrySetResult(myShinyNewMetadata);
                };
            }
            return await AsyncCtpThreadingExtensions.GetAwaiter(tcs.Task);
        }
    }

    public class ResourceBanner : ResourceInterceptorBase
    {

        String uri;

        public ResourceBanner(String uri) 
        {
            this.uri = uri;

        }
        public override ResourceResponse OnRequest(WebView caller, String url, String referrer)
        {
            if (url == uri)
            {
                return new ResourceResponse();
            }
            return ResourceResponse.Create(1, "x", "text/html");
        }
    }
}
