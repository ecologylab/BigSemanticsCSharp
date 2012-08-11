//
//  NotNull.cs
//  s.im.pl serialization
//
//  Generated by DotNetTranslator on 11/16/10.
//  Copyright 2010 Interface Ecology Lab. 
//

using System;
using System.Collections.Generic;
using Simpl.Serialization.Attributes;
using ecologylab.collections;

namespace ecologylab.semantics.actions 
{
	/// <summary>
	/// missing java doc comments or could not find the source file.
	/// </summary>
	[SimplInherit]
	public class NotNull : Condition
	{
		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[SimplScalar]
		private String value;

		public NotNull()
		{ }

		public String Value
		{
			get{return value;}
			set{this.value = value;}
		}

        ///<summary>
	    /// If the element is found in the semantic action environment and is not a null pointer, return
	    /// true. Otherwise, return false.
	    ///</summary>
	    public override bool Evaluate(SemanticOperationHandler handler)
	    {
		    String name = Value;
            Scope<Object> theMap = handler.SemanticOperationVariableMap;

		    return theMap.ContainsKey(name) && theMap[name] != null;
	    }
	}
}
