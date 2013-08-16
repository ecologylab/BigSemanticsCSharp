using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ecologylab.Semantics.MetadataNS.Builtins.Declarations;
using Ecologylab.Semantics.MetadataNS;
using Ecologylab.Semantics.MetadataNS.Scalar.Types;
using Simpl.Serialization;

namespace Ecologylab.Semantics.MetadataNS.Builtins 
{
    public class MetadataBuiltinsTypesScope
    {
        public static readonly string Name = "metadata_builtin_translations";

        public static readonly SimplTypesScope[] InheritedScopes =
            new[] 
            { 
                MetadataBuiltinDeclarationsTranslationScope.Get(), 
                BuiltinMediaTypeScope.Get(),
                CreativeActsTypesScope.Get()
            };

        protected static Type[] Translations =
        {
            typeof (Metadata),
            typeof (TextSelfmade),
            typeof (ClippableDocument),
            typeof (Clipping<>),
            typeof (CompoundDocument),
            typeof (DebugMetadata),
            typeof (Document),
            typeof (DocumentMetadataWrap),
            typeof (ImageClipping),
            typeof (TextClipping),
            typeof (MetadataCollection),
            typeof (RichArtifact<>),
            typeof (ImageSelfmade),
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
