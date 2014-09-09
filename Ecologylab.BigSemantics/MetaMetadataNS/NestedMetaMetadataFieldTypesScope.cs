using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simpl.Serialization;

namespace Ecologylab.BigSemantics.MetaMetadataNS
{
    public class NestedMetaMetadataFieldTypesScope
    {
        public const String Name = "nested_meta_metadata_field_tscope";

	
	    protected static readonly Type[]	Translations	= new Type[] {
		    typeof(MetaMetadataField),
		    typeof(MetaMetadataScalarField),
		    typeof(MetaMetadataNestedField),
		    typeof(MetaMetadataCompositeField),
		    typeof(MetaMetadataCollectionField),
		    typeof(MetaMetadata),
            typeof(MmdScope),
	    };

	    public static SimplTypesScope Get()
	    {
		    return SimplTypesScope.Get(Name, Translations);
	    }
    }
}
