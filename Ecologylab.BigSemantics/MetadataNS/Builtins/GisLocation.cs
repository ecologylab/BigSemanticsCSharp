using Simpl.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ecologylab.BigSemantics.MetadataNS.Builtins.Declarations;
using Ecologylab.BigSemantics.MetaMetadataNS;

namespace Ecologylab.BigSemantics.MetadataNS.Builtins
{
    [SimplInherit]
    public class GisLocation : GisLocationDeclaration
    {

        public GisLocation() { }

        public GisLocation(MetaMetadataCompositeField mmd) : base(mmd) { }

    }
}
