using Simpl.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ecologylab.BigSemantics.MetadataNS.Builtins.Declarations.CreativeWorkNS;
using Ecologylab.BigSemantics.MetaMetadataNS;

namespace Ecologylab.BigSemantics.MetadataNS.Builtins.CreativeWorkNS
{
    [SimplInherit]
    public class Curation : CurationDeclaration
    {

        public Curation() { }

        public Curation(MetaMetadataCompositeField mmd) : base(mmd) { }

    }
}
