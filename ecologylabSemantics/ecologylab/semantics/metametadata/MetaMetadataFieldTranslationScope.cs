using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simpl.Serialization;
using ecologylab.semantics.metametadata;

namespace ecologylabSemantics.ecologylab.semantics.metametadata
{
  internal class MetaMetadataFieldTranslationScope
  {

    public const string NAME = "meta-metadata-field-tscope";

    private static Type[] translations =
      {
        typeof (MetaMetadataField),
        typeof (MetaMetadataScalarField),
        typeof (MetaMetadataNestedField),
        typeof (MetaMetadataCompositeField),
        typeof (MetaMetadataCollectionField)
      };

    public static SimplTypesScope Get()
    {
      return SimplTypesScope.Get(NAME, translations);
    }

  }
}
