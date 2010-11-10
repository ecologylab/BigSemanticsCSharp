//
//  NsdlDocument.cs
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
using ecologylab.semantics.generated.library.rss;

namespace ecologylab.semantics.generated.library.nsdl 
{
	/// <summary>
	/// missing java doc comments or could not find the source file.
	/// </summary>
	[xml_tag("this_should_be_document")]
	[simpl_descriptor_classes(new Type[] { typeof(MetadataClassDescriptor), typeof(MetadataFieldDescriptor) })]
	[simpl_inherit]
	public class NsdlDocument : Metadata
	{
		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[xml_tag("fields")]
		[simpl_composite]
		[mm_name("nsdl_document")]
		private Dc nsdlDocument;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[simpl_composite]
		[mm_name("header")]
		private Header header;

		public NsdlDocument()
		{ }

		public Dc PNsdlDocument
		{
			get{return nsdlDocument;}
			set{nsdlDocument = value;}
		}

		public Header Header
		{
			get{return header;}
			set{header = value;}
		}
	}
}
