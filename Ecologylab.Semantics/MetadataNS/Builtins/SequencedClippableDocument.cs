//
// SequencedClippableDocument.cs
// s.im.pl serialization
//
// Generated by MetaMetadataDotNetTranslator.
// Copyright 2014 Interface Ecology Lab. 
//


using Ecologylab.Semantics.MetadataNS.Builtins.Declarations;
using Ecologylab.Semantics.MetadataNS.Scalar;
using Ecologylab.Semantics.MetaMetadataNS;
using Simpl.Serialization.Attributes;

namespace Ecologylab.Semantics.MetadataNS.Builtins 
{
	[SimplInherit]
	public class SequencedClippableDocument : SequencedClippableDocumentDeclaration
	{
		/// <summary>
		/// duration of media in milliseconds.
		/// </summary>
		[SimplScalar]
		private MetadataInteger duration;

		[SimplScalar]
		private MetadataString fileFormat;

		public SequencedClippableDocument()
		{ }

		public SequencedClippableDocument(MetaMetadataCompositeField mmd) : base(mmd) { }


		public MetadataInteger Duration
		{
			get{return duration;}
			set
			{
				if (this.duration != value)
				{
					this.duration = value;
					// TODO we need to implement our property change notification mechanism.
				}
			}
		}

		public MetadataString FileFormat
		{
			get{return fileFormat;}
			set
			{
				if (this.fileFormat != value)
				{
					this.fileFormat = value;
					// TODO we need to implement our property change notification mechanism.
				}
			}
		}
	}
}