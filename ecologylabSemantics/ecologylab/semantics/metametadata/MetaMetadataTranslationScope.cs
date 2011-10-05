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
using Simpl.Serialization;

namespace ecologylab.semantics.metametadata
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
            typeof(SemanticAction<InfoCollector, SemanticActionHandler>),
            typeof(NestedSemanticAction<InfoCollector, SemanticActionHandler>),
            typeof(SemanticsSite),
            typeof(BasicSite),
            typeof(Argument),
            typeof(RegexFilter),
            typeof(CookieProcessing),
            typeof(MetaMetadataRepository), 
            typeof(MetaMetadataSelector),
            typeof(DefVar)
        };
            //Condition scope
        static Type[] conditionClasses = {
            typeof(Condition),
            typeof(AndCondition),
            typeof(OrCondition),
            typeof(NotCondition),
        
            typeof(NotNull),
            typeof(Null)
        };

        static Type[] semanticActionClasses = {
            //SemanticAction scope
            typeof(BackOffFromSiteSemanticAction<InfoCollector, SemanticActionHandler>),
            typeof(ChooseSemanticAction<InfoCollector, SemanticActionHandler>),
            //typeof(ChooseSemanticAction.Otherwise), //FIXME: Unsupported
            typeof(CreateAndVisualizeImgSurrogateSemanticAction<InfoCollector, SemanticActionHandler>),
            typeof(CreateAndVisualizeTextSurrogateSemanticAction<InfoCollector, SemanticActionHandler>),
            typeof(CreateSemanticAnchorSemanticAction<InfoCollector, SemanticActionHandler>),
            typeof(EvaluateRankWeight<InfoCollector, SemanticActionHandler>),
            typeof(ForEachSemanticAction<InfoCollector, SemanticActionHandler>),
            typeof(GetFieldSemanticAction<InfoCollector, SemanticActionHandler>),
            typeof(IfSemanticAction<InfoCollector, SemanticActionHandler>),
            typeof(ParseDocumentSemanticAction<InfoCollector, SemanticActionHandler>),
            typeof(SearchSemanticAction<InfoCollector, SemanticActionHandler>),
            typeof(SetFieldSemanticAction<InfoCollector, SemanticActionHandler>),
            typeof(SetMetadataSemanticAction<InfoCollector, SemanticActionHandler>),
            typeof(SemanticAction<InfoCollector, SemanticActionHandler>)
        };

        public static TranslationScope get()
        {
            TranslationScope semanticActionScope    = TranslationScope.Get("semantic_action_translation_scope", semanticActionClasses);
            TranslationScope conditionScope         = TranslationScope.Get("condition_scope", conditionClasses);
            TranslationScope mmdScope               = TranslationScope.Get("meta_metadata", translations);
            mmdScope.AddTranslations(semanticActionScope);
            mmdScope.AddTranslations(conditionScope);
            return mmdScope;
        }
    }
}
