using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simpl.Fundamental.Net;
using ecologylab.semantics.metadata.builtins;

namespace ecologylab.semantics.collecting
{
    public class SemanticsSessionScope
    {

      public Document GetDocument(ParsedUri puri)
      {
        throw new NotImplementedException();

          // perhaps, try to connect and handle redirect, get mime type, and probably keep cookies
          // the connection could be reused if not xpath parsing

          // dispatch by parser (xpath / direct binding)

          // post-parsing: regex filtering, field parser

      }

    }
}
