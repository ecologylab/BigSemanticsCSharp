//
//  Category.cs
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

namespace ecologylab.semantics.generated.library 
{
	/// <summary>
	/// Wikipedia Categories
	/// This is a generated code. DO NOT edit or modify it.
	/// @author MetadataCompiler
	/// </summary>
	[simpl_descriptor_classes(new Type[] { typeof(MetadataClassDescriptor), typeof(MetadataFieldDescriptor) })]
	[simpl_inherit]
	public class Category : Metadata
	{
		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[simpl_scalar]
		private MetadataString name;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[simpl_scalar]
		private MetadataParsedURL catLink;

		public Category()
		{ }

		public MetadataString Name
		{
			get{return name;}
			set{name = value;}
		}

		public MetadataParsedURL CatLink
		{
			get{return catLink;}
			set{catLink = value;}
		}
	}
}
