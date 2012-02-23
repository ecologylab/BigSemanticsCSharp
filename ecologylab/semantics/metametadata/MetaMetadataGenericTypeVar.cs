using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simpl.Serialization.Attributes;

namespace ecologylab.semantics.metametadata
{
    [SimplTag("generic_type_var")]
    [SimplInherit]
    public class MetaMetadataGenericTypeVar
    {

        [SimplScalar]
        private String name;

        [SimplScalar]
        private String bound;

        [SimplScalar]
        private String parameter;

        [SimplScalar]
        private String genericType;

        [SimplCollection("generic_type_var")]
        [SimplNoWrap]
        private List<MetaMetadataGenericTypeVar> genericTypeVars;

    }
}
