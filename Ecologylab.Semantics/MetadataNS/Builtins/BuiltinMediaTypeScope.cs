using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simpl.Serialization;

namespace Ecologylab.Semantics.MetadataNS.Builtins
{
    public class BuiltinMediaTypeScope
    {
        public static readonly string Name = "repository_media";

        protected static readonly Type[] Translations =
            {
                typeof (HtmlText),
                typeof (Image),
                typeof (Video),
                typeof (WebVideo),
            };

        public static SimplTypesScope Get()
        {
            return SimplTypesScope.Get(Name, Translations);
        }
    }
}
