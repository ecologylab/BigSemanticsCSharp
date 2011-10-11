using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ecologylab.semantics.metadata;
using Simpl.Serialization;

namespace ecologylab.semantics.metadata.builtins
{
  public class MetadataBuiltinsTranslationScope
  {
    public static string NAME = "metadata_builtin_translations";

    protected static Type[] Translations =
    {
      typeof(Metadata),
      typeof(Document),
      typeof(CompoundDocument),
      typeof(ClippableDocument),
      typeof(ClippableMetadata),
      typeof(Clipping),
      typeof(DebugMetadata),
      typeof(Image),
      typeof(Text),
    };

    public static TranslationScope Get()
    {
      return TranslationScope.Get(NAME, Translations);
    }
  }
}
