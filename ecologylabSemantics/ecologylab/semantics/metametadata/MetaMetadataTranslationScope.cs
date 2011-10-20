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
using ecologylabSemantics.ecologylab.semantics.metametadata;

namespace ecologylab.semantics.metametadata
{
    public class MetaMetadataTranslationScope
    {
        static Type[] translations = {
            typeof(MetadataClassDescriptor),
            typeof(MetadataFieldDescriptor),
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



        public static SimplTypesScope Get()
        {
          SimplTypesScope semanticActionScope = SemanticActionTranslationScope.Get();
          SimplTypesScope conditionScope = SimplTypesScope.Get("condition_scope", conditionClasses);
          SimplTypesScope mmdScope = SimplTypesScope.Get("meta_metadata", MetaMetadataFieldTranslationScope.Get(),
                                                         translations);
          mmdScope.AddTranslations(semanticActionScope);
          mmdScope.AddTranslations(conditionScope);
          return mmdScope;
        }
    }
}
