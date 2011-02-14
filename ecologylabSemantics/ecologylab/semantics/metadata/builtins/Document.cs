//
//  Document.cs
//  s.im.pl serialization
//
//  Generated by DotNetTranslator on 02/09/11.
//  Copyright 2011 Interface Ecology Lab. 
//

using System;
using System.Collections.Generic;
using ecologylab.attributes;
using ecologylab.semantics.metadata.scalar;
using ecologylab.semantics.metadata;

namespace ecologylab.semantics.metadata.builtins 
{
	/// <summary>
	/// missing java doc comments or could not find the source file.
	/// </summary>
	[simpl_descriptor_classes(new Type[] { typeof(MetadataClassDescriptor), typeof(MetadataFieldDescriptor) })]
	[simpl_inherit]
	public class Document : ClippableMetadata
	{
		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[mm_name("location")]
		[simpl_scalar]
		private MetadataParsedURL location;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[mm_name("title")]
		[simpl_scalar]
		private MetadataString title;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[mm_name("description")]
		[simpl_scalar]
		private MetadataString description;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[simpl_scalar]
		[simpl_hints(new Hint[] { Hint.XML_LEAF })]
		private MetadataString query;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[mm_name("generation")]
		[simpl_scalar]
		private MetadataInteger generation;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[mm_name("page_structure")]
		[simpl_scalar]
		private MetadataString pageStructure;

        [mm_name("favicon")]
        [simpl_scalar]
        private MetadataParsedURL favicon;



		public Document()
		{ }

		public MetadataParsedURL Location
		{
			get{return location;}
			set{location = value;}
		}

		public MetadataString Title
		{
			get{return title;}
			set{title = value;}
		}

		public MetadataString Description
		{
			get{return description;}
			set{description = value;}
		}

		public MetadataString Query
		{
			get{return query;}
			set{query = value;}
		}

		public MetadataInteger Generation
		{
			get{return generation;}
			set{generation = value;}
		}

		public MetadataString PageStructure
		{
			get{return pageStructure;}
			set{pageStructure = value;}
		}

        public MetadataParsedURL Favicon
        {
            get { return favicon; }
            set { favicon = value; }
        }
	}   
}
