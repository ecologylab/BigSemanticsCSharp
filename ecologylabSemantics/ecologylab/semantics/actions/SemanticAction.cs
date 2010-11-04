//
//  SemanticAction.cs
//  s.im.pl serialization
//
//  Generated by DotNetTranslator on 10/10/10.
//  Copyright 2010 Interface Ecology Lab. 
//

using System;
using System.Collections.Generic;
using ecologylab.attributes;
using ecologylab.serialization;
using ecologylab.serialization.types;
using ecologylab.semantics.actions;
using ecologylab.semantics.connectors;
using ecologylab.semantics.metametadata;
using ecologylab.net;
using ecologylab.textformat;

namespace ecologylab.semantics.actions 
{
	/// <summary>
	/// missing java doc comments or could not find the source file.
	/// </summary>
	public class SemanticAction : ElementState
	{
		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[simpl_collection]
		[simpl_scope("condition_scope")]
		[simpl_nowrap]
		public List<Condition> checks;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[simpl_nowrap]
		[simpl_map("arg")]
		public Dictionary<String, Argument> args;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[simpl_scalar]
		[xml_tag("object")]
		public String objectStr;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[simpl_scalar]
		public String name;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[simpl_scalar]
		public String error;

		public SemanticAction()
		{ }
	}
}
