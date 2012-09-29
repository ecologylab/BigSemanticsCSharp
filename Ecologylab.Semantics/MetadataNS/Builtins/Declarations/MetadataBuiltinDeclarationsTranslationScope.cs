
using Ecologylab.Collections;
using Ecologylab.Semantics.MetaMetadataNS;
using Ecologylab.Semantics.MetadataNS.Builtins;
using Simpl.Fundamental.Generic;
using Simpl.Serialization;
using Simpl.Serialization.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;



// Developer should proof-read this TranslationScope before using it for production.
namespace Ecologylab.Semantics.MetadataNS.Builtins.Declarations 
{
	public class MetadataBuiltinDeclarationsTranslationScope
	{
		public MetadataBuiltinDeclarationsTranslationScope()
		{ }

		public static SimplTypesScope Get()
		{
			return SimplTypesScope.Get("repository_builtin_declarations_scope",
				typeof(AnnotationDeclaration),
				typeof(ClippableDocumentDeclaration<>),
				typeof(ClippingDeclaration),
				typeof(CompoundDocumentDeclaration),
				typeof(DebugMetadataDeclaration),
				typeof(DocumentDeclaration),
				typeof(DocumentMetadataWrapDeclaration),
				typeof(ImageClippingDeclaration),
				typeof(ImageDeclaration),
				typeof(MediaClippingDeclaration<>),
				typeof(MetadataDeclaration),
				typeof(TextClippingDeclaration));
		}

	}
}
