using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using Awesomium.Core;
using Simpl.Fundamental.Net;
using Simpl.Serialization;
using Simpl.Serialization.Context;
using ecologylab.semantics.documentparsers;
using ecologylab.semantics.metadata;
using ecologylab.semantics.metadata.builtins;
using ecologylab.semantics.metametadata;

namespace ecologylab.semantics.collecting
{
    public class WebBrowserPool
    {
        private Stack<WebView> _webViews;
        private const int NUM_WEB_VIEWS = 5;
        public static string MmdDomHelperJsString
        {
            get { return _mmdDomHelperJSString; }
        }

        private static bool IsWebCoreInitialized = false;

        private SemanticsSessionScope SemanticsSessionScope { get; set; }

        private static readonly string appPath = AppDomain.CurrentDomain.BaseDirectory;
        private static readonly string workspace = appPath + @"..\..\..\";
        private static readonly string jsPath = workspace + @"ecologylabSemantics\javascript\";
        private static readonly string _mmdDomHelperJSString;
        private Dispatcher dispatcher;
        private static readonly Dictionary<MetaMetadata, string> mmdJSONCache = new Dictionary<MetaMetadata, string>();

        private DispatcherTimer timer;

        private readonly Object _poolLock;

        static WebBrowserPool()
        {
            _mmdDomHelperJSString = File.ReadAllText(jsPath + "simplDeserializer.js");
            _mmdDomHelperJSString += File.ReadAllText(jsPath + "fieldParsers.js");
            _mmdDomHelperJSString += File.ReadAllText(jsPath + "mmdDomHelper.js");


        }

        public WebBrowserPool(SemanticsSessionScope scope)
        {
            SemanticsSessionScope = scope;
            _poolLock = new Object();
        }

        

        public void InitializeWebCore()
        {
            if (IsWebCoreInitialized)
            {
                throw new InvalidOperationException("Cannot initialize WebBrowserPool more than once.");
            }

            _webViews = new Stack<WebView>();
            Console.WriteLine("-- Initializing WebBrowserPool thread: " + Thread.CurrentThread.ManagedThreadId + " : " +
                              Thread.CurrentThread.Name);
            Console.WriteLine("-- Initializing WebBrowserPool At " + DateTime.Now);

            dispatcher = Dispatcher.CurrentDispatcher;
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
                                           EnableVisualStyles = false,
                                           // ...See comments for UserDataPath.
                                           // Let's gather some extra info for this sample.
                                           LogLevel = LogLevel.Verbose
                                       };
            WebCore.Initialize(config);
            
            for (int i = NUM_WEB_VIEWS - 1; i >= 0; i--)
            {
                WebView view = WebCore.CreateWebView(1024, 768);
                _webViews.Push(view);
            }
            Console.WriteLine("-- Done -- Initializing the WebBrowserPool At " + DateTime.Now);
            Console.WriteLine("Entering Update Loop");

            DispatcherTimer updateTimer = new DispatcherTimer(DispatcherPriority.Send, Dispatcher.CurrentDispatcher)
                                              {Interval = TimeSpan.FromMilliseconds(20)};
            updateTimer.Tick += (sender, args) => WebCore.Update();
            updateTimer.Start();

            IsWebCoreInitialized = true;            
            Dispatcher.Run();
        }


        public WebView Acquire()
        {
            lock (_poolLock)
            {
                if (_webViews.Count == 0)
                {
                    Console.WriteLine("New Webview being created");
                    return WebCore.CreateWebView(1024, 768);

                }

                return _webViews.Pop();
            }
        }

        public void Release(WebView releasedView)
        {
            if (releasedView == null)
            {
                throw new ArgumentNullException("releasedView");
            }

            lock (_poolLock)
            {
                if (!_webViews.Contains(releasedView))
                {
                    releasedView.ClearAllURLFilters();
                    _webViews.Push(releasedView);
                }
                else
                {
                    throw new InvalidOperationException("WebView pool already contains this item");
                }
            }
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



        /// <summary>
        /// Called by the _downloaderThread dispatched to the _awesomiumThread. 
        /// 
        /// 
        /// </summary>
        /// <param name="closure"></param>
        public void ExtractMetadata(DocumentClosure closure)
        {
            Console.WriteLine("Extractor running on thread: " + Thread.CurrentThread.ManagedThreadId);

            WebViewParser parser = new WebViewParser(closure);

            parser.Parse();
        }
    }
}
