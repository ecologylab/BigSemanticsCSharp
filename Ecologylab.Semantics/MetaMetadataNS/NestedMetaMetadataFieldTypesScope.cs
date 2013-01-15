using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simpl.Serialization;

namespace Ecologylab.Semantics.MetaMetadataNS
{
    public class NestedMetaMetadataFieldTypesScope
    {
        public const String Name = "nested_meta_metadata_field_tscope";

	
	    protected static readonly Type[]	Translations	= new Type[] {
		    typeof(MetaMetadataField),
		    typeof(MetaMetadataScalarField),
		    typeof(MetaMetadataCompositeField),
		    typeof(MetaMetadataCollectionField),
            typeof(MmdScope),
	    };

	    public static SimplTypesScope Get()
	    {
		    return SimplTypesScope.Get(Name, Translations);
	    }
    }
}
