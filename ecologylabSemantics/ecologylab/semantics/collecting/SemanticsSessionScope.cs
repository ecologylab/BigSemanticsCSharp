using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simpl.Fundamental.Net;
using Simpl.Serialization;
using ecologylab.semantics.documentparsers;
using ecologylab.semantics.metadata.builtins;
using ecologylab.semantics.metadata.scalar.types;
using ecologylab.semantics.metametadata;
using Simpl.Fundamental.Collections;
using System.Net;

namespace ecologylab.semantics.collecting
{
    public class SemanticsSessionScope : SemanticsGlobalScope
    {

        public SemanticsSessionScope(SimplTypesScope metadataTranslationScope, string repoLocation)
            : base(metadataTranslationScope, repoLocation)
        {
        }

        #region Properties

        public SimplTypesScope MetadataTranslationScope
        {
            get { return GetMetadataTranslationScope(); }
        }

        public MetaMetadataRepository MetaMetadataRepository
        {
            get { return GetMetaMetadataRepository(); }
        }

        #endregion

        public override Document GetOrConstructDocument(ParsedUri location)
        {
            Document doc = base.GetOrConstructDocument(location);
            doc.SemanticsSessionScope = this;
            return doc;
        }

        public void GetDocument(ParsedUri puri, DocumentParsingDone callback)
        {
            if (puri == null)
            {
                Console.WriteLine("Error: empty URL provided.");
                return;
            }

            Document doc = GetOrConstructDocument(puri);
            DocumentClosure closure = new DocumentClosure(this, doc) {DocumentParsingDoneHandler = callback};
            closure.PerformDownload();
        }

    }
}
