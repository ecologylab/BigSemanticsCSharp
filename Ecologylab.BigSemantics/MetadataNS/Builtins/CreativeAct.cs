using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecologylab.BigSemantics.MetaMetadataNS;
using Ecologylab.BigSemantics.MetadataNS.Builtins.Declarations;
using Ecologylab.BigSemantics.MetadataNS.Scalar;
using Simpl.Serialization.Attributes;

namespace Ecologylab.BigSemantics.MetadataNS.Builtins
{
    [SimplInherit]
    public class CreativeAct : CreativeActDeclaration
    {
        public enum CreativeAction
        {
            CurateClipping = 1,
            CurateLink = 2,
            AnnotateArtifact = 3,
            Note = 4,
            Sketch = 5,
            Upload = 6,
            Edit = 7,
            Aggregate = 10,
        }

        public CreativeAct() { }

        public CreativeAct(MetaMetadataCompositeField mmd) : base(mmd) { }

        public new CreativeAction Action
        {
            get { return (CreativeAction) base.Action.Value; }
            set
            {
                if (base.Action == null)
                    base.Action = new MetadataInteger();
                base.Action.Value = (int) value;
            }

        }
    }
}
