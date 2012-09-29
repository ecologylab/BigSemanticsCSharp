using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ecologylab.Semantics.Actions;
using Ecologylab.Semantics.Connectors;
using Ecologylab.Semantics.MetaMetadataNS.IO;
using Ecologylab.Semantics.MetaMetadataNS.Net;
using Ecologylab.Semantics.MetaMetadataNS.Textformat;
using Ecologylab.Semantics.MetadataNS;
using Ecologylab.Semantics.MetaMetadataNS;
using Simpl.Serialization;

namespace Ecologylab.Semantics.MetaMetadataNS
{
    public class MetaMetadataTranslationScope
    {
        static Type[] translations = {
            typeof(MetadataClassDescriptor),
            typeof(MetadataFieldDescriptor),
            typeof(MmdGenericTypeVar),//edit
            typeof(MetaMetadata),
            typeof(SearchEngines),
            typeof(SearchEngine),
            typeof(UserAgent), 
            typeof(NamedStyle), 
            typeof(SemanticOperation),
            typeof(NestedSemanticOperation),
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
          SimplTypesScope semanticActionScope = SemanticOperationTranslationScope.Get();
          SimplTypesScope conditionScope = SimplTypesScope.Get("condition_scope", conditionClasses);
          SimplTypesScope mmdScope = SimplTypesScope.Get("meta_metadata", MetaMetadataFieldTranslationScope.Get(),
                                                         translations);
          mmdScope.AddTranslations(semanticActionScope);
          mmdScope.AddTranslations(conditionScope);
          return mmdScope;
        }
    }
}
