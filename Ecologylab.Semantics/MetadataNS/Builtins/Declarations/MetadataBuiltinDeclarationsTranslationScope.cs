
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
				typeof(AnnotateDeclaration),
				typeof(AssignPrimaryLinkDeclaration),
				typeof(AudioDeclaration),
				typeof(ClippableDocumentDeclaration),
				typeof(ClippingDeclaration<>),
				typeof(CreativeActDeclaration),
				typeof(CurateLinkDeclaration),
				typeof(DebugMetadataDeclaration),
				typeof(DocumentDeclaration),
				typeof(DocumentMetadataWrapDeclaration),
				typeof(HtmlTextDeclaration),
				typeof(ImageClippingDeclaration),
				typeof(ImageDeclaration),
				typeof(ImageSelfmadeDeclaration),
				typeof(MetadataDeclaration),
				typeof(RichArtifactDeclaration<>),
				typeof(RichDocumentDeclaration),
				typeof(SequencedClippableDocumentDeclaration),
				typeof(TextClippingDeclaration),
				typeof(TextSelfmadeDeclaration),
				typeof(VideoDeclaration));
		}

	}
}
