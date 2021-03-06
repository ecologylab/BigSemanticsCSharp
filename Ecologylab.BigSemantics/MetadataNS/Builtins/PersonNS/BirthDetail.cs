﻿using Simpl.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ecologylab.BigSemantics.MetadataNS.Builtins.Declarations.PersonNS;
using Ecologylab.BigSemantics.MetaMetadataNS;

namespace Ecologylab.BigSemantics.MetadataNS.Builtins.PersonNS
{
    [SimplInherit]
    public class BirthDetail : BirthDetailDeclaration
    {

        public BirthDetail() { }

        public BirthDetail(MetaMetadataCompositeField mmd) : base(mmd) { }

    }
}
