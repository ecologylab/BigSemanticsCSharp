using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simpl.Serialization;

namespace Ecologylab.Semantics.MetadataNS.Builtins
{
    public class RichArtifactsTypeScope
    {
        public static readonly string Name = "rich_artifacts_scope";

        protected static readonly Type[] Translations =
            {
                typeof (RichArtifact<>),
                typeof (Clipping<>),
                typeof (ImageClipping),
                typeof (TextClipping),
                typeof (VideoClipping),
                typeof (ImageSelfmade),
                typeof (TextSelfmade),
                typeof (WebVideo),
                typeof (HtmlText),
                typeof (Image),
                typeof (Video),
                typeof (Audio)
            };

        public static SimplTypesScope Get()
        {
            return SimplTypesScope.Get(Name, Translations);
        }
    }
}
