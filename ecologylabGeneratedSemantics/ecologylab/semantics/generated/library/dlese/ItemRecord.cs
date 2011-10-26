//
// ItemRecord.cs
// s.im.pl serialization
//
// Generated by MetaMetadataDotNetTranslator on 10/26/11.
// Copyright 2011 Interface Ecology Lab. 
//


using Simpl.Fundamental.Generic;
using Simpl.Fundamental.Net;
using Simpl.Serialization;
using Simpl.Serialization.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using ecologylab.collections;
using ecologylab.semantics.generated.library.dlese;
using ecologylab.semantics.metadata;
using ecologylab.semantics.metadata.builtins;
using ecologylab.semantics.metadata.scalar;
using ecologylab.semantics.metametadata;

namespace ecologylab.semantics.generated.library.dlese 
{
	[SimplInherit]
	public class ItemRecord : Metadata
	{
		[SimplComposite]
		[SimplTag("metaMetadata")]
		[MmName("record_meta_metadata")]
		private RecordMetaMetadata recordMetaMetadata;

		[SimplScalar]
		[SimplTag("schemaLocation")]
		[SimplOtherTags(new String[] {"xsi:schemaLocation"})]
		private MetadataParsedURL location;

		[SimplComposite]
		[MmName("lifecycle")]
		private Lifecycle lifecycle;

		[SimplComposite]
		[MmName("educational")]
		private Educational educational;

		[SimplComposite]
		[MmName("general")]
		private General general;

		public ItemRecord()
		{ }

		public RecordMetaMetadata RecordMetaMetadata
		{
			get{return recordMetaMetadata;}
			set{recordMetaMetadata = value;}
		}

		public MetadataParsedURL Location
		{
			get{return location;}
			set{location = value;}
		}

		public Lifecycle Lifecycle
		{
			get{return lifecycle;}
			set{lifecycle = value;}
		}

		public Educational Educational
		{
			get{return educational;}
			set{educational = value;}
		}

		public General General
		{
			get{return general;}
			set{general = value;}
		}
	}
}
