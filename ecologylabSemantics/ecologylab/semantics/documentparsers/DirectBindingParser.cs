using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simpl.Fundamental.Net;
using ecologylab.semantics.collecting;
using ecologylab.semantics.metadata.builtins;
using ecologylab.semantics.metametadata;
using Simpl.Serialization;
using System.Threading.Tasks;

namespace ecologylab.semantics.documentparsers
{
    class DirectBindingParser : DocumentParser
    {
        public override async Task<Document> Parse()
        {
            SimplTypesScope metadataTScope = SemanticsSessionScope.MetadataTranslationScope; ;

            Document parsedDoc = metadataTScope.Deserialize(PURLConnection.Stream, Format.Xml) as Document;

            return parsedDoc;
            //DocumentParsingDoneHandler(parsedDoc);

            // post parse: regex filtering + field parser
        }
    }
}
