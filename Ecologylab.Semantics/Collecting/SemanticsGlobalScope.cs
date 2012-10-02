﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ecologylab.Semantics.MetadataNS.Builtins;
using Ecologylab.Semantics.MetaMetadataNS;
using Simpl.Fundamental.Net;
using Simpl.Serialization;

namespace Ecologylab.Semantics.Collecting
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