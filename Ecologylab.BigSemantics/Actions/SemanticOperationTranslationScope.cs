using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simpl.Serialization;
using Ecologylab.BigSemantics.Actions;

namespace Ecologylab.BigSemantics.Actions
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
            typeof(SemanticOperation),
            typeof(FilterLocation),
            typeof(SetParam),
            typeof(StripParam),
            typeof(ParamOp)
        };

        public static SimplTypesScope Get()
        {
            return SimplTypesScope.Get(ScopeName, semanticActionClasses);
        }
    }
}
