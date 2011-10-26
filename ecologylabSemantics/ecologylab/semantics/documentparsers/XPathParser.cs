using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simpl.Fundamental.Net;
using Simpl.Fundamental.Collections;
using ecologylab.semantics.collecting;
using ecologylab.semantics.metadata.builtins;
using ecologylab.semantics.metametadata;

namespace ecologylab.semantics.documentparsers
{
    public class XPathParser : DocumentParser
    {
        public override void Parse(SemanticsSessionScope semanticsSessionScope, ParsedUri puri, MetaMetadata metaMetadata)
        {
            // TODO how to introduce dependency on MMDExtractionBrowser and use it here?
            // TODO pool MMDExtractionBrowser objects for performance.
            Document parsedDoc = null;

            // callback
            DocumentParsingDoneHandler(parsedDoc);

            // post parse: regex filtering + field parser
        }
    }

}
