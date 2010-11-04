//
//  Argument.cs
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

namespace ecologylab.semantics.metametadata 
{
	/// <summary>
	/// missing java doc comments or could not find the source file.
	/// </summary>
	[xml_tag("arg")]
	public class Argument : ElementState
	{
		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[simpl_scalar]
		public String value;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[simpl_scalar]
		public String name;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[simpl_scalar]
		public String check;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[simpl_scalar]
		public String context;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[simpl_scalar]
		public Boolean isNested;

		public Argument()
		{ }
	}
}
