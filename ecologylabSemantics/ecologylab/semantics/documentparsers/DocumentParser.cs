using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simpl.Fundamental.Net;
using ecologylab.semantics.collecting;
using ecologylab.semantics.metadata.builtins;
using ecologylab.semantics.metametadata;
using System.IO;
using System.Net;

namespace ecologylab.semantics.documentparsers
{
    public abstract class DocumentParser
    {
        private static Dictionary<string, DocumentParserFactoryMethod> _registeredDocumentParserFactoryMethods =
            new Dictionary<string, DocumentParserFactoryMethod>();

        public static void RegisterDocumentParser(string parserName, DocumentParserFactoryMethod factoryMethod)
        {
            if (factoryMethod != null)
                _registeredDocumentParserFactoryMethods[parserName] = factoryMethod;
        }

        public static DocumentParser GetDocumentParser(string parserName)
        {
            if (parserName == null)
                return null;
            DocumentParserFactoryMethod factoryMethod = _registeredDocumentParserFactoryMethods[parserName];
            return factoryMethod != null ? factoryMethod() : null;
        }

        static DocumentParser()
        {
            RegisterDocumentParser("xpath", () => new XPathParser2());
            RegisterDocumentParser("direct", () => new DirectBindingParser());
        }

        /// <summary>
        /// The main parsing happens here.
        /// </summary>
        public abstract void Parse(SemanticsSessionScope semanticsSessionScope, ParsedUri puri, MetaMetadata metaMetadata);

        public DocumentParsingDone DocumentParsingDoneHandler { get; set; }

        protected Stream OpenStreamForParsedUri(ParsedUri puri)
        {
            if (puri.IsFile)
            {
                // TODO
            }
            else
            {
                var response = WebRequest.Create(puri).GetResponse();
                var stream = response.GetResponseStream();
                return stream;
            }
            return null;
        }
    }

    public delegate DocumentParser DocumentParserFactoryMethod();

    public delegate void DocumentParsingDone(Document parsedDoc);
}
