//
// TextSelfmade.cs
// s.im.pl serialization
//
// Generated by DotNetTranslator on 10/19/11.
// Copyright 2011 Interface Ecology Lab. 
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ecologylab.Semantics.MetaMetadataNS;
using Ecologylab.Semantics.MetadataNS.Builtins.Declarations;
using Simpl.Serialization.Attributes;

namespace Ecologylab.Semantics.MetadataNS.Builtins 
{
	[SimplInherit]
    [SimplOtherTags("annotation")]
	public class TextSelfmade : TextSelfmadeDeclaration
	{

		public TextSelfmade() { } 

        public TextSelfmade(MetaMetadataCompositeField mmd) : base(mmd) { }

	}
}