using Ecologylab.BigSemantics.MetaMetadataNS;
using Ecologylab.BigSemantics.MetadataNS.Builtins.Declarations;
using Ecologylab.BigSemantics.PlatformSpecifics;
using Simpl.Fundamental.Generic;
using Simpl.Fundamental.Net;
using Simpl.Serialization;
using Simpl.Serialization.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using Ecologylab.Collections;
using Ecologylab.BigSemantics.MetadataNS;
using Ecologylab.BigSemantics.MetadataNS.Scalar;
using Ecologylab.BigSemantics.MetadataNS.Builtins.PersonNS.AuthorNS;

namespace Ecologylab.BigSemantics.MetadataNS.Builtins 
{
	[SimplInherit]
	public class ImageClipping : ImageClippingDeclaration
	{
		public ImageClipping() { }

		public ImageClipping(MetaMetadataCompositeField mmd) : base(mmd) { }

        public ImageClipping(MetaMetadataCompositeField metaMetadata, Image clippedMedia, Document source, Document outlink, String caption, String context) : this(metaMetadata)
	    {
		    InitMediaClipping(this, clippedMedia, source, outlink, caption, context);
	    }

        public override bool IsImage
        {
            get
            {
                return true;
            }
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

	    public static ImageClipping ConstructClipping(ParsedUri location, Document sourceDoc, IdeaMacheUser creator)
	    {
            var imageClipping = new ImageClipping
            {
                Media = new Image
                {
                    Location = new MetadataParsedURL(location)
                },
                SourceDoc = sourceDoc,
                CreativeActs = new List<CreativeAct>(),

            };
            imageClipping.CreativeActs.Add(new CreativeAct
            {
                Action = CreativeAct.CreativeAction.CurateClipping,
                Time = new MetadataDate(DateTime.UtcNow),
                Creator = creator
            });

	        return imageClipping;
	    }


	}
}
