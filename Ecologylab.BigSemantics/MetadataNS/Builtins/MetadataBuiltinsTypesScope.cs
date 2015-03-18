using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ecologylab.BigSemantics.MetadataNS.Builtins.Declarations;
using Ecologylab.BigSemantics.MetadataNS;
using Ecologylab.BigSemantics.MetadataNS.Scalar.Types;
using Simpl.Serialization;

namespace Ecologylab.BigSemantics.MetadataNS.Builtins 
{
    public class MetadataBuiltinsTypesScope
    {
        public static readonly string Name = "metadata_builtin_translations";

        public static readonly SimplTypesScope[] InheritedScopes =
            new[] 
            { 
                MetadataBuiltinDeclarationsTranslationScope.Get(), 
                CreativeActsTypesScope.Get(),

            };

        protected static Type[] Translations =
        {
            typeof (Metadata),
            typeof (ClippableDocument),
            typeof (RichDocument),
            typeof (DebugMetadata),
            typeof (Document),
            typeof (DocumentMetadataWrap),
            typeof (MetadataCollection),
            typeof (Annotate),
            typeof (Audio),
            typeof (HtmlText),
            typeof (Image),
            typeof (SequencedClippableDocument),
            typeof (Video),
            typeof (ImageClipping),
            typeof (ImageSelfmade),
            typeof (TextClipping),
            typeof (TextSelfmade),
            typeof (FrameSelfmade),
            typeof (PresentationFrame),
            typeof (AggregateMedia),
            typeof (AggregateSelfmade)
        };

        static MetadataBuiltinsTypesScope()
        {
            MetadataScalarType.init();
        }

        public static SimplTypesScope Get()
        {
            return SimplTypesScope.Get(Name, InheritedScopes, Translations);
        }
    }
}
