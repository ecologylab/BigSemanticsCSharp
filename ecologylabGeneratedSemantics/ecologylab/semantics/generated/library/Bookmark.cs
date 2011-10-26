//
// Bookmark.cs
// s.im.pl serialization
//
// Generated by MetaMetadataDotNetTranslator on 10/26/11.
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
using ecologylab.semantics.metadata.builtins;
using ecologylab.semantics.metadata.scalar;
using ecologylab.semantics.metametadata;

namespace ecologylab.semantics.generated.library 
{
	[SimplInherit]
	public class Bookmark : Metadata
	{
		[SimplScalar]
		[SimplCompositeAsScalar]
		private MetadataString title;

		[SimplScalar]
		private MetadataParsedURL link;

		[SimplScalar]
		private MetadataParsedURL pic;

		public Bookmark()
		{ }

		public MetadataString Title
		{
			get{return title;}
			set{title = value;}
		}

		public MetadataParsedURL Link
		{
			get{return link;}
			set{link = value;}
		}

		public MetadataParsedURL Pic
		{
			get{return pic;}
			set{pic = value;}
		}
	}
}
