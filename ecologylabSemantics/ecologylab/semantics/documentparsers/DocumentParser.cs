using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simpl.Fundamental.Net;
using Simpl.Serialization;
using ecologylab.semantics.collecting;
using ecologylab.semantics.metadata;
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
        /// Make parsing Asynchronous everywhere, cause it's invariably IO/Processing bound
        /// </summary>
        public virtual async Task<Document> Parse()
        {
            throw new NotImplementedException();
        }

        //public DocumentParsingDone DocumentParsingDoneHandler { get; set; }


        /**
         * @return the document
         */
        public Document GetDocument()
        {
            return DocumentClosure.Document;
        }
        
	    Stack<MetaMetadataNestedField>	currentMMstack	= new Stack<MetaMetadataNestedField>();

	    Boolean deserializingRoot	= true;
	    /**
	     * For the root, compare the meta-metadata from the binding with the one we started with. Down the
	     * hierarchy, try to perform similar bindings.
	     */
	    public void DeserializationPreHook(Metadata deserializedMetadata, MetadataFieldDescriptor mfd)
	    {
		    if (deserializingRoot)
		    {
			    deserializingRoot							= false;
			    Document document							= GetDocument();
			    MetaMetadataCompositeField preMM			= document.MetaMetadata;
			    MetadataClassDescriptor mcd					= (MetadataClassDescriptor) ClassDescriptor.GetClassDescriptor(deserializedMetadata);;
			    MetaMetadataCompositeField metaMetadata;
			    String tagName 								= mcd.TagName;
			    if (preMM.GetTagForTranslationScope().Equals(tagName))
			    {
				    metaMetadata							= preMM;
			    }
			    else
			    {	// just match in translation scope
				    //TODO use local TranslationScope if there is one
				    metaMetadata							= SemanticsSessionScope.MetaMetadataRepository.GetMMByName(tagName);
			    }
			    deserializedMetadata.MetaMetadata = metaMetadata;

			    currentMMstack.Push(metaMetadata);
		    }
		    else
		    {
			    String mmName = mfd.MmName;
			    MetaMetadataNestedField currentMM = currentMMstack.Peek();
			    MetaMetadataNestedField childMMNested = (MetaMetadataNestedField) currentMM.LookupChild(mmName); // this fails for collections :-(
			    if (childMMNested == null)
				    throw new Exception("Can't find composite child meta-metadata for " + mmName + " amidst "+ mfd +
						    "\n\tThis probably means there is a conflict between the meta-metadata repository and the runtime."+
						    "\n\tProgrammer: Have you Changed the fields in built-in Metadata subclasses without updating primitives.xml???!");
			    MetaMetadataCompositeField childMMComposite = null;
			    if (childMMNested.IsPolymorphicInherently)
			    {
				    String tagName = ClassDescriptor.GetClassDescriptor(deserializedMetadata).TagName;
                    childMMComposite = SemanticsSessionScope.MetaMetadataRepository.GetMMByName(tagName);
			    }
			    else
			    {
			        childMMComposite = childMMNested.GetMetaMetadataCompositeField();
			    }
			    deserializedMetadata.MetaMetadata = childMMComposite;
			    currentMMstack.Push(childMMComposite);
		    }
	    }

	    public void deserializationPostHook(Metadata deserializedMetadata, MetadataFieldDescriptor mfd)
	    {
		    currentMMstack.Pop();
	    }

    }

    public delegate DocumentParser DocumentParserFactoryMethod();

    public delegate void DocumentParsingDone(Document parsedDoc);
}
