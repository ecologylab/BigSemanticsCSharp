//
// FlickrTagsInteresting.cs
// s.im.pl serialization
//
// Generated by MetaMetadataDotNetTranslator on 10/26/11.
// Copyright 2011 Interface Ecology Lab. 
//


using Simpl.Fundamental.Generic;
using Simpl.Serialization;
using Simpl.Serialization.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using ecologylab.collections;
using ecologylab.semantics.generated.library.flickr;
using ecologylab.semantics.generated.library.search;
using ecologylab.semantics.metadata;
using ecologylab.semantics.metadata.builtins;
using ecologylab.semantics.metametadata;

namespace ecologylab.semantics.generated.library.flickr 
{
	/// <summary>
	/// Used for flickr search results
	/// </summary>
	[SimplInherit]
	public class FlickrTagsInteresting : Search
	{
		/// <summary>
		/// Collection of all images of a user
		/// </summary>
		[SimplCollection("flickr_link")]
		[MmName("flickr_link_set")]
		private List<FlickrLink> flickrLinkSet;

		public FlickrTagsInteresting()
		{ }

		public List<FlickrLink> FlickrLinkSet
		{
			get{return flickrLinkSet;}
			set{flickrLinkSet = value;}
		}
	}
}
