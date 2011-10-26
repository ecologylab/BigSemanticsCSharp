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
        public override void Parse(SemanticsSessionScope semanticsSessionScope, ParsedUri puri, MetaMetadata metaMetadata)
        {
            SimplTypesScope metadataTScope = semanticsSessionScope.MetadataTranslationScope;
            Document parsedDoc = metadataTScope.Deserialize(OpenStreamForParsedUri(puri), Format.Xml) as Document;
            DocumentParsingDoneHandler(parsedDoc);

            // post parse: regex filtering + field parser
        }
    }
}
