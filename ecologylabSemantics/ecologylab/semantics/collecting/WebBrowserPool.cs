using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Awesomium.Core;

namespace ecologylab.semantics.collecting
{
    public class WebBrowserPool
    {


        private Stack<WebView> _webViews;
        private const int NUM_WEB_VIEWS = 5;

        private readonly Object _poolLock;

        static WebBrowserPool()
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
            

        }

        public WebBrowserPool(int capacity = NUM_WEB_VIEWS)
        {
            _poolLock = new Object();

            _webViews = new Stack<WebView>();
            for (int i = capacity - 1; i >= 0; i--)
            {
                WebView view = WebCore.CreateWebView(1024, 768);
                _webViews.Push(view);
            }
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
                    _webViews.Push(releasedView);
                }
                else
                {
                    throw new InvalidOperationException("WebView pool already contains this item");
                }
            }
        }
    }
}
