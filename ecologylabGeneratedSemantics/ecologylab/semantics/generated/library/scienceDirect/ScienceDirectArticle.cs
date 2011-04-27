//
//  ScienceDirectArticle.cs
//  s.im.pl serialization
//
//  Generated by DotNetTranslator on 02/02/11.
//  Copyright 2011 Interface Ecology Lab. 
//

using System;
using System.Collections.Generic;
using ecologylab.attributes;
using ecologylab.semantics.metadata.scalar;
using ecologylab.semantics.metadata;
using ecologylab.semantics.generated.library.scholarlyPublication;

namespace ecologylab.semantics.generated.library.scienceDirect 
{
	/// <summary>
	/// Information about the article
	/// This is a generated code. DO NOT edit or modify it.
	/// @author MetadataCompiler
	/// </summary>
	[simpl_descriptor_classes(new Type[] { typeof(MetadataClassDescriptor), typeof(MetadataFieldDescriptor) })]
	[simpl_inherit]
	public class ScienceDirectArticle : ScholarlyArticle
	{
		/// <summary>
		/// The journal or other publication that the article comes from
		/// </summary>
		[simpl_scalar]
		private MetadataString publicationName;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[simpl_scalar]
		private MetadataString volume;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[simpl_scalar]
		private MetadataString issue;

		/// <summary>
		/// The digital object identifier of the article
		/// </summary>
		[simpl_scalar]
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