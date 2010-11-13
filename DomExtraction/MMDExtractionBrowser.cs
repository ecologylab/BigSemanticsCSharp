using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cjc.ChromiumBrowser;
using ecologylab.serialization;

using CjcAwesomiumWrapper;
using ecologylab.semantics.metametadata;
using ecologylab.semantics.metadata.scalar.types;
using System.Threading.Tasks;


namespace DomExtraction
{
    public class MMDExtractionBrowser : WebBrowser
    {

        TranslationScope metadataTScope;
        String mmdJson;
        String js;
        String workspace = @"C:\Users\damaraju.m2icode\workspace\";

        /// <summary>
        /// TODO: Fix instantiation of webview to not depend on overriding the ArrangeOverride method.
        /// </summary>
        public MMDExtractionBrowser()
            :base()
        {
            MetadataScalarScalarType.init();
            TranslationScope tScope = MetaMetadataTranslationScope.get();

            metadataTScope = GeneratedMetadataTranslations.Get();


            string testFile = @"web\code\java\ecologylabSemantics\repository\repositorySources\imdb.xml";
            MetaMetadataRepository repo = (MetaMetadataRepository)tScope.deserialize(workspace + testFile);

            MetaMetadata imdbTitleMMD = repo.repositoryByTagName["imdb_title"];
            StringBuilder mmdJSON = new StringBuilder();
            mmdJSON.Append("mmd = ");
            imdbTitleMMD.serializeToJSON(mmdJSON);
            mmdJSON.Append(";");
            mmdJson = mmdJSON.ToString();
            js = System.IO.File.ReadAllText(workspace + @"cSharp\ecologylabSemantics\DomExtraction\javascript\mmdDomHelper.js");

        }


        

        /// <summary>
        /// Attempting to create an asynchronous method that returns the metadata of the Uri if available.
        /// Using C# Async CTP from http://msdn.microsoft.com/en-us/vstudio/async.aspx
        /// We would like to not have the caller create delegates for OnCompletion of this metadata extraction,
        /// but instead just use an await and continue control flow.
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public async Task<ElementState>  ExtractMetadata(String uri)
        {
            TaskCompletionSource<ElementState> tcs = new TaskCompletionSource<ElementState>();

            //We need webView to be instantiated correctly.
            SetResourceInterceptor(new ResourceBanner(uri));
            Console.WriteLine("Setting Source");
            Source = uri;

            FinishLoading += delegate
            {
                if (Source == null || "about:blank".Equals(Source))
                    return;
                Console.WriteLine("Finished loading. " + System.DateTime.Now);
                Console.WriteLine("Executing javascript");
                ExecuteJavascript(mmdJson.ToString());
                ExecuteJavascript(js);
                Console.WriteLine("Dones execution, calling function. " + System.DateTime.Now);
                JSValue value = ExecuteJavascriptWithResult("extractMetadata(mmd);").Get();
                //JSValue value = browser.ExecuteJavascriptWithResult("myTest();").Get();
                Console.WriteLine("Done getting value. Serializing JSValue now. " + System.DateTime.Now);
                String metadataJSON = (String)value.Value();

                Console.WriteLine("String MetadataJSON received. " + System.DateTime.Now);

                ElementState myShinyNewMetadata = metadataTScope.deserializeString(metadataJSON, Format.JSON); //, new Uri(uri));

                Console.WriteLine("My New Metadata");

                tcs.TrySetResult(myShinyNewMetadata);
            };

            //await TaskEx.ConfigureAwait(tcs.Task, true);
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
            ResourceResponse bannedResponse = ResourceResponse.Create(1, "x", "text/html");
            if (url == uri)
            {
                return new ResourceResponse();
            }
            return bannedResponse;
        }
    }
}
