//
// BingSearchImageSection.cs
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
using ecologylab.semantics.generated.library.search;
using ecologylab.semantics.metadata;
using ecologylab.semantics.metadata.builtins;
using ecologylab.semantics.metametadata;

namespace ecologylab.semantics.generated.library.search 
{
	[SimplInherit]
	public class BingSearchImageSection : CompoundDocument
	{
		[SimplCollection("mms:ImageResult")]
		[SimplTag("mms:Results")]
		[MmName("image_search_results")]
		private List<BingImageSearchResult> imageSearchResults;

		public BingSearchImageSection()
		{ }

		public List<BingImageSearchResult> ImageSearchResults
		{
			get{return imageSearchResults;}
			set{imageSearchResults = value;}
		}
	}
}
