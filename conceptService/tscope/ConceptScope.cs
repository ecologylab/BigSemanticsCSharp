using System;
using System.Collections.Generic;
using ecologylab.attributes;
using wixplorer.concept;
using ecologylab.serialization;

//developer should modify the namespace
//by default it falls into ecologylab.serialization
namespace ecologylab.serialization 
{
	public class ConceptScope
	{
		public ConceptScope()
		{ }

		public static TranslationScope Get()
		{
			return TranslationScope.Get("ConceptScope",
				typeof(SuggestedConcepts),
				typeof(ConceptGroup),
				typeof(Concept),
				typeof(SingleSourceRelatedness));
		}
	}
}
