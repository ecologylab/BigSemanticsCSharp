using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simpl.Serialization.Attributes;

namespace Ecologylab.BigSemantics.MetadataNS.Builtins
{
    [SimplInherit]
    public class FrameSelfmade : RichArtifact<PresentationFrame>
    {
        [SimplScalar]
        private float frameNumber;

        public float FrameNumber
        {
            get { return frameNumber; }
            set { frameNumber = value; }
        }
    }
}
