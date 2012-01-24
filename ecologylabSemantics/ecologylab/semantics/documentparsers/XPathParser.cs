using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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

        public XPathParser()
        {
            
        }

        public override async Task<Document> Parse()
        {
            Console.WriteLine("Parse Called In Thread Name: " + Thread.CurrentThread.ManagedThreadId);
            Document parsedDoc = await SemanticsSessionScope.DownloadMonitor.QueueExtractionRequest(DocumentClosure);

            return parsedDoc;
            //DocumentParsingDoneHandler(parsedDoc);

            // post parse: regex filtering + field parser
        }

       

        private void webView_LoadCompleted(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
