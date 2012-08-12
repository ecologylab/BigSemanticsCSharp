using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simpl.Fundamental.Net;
using Simpl.Serialization;
using ecologylab.semantics.actions;
using ecologylab.semantics.collecting;
using ecologylab.semantics.generated.library;
using ecologylab.semantics.metadata;
using ecologylab.semantics.metadata.builtins;
using ecologylab.semantics.metadata.builtins.declarations;
using ecologylab.semantics.metametadata;

namespace UnitTests
{
    [TestClass]
    public class SemanticOperationTests
    {
        [TestMethod]
        public void AndConditionTest()
        {
            String xml = "<and><or><and /><or /></or><not_null /></and>";
		    AndCondition and = (AndCondition) MetaMetadataTranslationScope.Get().Deserialize(
				    xml, StringFormat.Xml);
		    Console.WriteLine(and);
		    Console.WriteLine(and.Checks);
            Console.WriteLine(SimplTypesScope.Serialize(and, StringFormat.Xml));
        }

        [TestMethod]
	    public void OrConditionTest()// throws SIMPLTranslationException
	    {
		    String xml = "<or><or /><not_null /></or>";
		    OrCondition or = (OrCondition) MetaMetadataTranslationScope.Get().Deserialize(xml, StringFormat.Xml);
		    Console.WriteLine(or);
		    Console.WriteLine(or.Checks);
            Console.WriteLine(SimplTypesScope.Serialize(or, StringFormat.Xml));
	    }

        [TestMethod]
        public void NotConditionTest()// throws SIMPLTranslationException
	    {
		    String xml = "<not><and><or><and /><or /></or><not_null /></and></not>";
		    NotCondition not = (NotCondition) MetaMetadataTranslationScope.Get().Deserialize(xml,
				    StringFormat.Xml);
		    Console.WriteLine(not);
		    Console.WriteLine(not.Check);
            Console.WriteLine(SimplTypesScope.Serialize(not, StringFormat.Xml));
	    }

        [TestMethod]
	    public void ChooseSemanticOperation()// throws SIMPLTranslationException
	    {
		    String xml = "<choose><case><not_null /><get_field /><for_each /></case><case><not_null /><set_metadata /></case><otherwise><get_field /></otherwise></choose>";
		    //String xml2 = "<filter_location><alternative_host>dl.acm.org</alternative_host><set_param name=\"preflayout\" value=\"flat\" /><strip_param name=\"coll\" /></filter_location>";
            //String xml3 = "<set_param name=\"preflayout\" value=\"flat\" />";
            
            //Object obj = MetaMetadataTranslationScope.Get().Deserialize(xml2, StringFormat.Xml);
            ChooseSemanticOperation choose = (ChooseSemanticOperation) MetaMetadataTranslationScope.Get().Deserialize(xml, StringFormat.Xml);
		    Console.WriteLine(choose);
		    Console.WriteLine(choose.Cases);
		    Console.WriteLine(choose.Otherwise);
            Console.WriteLine(SimplTypesScope.Serialize(choose, StringFormat.Xml));
	    }


        [TestMethod]
        public void SemanticOperationTest()// throws SIMPLTranslationException
        {
            String collectedExampleUrlMetadata = @"..\..\..\..\..\MetaMetadataRepository\MmdRepository\testData\collectedExampleUrlMetadata.xml";
            String collectedExampleUrlMetadataNoAuthor = @"..\..\Data\InformationCompositionDeclarationExample.xml";
            String useAuthor = @"..\..\Data\InformationCompositionDeclarationUseAuthor.xml";

            SimplTypesScope _repositoryMetadataTranslationScope = RepositoryMetadataTranslationScope.Get();

            SemanticsGlobalScope _semanticsSessionScope = new SemanticsSessionScope(
                                        _repositoryMetadataTranslationScope,
                                        MetaMetadataRepositoryInit.DEFAULT_REPOSITORY_LOCATION);

            InformationCompositionDeclaration doc = (InformationCompositionDeclaration)_repositoryMetadataTranslationScope.DeserializeFile(collectedExampleUrlMetadata, Format.Xml);
            
            foreach (Metadata metadata in doc.Metadata)
            {
                MetaMetadata metaMetadata = (MetaMetadata) metadata.MetaMetadata;

                SemanticOperationHandler handler = new SemanticOperationHandler(_semanticsSessionScope, null);
                handler.TakeSemanticOperations(metaMetadata, metadata, metaMetadata.SemanticActions);
            }

            Console.WriteLine(SimplTypesScope.Serialize(doc, StringFormat.Xml));
        }

        [TestMethod]
        public void BeforeSemanticOperationTest()// throws SIMPLTranslationException
        {
            //test FilterLocation.paramOps & alternativeHosts
            String url1 = "http://dl.acm.org/citation.cfm?id=2063231.2063237&amp;coll=DL";
            
            //test FilterLocation.stripPrefix
            String url2 = "http://www.amazon.co.uk/gp/bestsellers/books/515344/ref=123";

            //test FilterLocation.Regex
            //added in file products.xml in meta_metadata name="amazon_bestseller_list":
            //<before_semantic_actions>
	        //  <filter_location>
	        //      <regex match="http://([w]+)\.amazon\.com/gp" replace="http://t$1.gstatic.com/images" />
	        //  </filter_location>
		    //</before_semantic_actions>	
            String url3 = "http://www.amazon.com/gp/bestsellers/books/6";

            SimplTypesScope _repositoryMetadataTranslationScope = RepositoryMetadataTranslationScope.Get();

            SemanticsGlobalScope _semanticsSessionScope = new SemanticsSessionScope(
                                        _repositoryMetadataTranslationScope,
                                        MetaMetadataRepositoryInit.DEFAULT_REPOSITORY_LOCATION);

            Document metadata = _semanticsSessionScope.GetOrConstructDocument(new ParsedUri(url1));

            MetaMetadata metaMetadata = (MetaMetadata)metadata.MetaMetadata;

            SemanticOperationHandler handler = new SemanticOperationHandler(_semanticsSessionScope, null);
            handler.TakeSemanticOperations(metaMetadata, metadata, metaMetadata.BeforeSemanticActions);
        }

    }
}
