using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecologylab.Semantics.MetaMetadataNS;
using Ecologylab.Semantics.MetadataNS.Builtins.Declarations;

namespace Ecologylab.Semantics.MetadataNS.Builtins
{
    public class Video : VideoDeclaration
    {
        public Video()
		{ }

        public Video(MetaMetadataCompositeField mmd) : base(mmd) 
        { }
    }
}
