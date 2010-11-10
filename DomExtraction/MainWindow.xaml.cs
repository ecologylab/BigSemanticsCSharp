using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CjcAwesomiumWrapper;
using System.Collections.ObjectModel;
using ecologylab.semantics.metametadata;
using ecologylab.serialization;

namespace DomExtraction
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Console.WriteLine("\n\nSetting Source: " + System.DateTime.Now + "\n\n");
            browser.Source = "http://www.imdb.com/title/tt1285016/";
            
            
            //var xpathResult = document.evaluate( xpathExpression, contextNode, namespaceResolver, resultType, result );  
            //More info at: https://developer.mozilla.org/en/Introduction_to_using_XPath_in_JavaScript
            //Mozilla has better documentation, but we're using chromium.
            //Not to worry, we're working with standards here. Both browsers implement the XPath defined in http://www.w3.org/TR/2004/NOTE-DOM-Level-3-XPath-20040226/DOM3-XPath.html
            //string js = "var res = document.evaluate(\"//div[@id='filmo-head-Actor']/following-sibling::div[1]//b/a\", document, null, XPathResult.ORDERED_NODE_SNAPSHOT_TYPE, null);";
            //string js = "var res = document.evaluate(\"//div[@id='filmo-head-Actor']/following-sibling::div[1]//b/a)\", document, null, XPathResult.NUMBER_TYPE, null);";
            //string jsGetCount = "res.snapshotLength";

            String workspace = @"C:\Users\damaraju.m2icode\workspace\";
            string js = System.IO.File.ReadAllText(workspace + @"cSharp\ecologylabSemantics\DomExtraction\javascript\mmdDomHelper.js");

            TranslationScope tScope = MetaMetadataTranslationScope.get();

            MetaMetadataRepository repo = (MetaMetadataRepository) tScope.deserialize(workspace + @"web\code\java\ecologylabSemantics\repository\repositorySources\imdb.xml");

            MetaMetadata imdbTitleMMD = repo.repositoryByTagName["imdb_title"];
            StringBuilder mmdJSON = new StringBuilder();
            mmdJSON.Append("mmd = ");
            imdbTitleMMD.serializeToJSON(mmdJSON);
            mmdJSON.Append(";");
            Console.WriteLine("JSON: \n" + mmdJSON);
            //mmdJSON.Replace("", "'");
            browser.Loaded += delegate
            {
                //We need webView to be instantiated correctly.
                browser.SetResourceInterceptor(new ResourceBanner());
            };

            /*
            browser.DOMReady += delegate
            {
                Console.WriteLine("DomReady " + System.DateTime.Now);

                //browser.Dispose();
            };*/

            browser.FinishLoading += delegate
            {
                Console.WriteLine("Finished loading. " + System.DateTime.Now);
                Console.WriteLine("Executing javascript");
                browser.ExecuteJavascript(mmdJSON.ToString());
                browser.ExecuteJavascript(js);
                Console.WriteLine("Dones execution, calling function. " + System.DateTime.Now);
                JSValue value = browser.ExecuteJavascriptWithResult("extractMetadata(mmd);").Get();
                //JSValue value = browser.ExecuteJavascriptWithResult("myTest();").Get();
                Console.WriteLine("Done getting value. Serializing JSValue now. " + System.DateTime.Now);
                String metadataJSON = null;
                if (value.IsString())
                    metadataJSON = value.ToString();
                Console.WriteLine("String MetadataJSON received. " + System.DateTime.Now);


                //if (value.IsArray())
                //{
                //    Collection<JSValue> arrVals = value.GetArray();
                //}
                //Console.WriteLine("Movies: " + value.ToString());
                //browser.ExecuteJavascript("alert('Number of movies as Actor - ' + res.snapshotLength);");
            };
        }
    }
    
    public class ResourceBanner : ResourceInterceptorBase
    {
        
        public ResourceBanner() { }
        public override ResourceResponse OnRequest(WebView caller, String url, String referrer)
        {
            ResourceResponse bannedResponse = ResourceResponse.Create(1, "x", "text/html");
            if (url == "http://www.imdb.com/name/nm0000168/")
            {
                return new ResourceResponse();
            }
            return bannedResponse;
        }
    }
}
