using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simpl.Fundamental.Net;
using ecologylab.semantics.metadata.builtins;

namespace ecologylab.semantics.documentparsers
{
    public interface IBrowserWrapper
    {
        Task<Document> ExtractMetadata(ParsedUri puri);

        // potentially other methods ...
    }
}
