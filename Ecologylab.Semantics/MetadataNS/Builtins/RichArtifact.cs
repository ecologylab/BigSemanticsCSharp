using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecologylab.Semantics.MetaMetadataNS;
using Ecologylab.Semantics.MetadataNS.Builtins.Declarations;

namespace Ecologylab.Semantics.MetadataNS.Builtins
{
    public class RichArtifact<M> : RichArtifactDeclaration<M> where M : Metadata
    {
        public RichArtifact()
        {
            CreativeActs = new List<CreativeAct>();
        }

        public RichArtifact(MetaMetadataCompositeField mmd) : base(mmd)
        {
            CreativeActs = new List<CreativeAct>();
        }

    }
}
