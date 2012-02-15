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

        public override void Parse()
        {
            SemanticsSessionScope.DispatchClosureToWebViewParser(DocumentClosure);
            
            //DownloadMonitor.QueueExtractionRequest(DocumentClosure);
            //DocumentParsingDoneHandler(parsedDoc);

            // post parse: regex filtering + field parser
        }
    }
}
