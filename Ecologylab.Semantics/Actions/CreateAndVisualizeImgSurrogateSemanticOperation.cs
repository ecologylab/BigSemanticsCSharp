//
//  CreateAndVisualizeImgSurrogateSemanticAction.cs
//  s.im.pl serialization
//
//  Generated by DotNetTranslator on 11/16/10.
//  Copyright 2010 Interface Ecology Lab. 
//

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Ecologylab.Semantics.Collecting;
using Ecologylab.Semantics.MetadataNS.Builtins;
using Ecologylab.Semantics.MetaMetadataNS;
using Simpl.Fundamental.Net;
using Simpl.Serialization.Attributes;
using Ecologylab.Semantics.Actions;

namespace Ecologylab.Semantics.Actions 
{
	/// <summary>
	/// missing java doc comments or could not find the source file.
	/// </summary>
	[SimplInherit]
	[SimplTag("create_and_visualize_img_surrogate")]
    public class CreateAndVisualizeImgSurrogateSemanticOperation : SemanticOperation 
	{
        public CreateAndVisualizeImgSurrogateSemanticOperation()
		{ }
	    
	    public override String GetOperationName()
	    {
		    return SemanticOperationStandardMethods.CreateAndVisualizeImgSurrogate;
	    }

	    public override void HandleError()
	    {
	    }

/*        public override Object Perform(Object obj)
        {
            return null;
        }
*/
	    public override Object Perform(Object obj)
	    {
		    Document source	= ResolveSourceDocument();

		    Image image = (Image) GetArgumentObject(SemanticOperationNamedArguments.Metadata);
		    if (image == null)
		    {
			    ParsedUri imagePURL = (ParsedUri) GetArgumentObject(SemanticOperationNamedArguments.ImagePurl);
/*			    if (imagePURL != null)
			    {
				    image								= sessionScope.GetOrConstructImage(imagePURL);
				
				    //TODO -- if it already exists: (1) do we need to download??
				    //															(2) should we merge metadata
			    }
*/
		    }
		    else
		    {
			    //TODO add to global collections?! if already there merge!
		    }
		    if (image != null && image.Location != null)
		    {
			    image.SemanticsSessionScope = sessionScope as SemanticsSessionScope;

			    Document mixin = (Document) GetArgumentObject(SemanticOperationNamedArguments.Mixin);
			    if (mixin != null)
				    image.AddMixin(mixin);
			
			    Object captionObject = GetArgumentObject(SemanticOperationNamedArguments.Caption);
                String caption = (captionObject != null) ? captionObject.ToString() : null;

                int width  		 		 		= GetArgumentInteger(SemanticOperationNamedArguments.Width, 0);
			    int height  		 		 	= GetArgumentInteger(SemanticOperationNamedArguments.Height, 0);
			
			    ParsedUri hrefPURL 		= (ParsedUri) GetArgumentObject(SemanticOperationNamedArguments.Href);
			    Document outlink 			= (Document) GetArgumentObject(SemanticOperationNamedArguments.HrefMetadata);
			    if (hrefPURL != null & outlink == null)
				    outlink				= sessionScope.GetOrConstructDocument(hrefPURL);
			
			    ImageClipping imageClipping	= image.ConstructClipping(source, outlink, caption, null);
                source.AddClipping(imageClipping);

		        DocumentClosure imageClosure;
                
                if (this.sessionScope is SemanticsSessionScope &&
                    (this.sessionScope as SemanticsSessionScope).MetadataServicesClient != null)
                {
                    imageClosure = image.GetOrConstructClosure();

                    if (documentParser != null)
                    {
                    }
                    else
                    {
                        //imageClosure.RequestMetadata();
                    }
                }
                else
		        {
		            image.GetOrConstructClosure();
		        }

			    return image;
		    }
		    else
		    {
			    MetaMetadata mm	= GetMetaMetadata();
			    String mmString	= mm != null ? mm.Name : "Couldn't getMetaMetadata()";
			    Debug.WriteLine("Can't createAndVisualizeImgSurrogate because null PURL: " + mmString
					    + " - " + source.Location);
		    }

		    return null;
	    }

	}
}
