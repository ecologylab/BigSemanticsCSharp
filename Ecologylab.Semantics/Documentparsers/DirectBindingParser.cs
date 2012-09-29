using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ecologylab.Semantics.MetadataNS;
using Ecologylab.Semantics.MetadataNS.Builtins;
using Simpl.Fundamental.Net;
using Ecologylab.Semantics.Collecting;
using Ecologylab.Semantics.MetaMetadataNS;
using Simpl.Serialization;
using System.Threading.Tasks;

namespace Ecologylab.Semantics.Documentparsers
{
    class DirectBindingParser : DocumentParser
    {
        public override void Parse()
        {
            SimplTypesScope metadataTScope = SemanticsSessionScope.MetadataTranslationScope; ;

            Document parsedDoc = metadataTScope.Deserialize(Simpl.Fundamental.Net.PURLConnection.Stream, Format.Xml) as Document;

            DocumentClosure.TaskCompletionSource.TrySetResult(parsedDoc);

            // post parse: regex filtering + field parser
        }
    }
}
