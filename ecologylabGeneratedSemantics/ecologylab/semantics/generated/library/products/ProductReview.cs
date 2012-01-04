//
// ProductReview.cs
// s.im.pl serialization
//
// Generated by MetaMetadataDotNetTranslator on 01/04/12.
// Copyright 2012 Interface Ecology Lab. 
//


using Simpl.Fundamental.Generic;
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

namespace ecologylab.semantics.generated.library.products 
{
	[SimplInherit]
	public class ProductReview : Metadata
	{
		[SimplScalar]
		private MetadataString content;

		[SimplScalar]
		private MetadataString rating;

		public ProductReview()
		{ }

		public ProductReview(MetaMetadataCompositeField mmd) : base(mmd) { }


		public MetadataString Content
		{
			get{return content;}
			set
			{
				if (this.content != value)
				{
					this.content = value;
					this.RaisePropertyChanged( () => this.Content );
				}
			}
		}

		public MetadataString Rating
		{
			get{return rating;}
			set
			{
				if (this.rating != value)
				{
					this.rating = value;
					this.RaisePropertyChanged( () => this.Rating );
				}
			}
		}
	}
}
