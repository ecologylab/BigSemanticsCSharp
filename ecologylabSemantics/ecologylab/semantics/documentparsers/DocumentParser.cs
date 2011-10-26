using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simpl.Fundamental.Net;
using ecologylab.semantics.collecting;
using ecologylab.semantics.metadata.builtins;
using ecologylab.semantics.metametadata;

namespace ecologylab.semantics.documentparsers
{
    public abstract class DocumentParser
    {

        public static readonly string DEFAULT_PARSER_NAME = "xpath";

        public delegate DocumentParser DocumentParserFactoryMethod();

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
            RegisterDocumentParser("xpath", () => new XPathParser());
            RegisterDocumentParser("direct", () => new DirectBindingParser());
        }

        /// <summary>
        /// The main parsing happens here.
        /// </summary>
        public abstract Document Parse(SemanticsSessionScope semanticsSessionScope, ParsedUri puri, MetaMetadata metaMetadata);

    }
}
