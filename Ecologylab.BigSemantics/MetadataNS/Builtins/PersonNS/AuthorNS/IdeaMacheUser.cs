using Simpl.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ecologylab.BigSemantics.MetadataNS.Builtins.Declarations.PersonNS.AuthorNS;
using Ecologylab.BigSemantics.MetaMetadataNS;

namespace Ecologylab.BigSemantics.MetadataNS.Builtins.PersonNS.AuthorNS
{
    [SimplInherit]
    public class IdeaMacheUser : IdeaMacheUserDeclaration
    {

        public IdeaMacheUser() { }

        public IdeaMacheUser(MetaMetadataCompositeField mmd) : base(mmd) { }

    }
}
