
using Simpl.Fundamental.Generic;
using Simpl.Serialization;
using Simpl.Serialization.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using ecologylab.collections;
using ecologylab.semantics.metadata.builtins;
using ecologylab.semantics.metametadata;


using ecologylab.semantics.metadata;
using ecologylab.semantics.metadata.builtins;
using ecologylab.semantics.namesandnums;

// Developer should proof-read this TranslationScope before using it for production.
namespace ecologylab.semantics.metadata.builtins.declarations 
{
	public class MetadataBuiltinDeclarationsTranslationScope
	{
		public MetadataBuiltinDeclarationsTranslationScope()
		{ }

		public static SimplTypesScope Get()
		{
			return SimplTypesScope.Get(SemanticNames.REPOSITORY_METADATA_TRANSLATIONS,
				typeof(AnnotationDeclaration),
				typeof(ClippableDocumentDeclaration<>),
				typeof(ClippingDeclaration),
				typeof(CompoundDocumentDeclaration),
				typeof(DebugMetadataDeclaration),
				typeof(DocumentDeclaration),
				typeof(DocumentMetadataWrapDeclaration),
				typeof(ImageClippingDeclaration),
				typeof(ImageDeclaration),
				typeof(InformationCompositionDeclaration),
				typeof(MediaClippingDeclaration<>),
				typeof(MetadataDeclaration),
				typeof(TextClippingDeclaration));
		}

	}
}
