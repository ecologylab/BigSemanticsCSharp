//
//  Condition.cs
//  s.im.pl serialization
//
//  Generated by DotNetTranslator on 11/16/10.
//  Copyright 2010 Interface Ecology Lab. 
//

using System;
using System.Collections.Generic;
using Simpl.Serialization.Attributes;
using Simpl.Serialization;

namespace ecologylab.semantics.actions 
{
	/// <summary>
	/// missing java doc comments or could not find the source file.
	/// </summary>
	[SimplInherit]
	public class Condition : ElementState	
    {
        public virtual bool Evaluate(SemanticOperationHandler handler)
        {
            Console.WriteLine("Condition.Evaluate() gets called: this should never happen.");
            return false;
        }
	}
}
