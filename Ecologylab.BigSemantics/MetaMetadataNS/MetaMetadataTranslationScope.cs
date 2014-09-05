using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ecologylab.BigSemantics.Actions;
using Ecologylab.BigSemantics.Connectors;
using Ecologylab.BigSemantics.MetaMetadataNS.IO;
using Ecologylab.BigSemantics.MetaMetadataNS.Net;
using Ecologylab.BigSemantics.MetaMetadataNS.Textformat;
using Ecologylab.BigSemantics.MetadataNS;
using Ecologylab.BigSemantics.MetaMetadataNS;
using Simpl.Serialization;

namespace Ecologylab.BigSemantics.MetaMetadataNS
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
            SimplTypesScope nestedFieldTypes = NestedMetaMetadataFieldTypesScope.Get();
            SimplTypesScope mmdScope = SimplTypesScope.Get("meta_metadata", MetaMetadataFieldTranslationScope.Get(),
                                                         translations);
            mmdScope.AddTranslations(semanticActionScope);
            mmdScope.AddTranslations(conditionScope);
            mmdScope.AddTranslations(nestedFieldTypes);

            return mmdScope;
        }
    }
}
