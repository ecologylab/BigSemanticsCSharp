//
// ScienceDirectArticle.cs
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
using ecologylab.semantics.generated.library.scholarlyPublication;
using ecologylab.semantics.metadata.builtins;
using ecologylab.semantics.metadata.scalar;
using ecologylab.semantics.metametadata;

namespace ecologylab.semantics.generated.library.scienceDirect 
{
	/// <summary>
	/// Information about the article
	/// </summary>
	[SimplInherit]
	public class ScienceDirectArticle : ScholarlyArticle
	{
		/// <summary>
		/// The journal or other publication that the article comes from
		/// </summary>
		[SimplScalar]
		private MetadataString publicationName;

		[SimplScalar]
		private MetadataString volume;

		[SimplScalar]
		private MetadataString issue;

		/// <summary>
		/// The digital object identifier of the article
		/// </summary>
		[SimplScalar]
		private MetadataString doi;

		public ScienceDirectArticle()
		{ }

		public MetadataString PublicationName
		{
			get{return publicationName;}
			set{publicationName = value;}
		}

		public MetadataString Volume
		{
			get{return volume;}
			set{volume = value;}
		}

		public MetadataString Issue
		{
			get{return issue;}
			set{issue = value;}
		}

		public MetadataString Doi
		{
			get{return doi;}
			set{doi = value;}
		}
	}
}
