using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cjc.ChromiumBrowser;
using ecologylab.serialization;

using CjcAwesomiumWrapper;
using ecologylab.semantics.metametadata;
using ecologylab.semantics.metadata.scalar.types;
using System.Threading.Tasks;
using ecologylab.semantics.generated;
using ecologylab.net;


namespace DomExtraction
{
    public class MMDExtractionBrowser : WebBrowser
    {

        TranslationScope metadataTScope;
        //String mmdJson;
        String js;
        String workspace = @"C:\Users\damaraju.m2icode\workspace\";

        MetaMetadataRepository repo;

        Dictionary<MetaMetadata, String> mmdJSONCache = new Dictionary<MetaMetadata, String>();

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

            repo = MetaMetadataRepository.ReadDirectoryRecursively(workspace + testFile, mmdTScope, metadataTScope);


            //TODO: implement repo.getMMD(uri) correctly.
            //MetaMetadata imdbTitleMMD = null;// repo.repositoryByTagName["imdb_title"];

            js = System.IO.File.ReadAllText(workspace + @"cSharp\ecologylabSemantics\DomExtraction\javascript\mmdDomHelper.js");
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
            TaskCompletionSource<ElementState> tcs = new TaskCompletionSource<ElementState>();
            if (puri == null && uri == null)
                return null;
            if (uri == null)
                uri = puri.ToString();
            else
                puri = new ParsedUri(uri);

            //We need webView to be instantiated correctly.
            SetResourceInterceptor(new ResourceBanner(uri));
            Console.WriteLine("Setting Source");
            Source = uri;
            FinishLoading += delegate
            {
                if (Source == null || "about:blank".Equals(Source))// || loadingComplete)
                    return;
                Console.WriteLine("Finished loading. Executing javascript. -- " + System.DateTime.Now);
                String jsonMMD = GetJsonMMD(puri);
                
                ExecuteJavascript(jsonMMD);
                ExecuteJavascript(js);
                Console.WriteLine("Done js code execution, calling function. --" + System.DateTime.Now);
                JSValue value = ExecuteJavascriptWithResult("extractMetadata(mmd);").Get();
                String metadataJSON = (String)value.Value();
                Console.WriteLine("Done getting value. Serializing JSON string to ElementState. --" + System.DateTime.Now);
                ElementState myShinyNewMetadata = metadataTScope.deserializeString(metadataJSON, Format.JSON); //, new ParsedUri(uri));
                Console.WriteLine("Metadata ElementState object created. " + System.DateTime.Now);

                tcs.TrySetResult(myShinyNewMetadata);
            };

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
