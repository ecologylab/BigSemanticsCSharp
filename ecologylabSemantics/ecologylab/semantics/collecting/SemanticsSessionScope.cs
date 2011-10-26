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

namespace ecologylab.semantics.collecting
{
    public class SemanticsSessionScope : SemanticsGlobalScope
    {

        private SimplTypesScope _metaMetadataTypesScope;
        private MetaMetadataRepositoryInit _repositoryInitter;
        private MetaMetadataRepository _repository;
        private SimplTypesScope _metadataTypesScope;

        public SemanticsSessionScope(SimplTypesScope metadataTranslationScope, string repoLocation) : base(metadataTranslationScope, repoLocation)
        {
        }

        public SimplTypesScope MetadataTranslationScope
        {
            get { return GetMetadataTranslationScope(); }
        }

        public MetaMetadataRepository MetaMetadataRepository
        {
            get { return GetMetaMetadataRepository(); }
        }

        public MetaMetadata Connect(ParsedUri puri)
        {
            MetaMetadata mmd = MetaMetadataRepository.GetDocumentMM(puri);
            if (mmd == null)
            {
                // TODO connect and use MIME type. handle redirects.
            }
            return mmd;
        }

        public Document GetDocument(ParsedUri puri)
        {
            if (puri == null)
            {
                Console.Error.WriteLine("Error: empty URL provided.");
                return null;
            }

            MetaMetadata mmd = Connect(puri);
            if (mmd == null)
            {
                Console.Error.WriteLine("No meta-metadata found for URL: " + puri.ToString() + " .");
                return null;
            }
            else
            {
                string parserName = mmd.Parser ?? DocumentParser.DEFAULT_PARSER_NAME;
                DocumentParser parser = DocumentParser.GetDocumentParser(parserName);
                if (parser == null)
                {
                    Console.Error.WriteLine("Parser not defined: " + parserName);
                    return null;
                }
                else
                {
                    Document result = parser.Parse(this, puri, mmd);
                    return result;
                }
            }
        }

    }
}
