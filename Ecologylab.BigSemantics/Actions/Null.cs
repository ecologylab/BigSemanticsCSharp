//
//  Null.cs
//  s.im.pl serialization
//
//  Generated by DotNetTranslator on 11/16/10.
//  Copyright 2010 Interface Ecology Lab. 
//

using System;
using System.Collections.Generic;
using Ecologylab.Collections;
using Simpl.Serialization.Attributes;

namespace Ecologylab.BigSemantics.Actions 
{
	/// <summary>
	/// missing java doc comments or could not find the source file.
	/// </summary>
	[SimplInherit]
	public class Null : Condition
	{
		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[SimplScalar]
		private String value;

		public Null()
		{ }

		public String Value
		{
			get{return value;}
			set{this.value = value;}
		}

	    /**
	     * If the element is found in the semantic action environment and is not a null pointer, return
	     * true. Otherwise, return false.
	     */
	    public override bool Evaluate(SemanticOperationHandler handler)
	    {
		    String name = Value;
            Scope<Object> theMap = handler.SemanticOperationVariableMap;
		    return theMap.ContainsKey(name) && theMap[name] == null;
	    }
	}
}
