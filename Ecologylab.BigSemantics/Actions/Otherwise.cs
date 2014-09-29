//
//  Otherwise.cs
//  s.im.pl serialization
//
//  Generated by DotNetTranslator on 11/16/10.
//  Copyright 2010 Interface Ecology Lab. 
//

using System;
using System.Collections.Generic;
using Simpl.Serialization.Attributes;
using Ecologylab.BigSemantics.Actions;

namespace Ecologylab.BigSemantics.Actions 
{
	/// <summary>
	/// missing java doc comments or could not find the source file.
	/// </summary>
	[SimplInherit]
    public class Otherwise : NestedSemanticOperation
	{
		public Otherwise()
		{ }

        public override String GetOperationName()
		{
			return SemanticOperationStandardMethods.Otherwise;
		}

		///<summary>
		/// Otherwise.perform() does not do anything since Otherwise is merely a container for nested
		/// semantic actions.
		///</summary>
		public override Object Perform(Object obj)
		{
			return null;
		}
	}
}