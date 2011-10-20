//
// Thumbinner.cs
// s.im.pl serialization
//
// Generated by MetaMetadataDotNetTranslator on 10/11/11.
// Copyright 2011 Interface Ecology Lab. 
//


using Simpl.Fundamental.Generic;
using Simpl.Fundamental.Net;
using Simpl.Serialization;
using Simpl.Serialization.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using ecologylab.collections;
using ecologylab.semantics.metadata;
using ecologylab.semantics.metadata.scalar;
using ecologylab.semantics.metametadata;

namespace ecologylab.semantics.generated.library.wikipedia 
{
	[SimplInherit]
	public class Thumbinner : Metadata
	{
		[SimplScalar]
		private MetadataString thumbImgCaption;

		[SimplScalar]
		private MetadataParsedURL thumbImgSrc;

		public Thumbinner()
		{ }

		public MetadataString ThumbImgCaption
		{
			get{return thumbImgCaption;}
			set{thumbImgCaption = value;}
		}

		public MetadataParsedURL ThumbImgSrc
		{
			get{return thumbImgSrc;}
			set{thumbImgSrc = value;}
		}
	}
}