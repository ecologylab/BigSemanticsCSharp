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
    class XPathParser : DocumentParser
    {
        private static readonly BrowserPool _browserPool = new BrowserPool(4);

        public override Document Parse(SemanticsSessionScope semanticsSessionScope, ParsedUri puri, MetaMetadata metaMetadata)
        {
            throw new NotImplementedException();
        }
    }
}
