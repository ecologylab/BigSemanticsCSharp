//
//  SemanticAction.cs
//  s.im.pl serialization
//
//  Generated by DotNetTranslator on 11/16/10.
//  Copyright 2010 Interface Ecology Lab. 
//

using System;
using System.Collections.Generic;
using Simpl.Serialization.Attributes;
using ecologylab.semantics.metametadata;
using Simpl.Serialization;

namespace ecologylab.semantics.actions 
{
	/// <summary>
	/// missing java doc comments or could not find the source file.
	/// </summary>
	public class SemanticAction
	{
		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[SimplCollection]
		[SimplScope("condition_scope")]
		[SimplNoWrap]
		private List<Condition> checks;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[SimplNoWrap]
		[SimplMap("arg")]
		private Dictionary<String, Argument> args;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[SimplScalar]
		[SimplTag("object")]
		private String objectStr;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[SimplScalar]
		private String name;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[SimplScalar]
		private String error;

		public SemanticAction()
		{ }

		public List<Condition> Checks
		{
			get{return checks;}
			set{checks = value;}
		}

		public Dictionary<String, Argument> Args
		{
			get{return args;}
			set{args = value;}
		}

		public String ObjectStr
		{
			get{return objectStr;}
			set{objectStr = value;}
		}

		public String Name
		{
			get{return name;}
			set{name = value;}
		}

		public String Error
		{
			get{return error;}
			set{error = value;}
		}
	}
}