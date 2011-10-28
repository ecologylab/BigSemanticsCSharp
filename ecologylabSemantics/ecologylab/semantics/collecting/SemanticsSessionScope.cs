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

        public MetaMetadata Connect(ParsedUri puri, out string mimeType)
        {
            var request = WebRequest.Create(puri);
            var response = request.GetResponse();
            mimeType = response.ContentType;

            MetaMetadata mmd = MetaMetadataRepository.GetDocumentMM(puri);
            if (mmd == null)
            {
                // TODO connect and use MIME type. handle redirects.
            }
            return mmd;
        }

        public void GetDocument(ParsedUri puri, DocumentParsingDone callback)
        {
            if (puri == null)
            {
                Console.WriteLine("Error: empty URL provided.");
                return;
            }

            string mimeType = null;
            MetaMetadata mmd = Connect(puri, out mimeType);
            string parserName = null;
            if (IsXml(mimeType))
                parserName = "direct";
            else
                parserName = mmd.Parser;
            if (mmd == null && parserName == "xpath")
            {
                Console.WriteLine("No meta-metadata found for URL: " + puri.ToString() + " .");
                return;
            }
            else
            {
                if (parserName != null)
                {
                    DocumentParser parser = DocumentParser.GetDocumentParser(parserName);
                    if (parser == null)
                    {
                        Console.WriteLine("Parser not defined: " + parserName);
                        return;
                    }
                    else
                    {
                        parser.DocumentParsingDoneHandler = callback;
                        parser.Parse(this, puri, mmd);
                    }
                }
                else
                {
                    Console.WriteLine("No parser defined for meta-metadata: " + mmd.Name);
                }
            }
        }

        private static bool IsXml(string mimeType)
        {
            if (mimeType == null)
                return false;
            if (mimeType.StartsWith("text/xml") ||
                mimeType.StartsWith("text/rss") ||
                mimeType.StartsWith("application/xml") ||
                mimeType.StartsWith("application/rss") ||
                mimeType.StartsWith("xml/rss"))
                return true;
            return false;
        }

    }
}
