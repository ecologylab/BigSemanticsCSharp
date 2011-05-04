//
//  BirthDetail.cs
//  s.im.pl serialization
//
//  Generated by DotNetTranslator on 04/01/11.
//  Copyright 2011 Interface Ecology Lab. 
//

using System;
using System.Collections.Generic;
using ecologylab.attributes;
using ecologylab.semantics.metadata.scalar;
using ecologylab.semantics.metadata;
using ecologylab.semantics.metadata.builtins;

namespace ecologylab.semantics.generated.library.imdb 
{
	/// <summary>
	/// Metadata for storing details of birth (date and place) of people
	/// This is a generated code. DO NOT edit or modify it.
	/// @author MetadataCompiler
	/// </summary>
	[simpl_descriptor_classes(new Type[] { typeof(MetadataClassDescriptor), typeof(MetadataFieldDescriptor) })]
	[simpl_inherit]
	public class BirthDetail : Document
	{
		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[simpl_scalar]
		private MetadataString dayOfBirth;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[simpl_scalar]
		private MetadataString yearOfBirth;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[simpl_scalar]
		private MetadataString placeOfBirth;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[simpl_scalar]
		private MetadataParsedURL dayOfBirthLink;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[simpl_scalar]
		private MetadataParsedURL yearOfBirthLink;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[simpl_scalar]
		private MetadataParsedURL placeOfBirthLink;

		public BirthDetail()
		{ }

		public MetadataString DayOfBirth
		{
			get{return dayOfBirth;}
			set{dayOfBirth = value;}
		}

		public MetadataString YearOfBirth
		{
			get{return yearOfBirth;}
			set{yearOfBirth = value;}
		}

		public MetadataString PlaceOfBirth
		{
			get{return placeOfBirth;}
			set{placeOfBirth = value;}
		}

		public MetadataParsedURL DayOfBirthLink
		{
			get{return dayOfBirthLink;}
			set{dayOfBirthLink = value;}
		}

		public MetadataParsedURL YearOfBirthLink
		{
			get{return yearOfBirthLink;}
			set{yearOfBirthLink = value;}
		}

		public MetadataParsedURL PlaceOfBirthLink
		{
			get{return placeOfBirthLink;}
			set{placeOfBirthLink = value;}
		}
	}
}
