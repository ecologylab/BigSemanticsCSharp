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
            RegisterDocumentParser("xpath", () => new XPathParser());
            RegisterDocumentParser("direct", () => new DirectBindingParser());
        }

        public SemanticsSessionScope SemanticsSessionScope { get; private set; }

        public PURLConnection PURLConnection { get; private set; }

        public MetaMetadataCompositeField MetaMetadata { get; private set; }

        public DocumentClosure DocumentClosure { get; private set; }

        public void FillValues(SemanticsSessionScope semanticsSessionScope, PURLConnection purlConnection, MetaMetadataCompositeField metaMetadata, DocumentClosure documentClosure)
        {
            SemanticsSessionScope = semanticsSessionScope;
            PURLConnection = purlConnection;
            MetaMetadata = metaMetadata;
            DocumentClosure = documentClosure;
        }

        /// <summary>
        /// The main parsing happens here.
        /// </summary>
        public abstract void Parse();

        public DocumentParsingDone DocumentParsingDoneHandler { get; set; }
    }

    public delegate DocumentParser DocumentParserFactoryMethod();

    public delegate void DocumentParsingDone(Document parsedDoc);
}
