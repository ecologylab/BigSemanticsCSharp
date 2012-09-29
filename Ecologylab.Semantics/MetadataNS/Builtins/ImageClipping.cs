using Ecologylab.Semantics.MetaMetadataNS;
using Ecologylab.Semantics.MetadataNS.Builtins.Declarations;
using Ecologylab.Semantics.PlatformSpecifics;
using Simpl.Fundamental.Generic;
using Simpl.Serialization;
using Simpl.Serialization.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using Ecologylab.Collections;
using Ecologylab.Semantics.MetadataNS;
using Ecologylab.Semantics.MetadataNS.Scalar;

namespace Ecologylab.Semantics.MetadataNS.Builtins 
{
	[SimplInherit]
	public class ImageClipping : ImageClippingDeclaration
	{

		public ImageClipping() { }

		public ImageClipping(MetaMetadataCompositeField mmd) : base(mmd) { }

        public ImageClipping(MetaMetadataCompositeField metaMetadata, Image clippedMedia, Document source, Document outlink, String caption, String context) : this(metaMetadata)
	    {
		    MediaClipping<Image>.InitMediaClipping(this, clippedMedia, source, outlink, caption, context);
	    }

        public override bool IsImage
        {
            get
            {
                return true;
            }
        }

        public object ImageLocation
        {
            get
            {
                object result = null;       // System.Windows.Media.ImageSource || Windows.UI.Xaml.Media.ImageSource
                if (this.Media != null)
                {
                    String uri = (this.Media.LocalLocation != null) ? this.Media.LocalLocation.Value.AbsoluteUri : this.Media.Location.Value.AbsoluteUri;
                    result = SemanticsPlatformSpecifics.Get().CreateNewBitmapImageFromUri(new Uri(uri));
                }
                return result;
            }
        }
	}
}
