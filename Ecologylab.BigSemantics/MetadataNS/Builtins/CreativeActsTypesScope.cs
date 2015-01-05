using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simpl.Serialization;

namespace Ecologylab.BigSemantics.MetadataNS.Builtins
{
    public class CreativeActsTypesScope
    {
        public static readonly string Name = "creative_acts_scope";

        protected static readonly Type[] Translations =
            {
                typeof (CreativeAct),
                typeof (CurateLink),
                typeof (AssignPrimaryLink),
                typeof (Annotate),
            };

        public static SimplTypesScope Get()
        {
            return SimplTypesScope.Get(Name, Translations);
        }
    }
}
