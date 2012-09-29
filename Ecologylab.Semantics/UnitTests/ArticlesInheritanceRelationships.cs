using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ecologylab.semantics.metametadata;
using Simpl.Serialization;
using ecologylab.semantics.generated.library;
using ecologylab.semantics.metadata.scalar.types;

namespace UnitTests
{
    /// <summary>
    /// ported from Java project simplTranslators, source folder 'test', 
    /// class ecologylab.semantics.compiler.TestNewMetaMetadataCompiler
    /// </summary>
    [TestClass]
    public class ArticlesInheritanceRelationships
    {
        [TestMethod]
        public void TestArticlesInheritanceRelationships()
        {

            MetaMetadataRepositoryLoader loader = new MetaMetadataRepositoryLoader();
            SimplTypesScope metadataTranslationScope = RepositoryMetadataTranslationScope.Get();
            SimplTypesScope mmdTScope = MetaMetadataTranslationScope.Get();
            MetaMetadataRepository repository = MetaMetadataRepositoryLoader.ReadDirectoryRecursively(
                "../../../UnitTests/Data/TestRepository",
                mmdTScope,
                metadataTranslationScope);

            repository.TraverseAndInheritMetaMetadata();

            MetaMetadata metadata = repository.GetMMByName("metadata");
            Assert.IsNull(metadata.InheritedMmd);
            Assert.IsTrue(metadata.MmdScope == null || metadata.MmdScope.Count == 0);
            // meta_metadata_name
            MetaMetadataScalarField metadata__meta_metadata_name = (MetaMetadataScalarField)metadata.GetChildMetaMetadata()["meta_metadata_name"];
            Assert.IsNull(metadata__meta_metadata_name.InheritedField);
            Assert.AreSame(metadata, metadata__meta_metadata_name.DeclaringMmd);
            Assert.AreEqual(/*typeof(MetadataStringScalarType).Name*/"MetadataString", metadata__meta_metadata_name.ScalarTypeP.SimplName);
            // mixins
            MetaMetadataCollectionField metadata__mixins = (MetaMetadataCollectionField)metadata.GetChildMetaMetadata()["mixins"];
            Assert.IsNull(metadata__mixins.InheritedField);
            Assert.AreSame(metadata, metadata__mixins.DeclaringMmd);
            Assert.AreSame(metadata, metadata__mixins.InheritedMmd);

            MetaMetadata document = repository.GetMMByName("document");
            Assert.AreSame(metadata, document.InheritedMmd);
            Assert.IsTrue(document.MmdScope == null || document.MmdScope.Count == 0);
            Assert.AreEqual(metadata__meta_metadata_name, document.GetChildMetaMetadata()["meta_metadata_name"]);
            Assert.AreEqual(metadata__mixins, document.GetChildMetaMetadata()["mixins"]);
            // location
            MetaMetadataScalarField document__location = (MetaMetadataScalarField)document.GetChildMetaMetadata()["location"];
            Assert.IsNull(document__location.InheritedField);
            Assert.AreSame(document, document__location.DeclaringMmd);
            // additional_locations
            MetaMetadataCollectionField document__additional_locations = (MetaMetadataCollectionField)document.GetChildMetaMetadata()["additional_locations"];
            Assert.IsNull(document__additional_locations.InheritedField);
            Assert.AreSame(document, document__additional_locations.DeclaringMmd);
            Assert.IsNull(document__additional_locations.InheritedMmd);

            MetaMetadata article = repository.GetMMByName("article");
            MetaMetadata author = article.MmdScope["author"];
            Assert.AreSame(metadata, author.InheritedMmd);
            Assert.AreEqual(metadata__meta_metadata_name, author.GetChildMetaMetadata()["meta_metadata_name"]);
            Assert.AreEqual(metadata__mixins, author.GetChildMetaMetadata()["mixins"]);
            // name
            MetaMetadataScalarField author__name = (MetaMetadataScalarField)author.GetChildMetaMetadata()["name"];
            Assert.IsNull(author__name.InheritedField);
            Assert.AreSame(author, author__name.DeclaringMmd);
            // affiliation
            MetaMetadataScalarField author__affiliation = (MetaMetadataScalarField)author.GetChildMetaMetadata()["affiliation"];
            //Assert.IsNull(author__affiliation.InheritedField);
            Assert.AreSame(author, author__affiliation.DeclaringMmd);

            MetaMetadata source = article.MmdScope["source"];
            Assert.AreSame(document, source.InheritedMmd);
            Assert.AreEqual(metadata__meta_metadata_name, source.GetChildMetaMetadata()["meta_metadata_name"]);
            Assert.AreEqual(metadata__mixins, source.GetChildMetaMetadata()["mixins"]);
            Assert.AreEqual(document__additional_locations, source.GetChildMetaMetadata()["additional_locations"]);
            // archive_name
            MetaMetadataScalarField source__archive_name = (MetaMetadataScalarField)source.GetChildMetaMetadata()["archive_name"];
            Assert.IsNull(source__archive_name.InheritedField);
            Assert.AreSame(source, source__archive_name.DeclaringMmd);
            // location
            MetaMetadataScalarField source__location = (MetaMetadataScalarField)source.GetChildMetaMetadata()["location"];
            Assert.AreSame(document__location, source__location.InheritedField);
            Assert.IsFalse(document__location.Hide);
            Assert.IsTrue(source__location.Hide);
            // year_of_publication
            MetaMetadataScalarField source__year_of_publication = (MetaMetadataScalarField)source.GetChildMetaMetadata()["year_of_publication"];
            Assert.IsNull(source__year_of_publication.InheritedField);
            Assert.AreSame(source, source__year_of_publication.DeclaringMmd);
            // isbn
            MetaMetadataScalarField source__isbn = (MetaMetadataScalarField)source.GetChildMetaMetadata()["isbn"];
            Assert.IsNull(source__isbn.InheritedField);
            Assert.AreSame(source, source__isbn.DeclaringMmd);

            Assert.AreSame(document, article.InheritedMmd);
            Assert.IsTrue(article.MmdScope.Count == 2);
            Assert.AreEqual(metadata__meta_metadata_name, article.GetChildMetaMetadata()["meta_metadata_name"]);
            Assert.AreEqual(metadata__mixins, article.GetChildMetaMetadata()["mixins"]);
            Assert.AreEqual(document__location, article.GetChildMetaMetadata()["location"]);
            Assert.AreEqual(document__additional_locations, article.GetChildMetaMetadata()["additional_locations"]);
            // title
            MetaMetadataScalarField article__title = (MetaMetadataScalarField)article.GetChildMetaMetadata()["title"];
            Assert.IsNull(article__title.InheritedField);
            Assert.AreSame(article, article__title.DeclaringMmd);
            // authors
            MetaMetadataCollectionField article__authors = (MetaMetadataCollectionField)article.GetChildMetaMetadata()["authors"];
            Assert.IsNull(article__authors.InheritedField);
            Assert.AreSame(article, article__authors.DeclaringMmd);
            Assert.AreSame(author, article__authors.InheritedMmd);
            // source
            MetaMetadataCompositeField article__source = (MetaMetadataCompositeField)article.GetChildMetaMetadata()["source"];
            Assert.IsNull(article__source.InheritedField);
            Assert.AreSame(article, article__source.DeclaringMmd);
            Assert.AreSame(source, article__source.InheritedMmd);
            // pages
            MetaMetadataScalarField article__pages = (MetaMetadataScalarField)article.GetChildMetaMetadata()["pages"];
            Assert.IsNull(article__pages.InheritedField);
            Assert.AreSame(article, article__pages.DeclaringMmd);

            MetaMetadata paper = repository.GetMMByName("paper");
            MetaMetadata tag = paper.MmdScope["tag"];
            Assert.AreSame(metadata, tag.InheritedMmd);
            Assert.AreEqual(metadata__meta_metadata_name, tag.GetChildMetaMetadata()["meta_metadata_name"]);
            Assert.AreEqual(metadata__mixins, tag.GetChildMetaMetadata()["mixins"]);
            // tag_name
            MetaMetadataScalarField tag__tag_name = (MetaMetadataScalarField)tag.GetChildMetaMetadata()["tag_name"];
            Assert.IsNull(tag__tag_name.InheritedField);
            Assert.AreSame(tag, tag__tag_name.DeclaringMmd);
            // link
            MetaMetadataScalarField tag__link = (MetaMetadataScalarField)tag.GetChildMetaMetadata()["link"];
            Assert.IsNull(tag__link.InheritedField);
            Assert.AreSame(tag, tag__link.DeclaringMmd);

            Assert.AreSame(article, paper.InheritedMmd);
            Assert.IsTrue(paper.MmdScope.Count == 1);
            Assert.AreEqual(metadata__meta_metadata_name, paper.GetChildMetaMetadata()["meta_metadata_name"]);
            Assert.AreEqual(metadata__mixins, paper.GetChildMetaMetadata()["mixins"]);
            Assert.AreEqual(document__location, paper.GetChildMetaMetadata()["location"]);
            Assert.AreEqual(document__additional_locations, paper.GetChildMetaMetadata()["additional_locations"]);
            Assert.AreEqual(article__title, paper.GetChildMetaMetadata()["title"]);
            // Assert.AreSame(article__authors, paper.GetChildMetaMetadata()["authors"]);
            Assert.AreEqual(article__source, paper.GetChildMetaMetadata()["source"]);
            Assert.AreEqual(article__pages, paper.GetChildMetaMetadata()["pages"]);
            // authors: TODO
            // abstract_field
            MetaMetadataScalarField paper__abstract_field = (MetaMetadataScalarField)paper.GetChildMetaMetadata()["abstract_field"];
            Assert.IsNull(paper__abstract_field.InheritedField);
            Assert.AreSame(paper, paper__abstract_field.DeclaringMmd);
            // references
            MetaMetadataCollectionField paper__references = (MetaMetadataCollectionField)paper.GetChildMetaMetadata()["references"];
            Assert.IsNull(paper__references.InheritedField);
            Assert.AreSame(paper, paper__references.DeclaringMmd);
            Assert.AreSame(paper, paper__references.InheritedMmd);
            // citations
            MetaMetadataCollectionField paper__citations = (MetaMetadataCollectionField)paper.GetChildMetaMetadata()["citations"];
            Assert.IsNull(paper__citations.InheritedField);
            Assert.AreSame(paper, paper__citations.DeclaringMmd);
            Assert.AreSame(paper, paper__citations.InheritedMmd);
            // keywords
            MetaMetadataCollectionField paper__keywords = (MetaMetadataCollectionField)paper.GetChildMetaMetadata()["keywords"];
            Assert.IsNull(paper__keywords.InheritedField);
            Assert.AreSame(paper, paper__keywords.DeclaringMmd);
            Assert.AreEqual(/*typeof(MetadataStringScalarType).Name*/"MetadataString", paper__keywords.ChildScalarType.SimplName);

            MetaMetadata acm_paper = repository.GetMMByName("acm_paper");
            Assert.AreSame(paper, acm_paper.InheritedMmd);
            Assert.IsTrue(acm_paper.MmdScope == null || acm_paper.MmdScope.Count == 0);
            Assert.AreEqual(metadata__meta_metadata_name, acm_paper.GetChildMetaMetadata()["meta_metadata_name"]);
            Assert.AreEqual(metadata__mixins, acm_paper.GetChildMetaMetadata()["mixins"]);
            Assert.AreEqual(document__location, acm_paper.GetChildMetaMetadata()["location"]);
            Assert.AreEqual(document__additional_locations, acm_paper.GetChildMetaMetadata()["additional_locations"]);
            Assert.AreEqual(article__source, acm_paper.GetChildMetaMetadata()["source"]);
            Assert.AreEqual(article__pages, acm_paper.GetChildMetaMetadata()["pages"]);
            Assert.AreEqual(paper__abstract_field, acm_paper.GetChildMetaMetadata()["abstract_field"]);
            Assert.AreEqual(paper__references, acm_paper.GetChildMetaMetadata()["references"]);
            Assert.AreEqual(paper__citations, acm_paper.GetChildMetaMetadata()["citations"]);
            Assert.AreEqual(paper__keywords, acm_paper.GetChildMetaMetadata()["keywords"]);
            // title
            MetaMetadataScalarField acm_paper__title = (MetaMetadataScalarField)acm_paper.GetChildMetaMetadata()["title"];
            Assert.AreEqual(article__title, acm_paper__title.InheritedField);
            Assert.AreSame(article, acm_paper__title.DeclaringMmd);
            // authors
            MetaMetadataCollectionField acm_paper__authors = (MetaMetadataCollectionField)acm_paper.GetChildMetaMetadata()["authors"];
            // Assert.AreSame(article__authors, acm_paper__authors.InheritedField); // should
            // inherit from paper__authors
            Assert.AreSame(article, acm_paper__authors.DeclaringMmd);
            Assert.AreSame(author, acm_paper__authors.InheritedMmd);
            // authors.name
            MetaMetadataScalarField acm_paper__authors__name = (MetaMetadataScalarField)acm_paper__authors.GetChildMetaMetadata()["name"];
            //Assert.AreEqual(author__name, acm_paper__authors__name.InheritedField);
            Assert.AreSame(author, acm_paper__authors__name.DeclaringMmd);
            Assert.AreEqual("location", acm_paper__authors__name.NavigatesTo);
            // authors.affiliation
            MetaMetadataScalarField acm_paper__authors__affiliation = (MetaMetadataScalarField)acm_paper__authors.GetChildMetaMetadata()["affiliation"];
            Assert.AreEqual("./affiliation", acm_paper__authors__affiliation.Xpath);
        }
    }
}
