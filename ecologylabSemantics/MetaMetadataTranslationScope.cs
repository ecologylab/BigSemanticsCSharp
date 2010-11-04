using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ecologylab.semantics.metadata;
using ecologylab.semantics.metametadata;
using ecologylab.net;
using ecologylab.textformat;
using ecologylab.semantics.actions;
using ecologylab.semantics.connectors;
using ecologylab.io;
using ecologylab.serialization;

namespace ecologylabSemantics
{
    public class MetaMetadataTranslationScope
    {
        static Type[] translations = {
            typeof(MetadataClassDescriptor),
            typeof(MetadataFieldDescriptor),
            typeof(MetaMetadataField),
            typeof(MetaMetadataScalarField),
            typeof(MetaMetadataCompositeField),
            typeof(MetaMetadataNestedField),
            typeof(MetaMetadataCollectionField),
            typeof(MetaMetadata),
            typeof(SearchEngines),
            typeof(SearchEngine),
            typeof(UserAgent), 
            typeof(NamedStyle), 
            typeof(SemanticAction),
            typeof(NestedSemanticAction),
            typeof(SemanticsSite),
            typeof(BasicSite),
            typeof(Argument),
            typeof(RegexFilter),
            typeof(CookieProcessing),
            typeof(MetaMetadataRepository), 
            typeof(MetaMetadataSelector),
            typeof(DefVar),
 
            //Condition scope
            typeof(Condition),
            typeof(AndCondition),
            typeof(OrCondition),
            typeof(NotCondition),
        
            typeof(NotNull),
            typeof(Null),
        };

        static Type[] semanticActionClasses = {
            //SemanticAction scope
            typeof(BackOffFromSiteSemanticAction),
            typeof(ChooseSemanticAction),
            //typeof(ChooseSemanticAction.Otherwise), //FIXME: Unsupported
            typeof(CreateAndVisualizeImgSurrogateSemanticAction),
            typeof(CreateAndVisualizeTextSurrogateSemanticAction),
            typeof(CreateSemanticAnchorSemanticAction),
            typeof(EvaluateRankWeight),
            typeof(ForEachSemanticAction),
            typeof(GetFieldSemanticAction),
            typeof(IfSemanticAction),
            typeof(ParseDocumentSemanticAction),
            typeof(SearchSemanticAction),
            typeof(SetFieldSemanticAction),
            typeof(SetMetadataSemanticAction),
        };

        public static TranslationScope get()
        {
            TranslationScope semanticActionScope = TranslationScope.Get("semantic_action_translation_scope", semanticActionClasses);
            TranslationScope mmdScope = TranslationScope.Get("meta_metadata", translations);
            mmdScope.AddTranslations(semanticActionScope);
            return mmdScope;
        }
    }
}
