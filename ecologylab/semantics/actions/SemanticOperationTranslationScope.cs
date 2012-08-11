using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simpl.Serialization;

namespace ecologylab.semantics.actions
{
    public class SemanticOperationTranslationScope
    {
        public const string ScopeName = "semantic_action_translation_scope";

        private static Type[] semanticActionClasses = {
            //SemanticAction scope
            typeof(BackOffFromSiteSemanticOperation),
            typeof(ChooseSemanticOperation),
            typeof(Otherwise),
            typeof(CreateAndVisualizeImgSurrogateSemanticOperation),
            typeof(CreateAndVisualizeTextSurrogateSemanticOperation),
            typeof(CreateSemanticAnchorSemanticOperation),
            typeof(EvaluateRankWeight),
            typeof(ForEachSemanticOperation),
            typeof(GetFieldSemanticOperation),
            typeof(IfSemanticOperation),
            typeof(ParseDocumentSemanticOperation),
            typeof(SearchSemanticOperation),
            typeof(SetFieldSemanticOperation),
            typeof(SetMetadataSemanticOperation),
            typeof(SemanticOperation)
        };

        public static SimplTypesScope Get()
        {
            return SimplTypesScope.Get(ScopeName, semanticActionClasses);
        }
    }
}
