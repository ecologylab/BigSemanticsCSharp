using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simpl.Fundamental.Net;

namespace ecologylab.semantics.documentparsers
{
    abstract class ParserBase : DocumentParser
    {

        private ParsedUri _puri;

        public ParserBase(ParsedUri puri)
        {
            _puri = puri;
        }

        public override void Parse()
        {
            // connect, handle redirect, select meta-metadata

            throw new NotImplementedException();
        }

        public override void PostParse()
        {
            throw new NotImplementedException();
        }

    }
}
