//
// ImageClippingDeclaration.cs
// s.im.pl serialization
//
// Generated by MetaMetadataDotNetTranslator.
// Copyright 2015 Interface Ecology Lab. 
//


using Ecologylab.BigSemantics.MetaMetadataNS;
using Ecologylab.BigSemantics.MetadataNS.Builtins;
using Ecologylab.Collections;
using Simpl.Fundamental.Generic;
using Simpl.Serialization;
using Simpl.Serialization.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Ecologylab.BigSemantics.MetadataNS.Builtins.Declarations 
{
	[SimplInherit]
	public class ImageClippingDeclaration : Clipping<Image>
	{
		public ImageClippingDeclaration()
		{ }

		public ImageClippingDeclaration(MetaMetadataCompositeField mmd) : base(mmd) { }

	}
}
