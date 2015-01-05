using Simpl.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ecologylab.BigSemantics.MetadataNS.Builtins.Declarations.CommentNS;
using Ecologylab.BigSemantics.MetaMetadataNS;

namespace Ecologylab.BigSemantics.MetadataNS.Builtins.CommentNS
{
    [SimplInherit]
    public class Comment : CommentDeclaration
    {

        public Comment() { }

        public Comment(MetaMetadataCompositeField mmd) : base(mmd) { }

    }
}
