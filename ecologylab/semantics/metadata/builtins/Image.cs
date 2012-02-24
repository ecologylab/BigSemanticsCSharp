//
//  Image.cs
//  s.im.pl serialization
//
//  Generated by DotNetTranslator on 02/02/11.
//  Copyright 2011 Interface Ecology Lab. 
//

using System;
using System.Collections.Generic;
using Simpl.Serialization.Attributes;
using ecologylab.semantics.metadata.builtins.declarations;
using ecologylab.semantics.metadata.scalar;
using ecologylab.semantics.metadata;
using ecologylab.semantics.metametadata;

namespace ecologylab.semantics.metadata.builtins 
{
	[SimplInherit]
	public class Image : ImageDeclaration
	{

		public Image()
		{ }

        public Image(MetaMetadataCompositeField metaMetadata) : base(metaMetadata) { }

	}
}