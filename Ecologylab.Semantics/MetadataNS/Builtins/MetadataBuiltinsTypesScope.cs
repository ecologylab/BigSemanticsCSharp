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
        public static string Name = "metadata_builtin_translations";

        protected static Type[] Translations =
        {
            typeof (Metadata),
            typeof (Annotation),
            typeof (ClippableDocument<>),
            typeof (Clipping),
            typeof (CompoundDocument),
            typeof (DebugMetadata),
            typeof (Document),
            typeof (DocumentMetadataWrap),
            typeof (Image),
            typeof (ImageClipping),
            typeof (MediaClipping<>),
            typeof (TextClipping),
            typeof (MetadataCollection),
        };

        static MetadataBuiltinsTypesScope()
        {
            MetadataScalarType.init();
        }

        public static SimplTypesScope Get()
        {
            return SimplTypesScope.Get(Name, MetadataBuiltinDeclarationsTranslationScope.Get(), Translations);
        }
    }
}
