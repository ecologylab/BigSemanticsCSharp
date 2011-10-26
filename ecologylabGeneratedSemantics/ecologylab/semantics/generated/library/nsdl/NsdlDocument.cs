//
// NsdlDocument.cs
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
using ecologylab.semantics.generated.library.nsdl;
using ecologylab.semantics.generated.library.rss;
using ecologylab.semantics.metadata;
using ecologylab.semantics.metadata.builtins;
using ecologylab.semantics.metametadata;

namespace ecologylab.semantics.generated.library.nsdl 
{
	[SimplInherit]
	public class NsdlDocument : Dc
	{
		[SimplComposite]
		[MmName("header")]
		private Header header;

		public NsdlDocument()
		{ }

		public Header Header
		{
			get{return header;}
			set{header = value;}
		}
	}
}
