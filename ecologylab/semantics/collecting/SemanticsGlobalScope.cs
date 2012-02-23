using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simpl.Fundamental.Net;
using Simpl.Serialization;
using ecologylab.semantics.metadata.builtins;
using ecologylab.semantics.metametadata;

namespace ecologylab.semantics.collecting
{
    public class SemanticsGlobalScope : MetaMetadataRepositoryInit
    {

        private SemanticsGlobalCollection<Document> _globalDocumentCollection;

        public SemanticsGlobalCollection<Document> GlobalDocumentCollection
        {
            get { return _globalDocumentCollection; }
        }

        public virtual Document GetOrConstructDocument(ParsedUri location)
        {
            if (location == null)
                return null;
            
            Document doc;

            GlobalDocumentCollection.TryGetDocument(location, out doc);
            if(doc == null)
            {
                doc = MetaMetadataRepository.ConstructDocument(location, false);
                if (doc != null)
                    GlobalDocumentCollection.AddDocument(doc, location);
            }
            
            return doc;
        }

        public SemanticsGlobalScope(SimplTypesScope metadataTranslationScope, string repoLocation) : base(metadataTranslationScope, repoLocation)
        {
            _globalDocumentCollection = new SemanticsGlobalCollection<Document>();
        }

    }
}
