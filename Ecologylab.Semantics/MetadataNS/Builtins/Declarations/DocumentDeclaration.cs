//
// DocumentDeclaration.cs
// s.im.pl serialization
//
// Generated by MetaMetadataDotNetTranslator.
// Copyright 2012 Interface Ecology Lab. 
//


using Ecologylab.Collections;
using Ecologylab.Semantics.MetaMetadataNS;
using Ecologylab.Semantics.MetadataNS;
using Ecologylab.Semantics.MetadataNS.Builtins;
using Ecologylab.Semantics.MetadataNS.Scalar;
using Simpl.Fundamental.Generic;
using Simpl.Fundamental.Net;
using Simpl.Serialization;
using Simpl.Serialization.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Ecologylab.Semantics.MetadataNS.Builtins.Declarations 
{
	/// <summary>
	/// The Document Class
	/// </summary>
	[SimplInherit]
	public class DocumentDeclaration : Metadata
	{
		/// <summary>
		/// The Title of the Document
		/// </summary>
		[SimplScalar]
		[SimplHints(new Hint[] {Hint.XmlLeaf})]
		[SimplCompositeAsScalar]
		private MetadataString title;

		/// <summary>
		/// The document's actual location.
		/// </summary>
		[SimplScalar]
		private MetadataParsedURL location;

		[SimplScalar]
		[SimplHints(new Hint[] {Hint.XmlLeaf})]
		[SimplOtherTags(new String[] {"abstract_field"})]
		private MetadataString description;

		/// <summary>
		/// Relative location of a local copy of the document.
		/// </summary>
		[SimplScalar]
		private MetadataParsedURL localLocation;

		[SimplCollection("location")]
		[MmName("additional_locations")]
		private List<Ecologylab.Semantics.MetadataNS.Scalar.MetadataParsedURL> additionalLocations;

		public DocumentDeclaration()
		{ }

		public DocumentDeclaration(MetaMetadataCompositeField mmd) : base(mmd) { }


		public MetadataString Title
		{
			get{return title;}
			set
			{
				if (this.title != value)
				{
					this.title = value;
					// TODO we need to implement our property change notification mechanism.
				}
			}
		}

		public MetadataParsedURL Location
		{
			get{return location;}
			set
			{
				if (this.location != value)
				{
					this.location = value;
					// TODO we need to implement our property change notification mechanism.
				}
			}
		}

		public MetadataString Description
		{
			get{return description;}
			set
			{
				if (this.description != value)
				{
					this.description = value;
					// TODO we need to implement our property change notification mechanism.
				}
			}
		}

		public MetadataParsedURL LocalLocation
		{
			get{return localLocation;}
			set
			{
				if (this.localLocation != value)
				{
					this.localLocation = value;
					// TODO we need to implement our property change notification mechanism.
				}
			}
		}

		public List<Ecologylab.Semantics.MetadataNS.Scalar.MetadataParsedURL> AdditionalLocations
		{
			get{return additionalLocations;}
			set
			{
				if (this.additionalLocations != value)
				{
					this.additionalLocations = value;
					// TODO we need to implement our property change notification mechanism.
				}
			}
		}
	}
}