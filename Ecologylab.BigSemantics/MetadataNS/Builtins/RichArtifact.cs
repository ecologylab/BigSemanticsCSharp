using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecologylab.Semantics.MetaMetadataNS;
using Ecologylab.Semantics.MetadataNS.Builtins.Declarations;
using Simpl.Fundamental.Net;
using Simpl.Serialization.Attributes;

namespace Ecologylab.Semantics.MetadataNS.Builtins
{
    public class RichArtifact<M> : RichArtifactDeclaration<M>, IRichArtifact<M> where M : Metadata
    {
        [SimplScalar]
        [SimplTag("primary_location")]
        private ParsedUri _primaryLocation;

        public RichArtifact()
        {
            CreativeActs = new List<CreativeAct>();
        }

        public RichArtifact(MetaMetadataCompositeField mmd) : base(mmd)
        {
            CreativeActs = new List<CreativeAct>();
        }
        
    }

    public interface IRichArtifact<out TM> where TM: Metadata
    {
        TM Media { get; }

        List<CreativeAct> CreativeActs { get; set; }
    }

    
}
