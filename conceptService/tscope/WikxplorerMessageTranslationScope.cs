using System;
using System.Collections.Generic;
using ecologylab.attributes;
using wikxplorer.messages;
using ecologylab.oodss.messages;
using ecologylab.serialization;

//developer should modify the namespace
//by default it falls into ecologylab.serialization
namespace ecologylab.serialization 
{
	public class WikxplorerMessageTranslationScope
	{
		public WikxplorerMessageTranslationScope()
		{ }

		public static TranslationScope Get()
		{
            return TranslationScope.Get("wikxplorer_message_translation_scope",
                //typeof(ServiceMessage),
                typeof(UpdateContextRequest),
                typeof(RelatednessRequest),
                typeof(SuggestionRequest),
                typeof(SuggestionResponse),
                typeof(ConceptGroup),
                //typeof(RequestMessage),
                typeof(Concept),
                typeof(RelatednessResponse));//,
				//typeof(ResponseMessage));

		}
	}
}
