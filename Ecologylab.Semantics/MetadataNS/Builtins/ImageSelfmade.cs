﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecologylab.Semantics.MetadataNS.Builtins.Declarations;
using Ecologylab.Semantics.MetadataNS.Scalar;
using Simpl.Fundamental.Net;

namespace Ecologylab.Semantics.MetadataNS.Builtins
{
    public class ImageSelfmade : ImageSelfmadeDeclaration
    {

        public static ImageSelfmade ConstructRichArtifact(Image media, Document creator)
        {
            var imageSelfmade = new ImageSelfmade
            {
                Media = media,
                CreativeActs = new List<CreativeAct>(),

            };
            imageSelfmade.CreativeActs.Add(new CreativeAct
            {
                Action = CreativeAct.CreativeAction.CurateClipping,
                Time = new MetadataDate(DateTime.UtcNow),
                Creator = creator
            });

            return imageSelfmade;
        }

        public ParsedUri ImageLocation
        {
            get
            {
                ParsedUri result = null;       // System.Windows.Media.ImageSource || Windows.UI.Xaml.Media.ImageSource
                if (this.Media != null)
                {
                    result = new ParsedUri((this.Media.LocalLocation != null) ? this.Media.LocalLocation.Value.AbsoluteUri : this.Media.Location.Value.AbsoluteUri);
                    //result = SemanticsPlatformSpecifics.Get().CreateNewBitmapImageFromUri(new Uri(uri));
                }
                return result;
            }
        }
    }
}
