//
//  MetaMetadataCollectionField.cs
//  s.im.pl serialization
//
//  Generated by DotNetTranslator on 10/10/10.
//  Copyright 2010 Interface Ecology Lab. 
//

using System;
using System.Collections.Generic;
using ecologylab.attributes;
using ecologylab.serialization;
using ecologylab.serialization.types;
using ecologylab.semantics.actions;
using ecologylab.semantics.connectors;
using ecologylab.semantics.metametadata;
using ecologylab.net;
using ecologylab.textformat;

namespace ecologylab.semantics.metametadata 
{
	/// <summary>
	/// missing java doc comments or could not find the source file.
	/// </summary>
	[simpl_inherit]
	[xml_tag("collection")]
	public class MetaMetadataCollectionField : MetaMetadataNestedField
	{
		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[simpl_scalar]
		public String childTag;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[simpl_scalar]
		public String childType;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[simpl_scalar]
		public Boolean childEntity;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[simpl_scalar]
		public Boolean noWrap;

		public MetaMetadataCollectionField()
		{ }
	}
}
