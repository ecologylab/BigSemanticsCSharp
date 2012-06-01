using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ecologylab.semantics.metadata;
using Simpl.Serialization;
using ecologylab.semantics.metadata.builtins.declarations;
using ecologylab.semantics.metadata.scalar.types;

namespace ecologylab.semantics.metadata.builtins 
{
    public class MetadataBuiltinsTypesScope
    {
        public static string NAME = "metadata_builtin_translations";

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
            return SimplTypesScope.Get(NAME, MetadataBuiltinDeclarationsTranslationScope.Get(), Translations);
        }
    }
}
