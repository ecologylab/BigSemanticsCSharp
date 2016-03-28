using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecologylab.BigSemantics.MetadataNS.Builtins;
using Ecologylab.BigSemantics.MetaMetadataNS;
using Simpl.Fundamental.Net;
using Simpl.Serialization;

namespace Ecologylab.BigSemantics.Collecting
{
    public class SemanticsGlobalScope : MetaMetadataRepositoryInit
    {

        private SemanticsGlobalCollection<Document> _globalDocumentCollection;

        public SemanticsGlobalCollection<Document> GlobalDocumentCollection
        {
            get { return _globalDocumentCollection; }
        }

        public virtual async Task<Document> GetOrConstructDocument(ParsedUri location)
        {
            var doc = await GetDocument(location);
   
            /*if(doc == null)
            {
                doc = MetaMetadataRepository.ConstructDocument(location, false);
                if (doc != null)
                    GlobalDocumentCollection.AddDocument(doc, location);
            }*/
            
            return doc;
        }

        public virtual async Task<Document> GetDocument(ParsedUri location)
        {
            if (location == null)
                return null;

            Document doc;
            GlobalDocumentCollection.TryGetDocument(location, out doc);

            return doc;
        }

        public SemanticsGlobalScope(SimplTypesScope metadataTranslationScope, string repoLocation, EventHandler<EventArgs> onCompleted) : base(metadataTranslationScope, repoLocation, onCompleted)
        {
            _globalDocumentCollection = new SemanticsGlobalCollection<Document>();
        }

    }
}
