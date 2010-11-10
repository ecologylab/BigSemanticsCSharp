//
//  SlashdotRss.cs
//  s.im.pl serialization
//
//  Generated by DotNetTranslator on 11/10/10.
//  Copyright 2010 Interface Ecology Lab. 
//

using System;
using System.Collections.Generic;
using ecologylab.attributes;
using ecologylab.semantics.metadata.scalar;
using ecologylab.semantics.metadata;
using ecologylab.semantics.metadata.builtins;

namespace ecologylab.semantics.generated.library.slashdot 
{
	/// <summary>
	/// missing java doc comments or could not find the source file.
	/// </summary>
	[xml_tag("rdf:RDF")]
	[simpl_descriptor_classes(new Type[] { typeof(MetadataClassDescriptor), typeof(MetadataFieldDescriptor) })]
	[simpl_inherit]
	public class SlashdotRss : Document
	{
		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[simpl_collection("item")]
		[simpl_nowrap]
		[mm_name("items")]
		private List<SlashdotItem> items;

		public SlashdotRss()
		{ }

		public List<SlashdotItem> Items
		{
			get{return items;}
			set{items = value;}
		}
	}
}
