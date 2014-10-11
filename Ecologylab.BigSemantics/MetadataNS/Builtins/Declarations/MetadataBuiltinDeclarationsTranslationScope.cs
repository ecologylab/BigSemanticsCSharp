
using Ecologylab.BigSemantics.MetaMetadataNS;
using Ecologylab.BigSemantics.MetadataNS.Builtins;
using Ecologylab.Collections;
using Simpl.Fundamental.Generic;
using Simpl.Serialization;
using Simpl.Serialization.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;


using Ecologylab.BigSemantics.MetadataNS.Builtins.Declarations.CommentNS;
using Ecologylab.BigSemantics.MetadataNS.Builtins.Declarations.CreativeWorkNS;
using Ecologylab.BigSemantics.MetadataNS.Builtins.Declarations.PersonNS;
using Ecologylab.BigSemantics.MetadataNS.Builtins.Declarations.PersonNS.AuthorNS;

// Developer should proof-read this TranslationScope before using it for production.
namespace Ecologylab.BigSemantics.MetadataNS.Builtins.Declarations 
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
				typeof(AuthorDeclaration),
				typeof(BirthDetailDeclaration),
				typeof(ClippableDocumentDeclaration),
				typeof(ClippingDeclaration<>),
				typeof(CommentDeclaration),
				typeof(ContactPointDeclaration),
				typeof(CreativeActDeclaration),
				typeof(CreativeWorkDeclaration),
				typeof(CurateLinkDeclaration),
				typeof(CurationDeclaration),
				typeof(DebugMetadataDeclaration),
				typeof(DocumentDeclaration),
				typeof(DocumentMetadataWrapDeclaration),
				typeof(GisLocationDeclaration),
				typeof(HtmlTextDeclaration),
				typeof(IdeaMacheUserDeclaration),
				typeof(ImageClippingDeclaration),
				typeof(ImageDeclaration),
				typeof(ImageSelfmadeDeclaration),
				typeof(MetadataDeclaration),
				typeof(PersonDeclaration),
				typeof(RatingDeclaration),
				typeof(RichArtifactDeclaration<>),
				typeof(RichDocumentDeclaration),
				typeof(SequencedClippableDocumentDeclaration),
				typeof(TextClippingDeclaration),
				typeof(TextSelfmadeDeclaration),
				typeof(VideoDeclaration));
		}

	}
}
