using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using Simpl.Fundamental.Net;
using Simpl.Serialization;
using Ecologylab.BigSemantics.MetadataNS;
using Ecologylab.BigSemantics.MetadataNS.Builtins;
using Ecologylab.BigSemantics.MetaMetadataNS;
using Ecologylab.BigSemantics.MetadataNS.Scalar.Types;

using System.Threading.Tasks;
using System.IO;
using Simpl.Serialization.Context;
using Ecologylab.BigSemantics.Collecting;


namespace Ecologylab.BigSemantics.Documentparsers
{
    public class XPathParser : DocumentParser
    {

        public override void Parse()
        {
            //SemanticsSessionScope.DispatchClosureToWebViewParser(Metadata.Builtins.DocumentClosure);
            
            //DownloadMonitor.QueueExtractionRequest(DocumentClosure);
            //DocumentParsingDoneHandler(parsedDoc);

            // post parse: regex filtering + field parser
        }
    }
}
