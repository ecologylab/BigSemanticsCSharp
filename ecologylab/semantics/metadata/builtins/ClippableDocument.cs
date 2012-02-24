//
//  ClippableDocument.cs
//  s.im.pl serialization
//
//  Generated by DotNetTranslator on 11/10/10.
//  Copyright 2010 Interface Ecology Lab. 
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simpl.Serialization.Attributes;
using ecologylab.semantics.metadata.builtins.declarations;
using ecologylab.semantics.metadata.scalar;
using ecologylab.semantics.metametadata;

namespace ecologylab.semantics.metadata.builtins
{
	[SimplInherit]
	public class ClippableDocument<ME> : ClippableDocumentDeclaration<ME> where ME : ClippableDocument<ME>
	{

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[MmName("caption")]
		[SimplScalar]
		private MetadataString caption;

		public ClippableDocument() { }

        public ClippableDocument(MetaMetadataCompositeField mmd) : base(mmd) { }

	}
}