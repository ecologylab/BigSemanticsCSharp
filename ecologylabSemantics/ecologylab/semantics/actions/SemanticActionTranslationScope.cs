using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simpl.Serialization;

namespace ecologylab.semantics.actions
{
    public class SemanticActionTranslationScope
    {
        public const string ScopeName = "semantic_action_translation_scope";

        private static Type[] semanticActionClasses = {
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
            typeof(SemanticAction)
        };

        public static SimplTypesScope Get()
        {
            return SimplTypesScope.Get(ScopeName, semanticActionClasses);
        }
    }
}
