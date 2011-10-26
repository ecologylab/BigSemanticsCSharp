//
// BestsellerList.cs
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
using ecologylab.semantics.metadata.builtins;
using ecologylab.semantics.metadata.scalar;
using ecologylab.semantics.metametadata;

namespace ecologylab.semantics.generated.library.products 
{
	[SimplInherit]
	public class BestsellerList : Document
	{
		[SimplScalar]
		private MetadataString rank;

		public BestsellerList()
		{ }

		public MetadataString Rank
		{
			get{return rank;}
			set{rank = value;}
		}
	}
}
