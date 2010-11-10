//
//  DcDocument.cs
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

namespace ecologylab.semantics.generated.library.misc 
{
	/// <summary>
	/// missing java doc comments or could not find the source file.
	/// </summary>
	[simpl_descriptor_classes(new Type[] { typeof(MetadataClassDescriptor), typeof(MetadataFieldDescriptor) })]
	[simpl_inherit]
	public class DcDocument : Document
	{
		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[simpl_scalar]
		private MetadataString subject;

		public DcDocument()
		{ }

		public MetadataString Subject
		{
			get{return subject;}
			set{subject = value;}
		}
	}
}
