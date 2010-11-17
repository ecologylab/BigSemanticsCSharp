//
//  Argument.cs
//  s.im.pl serialization
//
//  Generated by DotNetTranslator on 11/16/10.
//  Copyright 2010 Interface Ecology Lab. 
//

using System;
using System.Collections.Generic;
using ecologylab.attributes;
using ecologylab.serialization;
using ecologylab.serialization.types.element;

namespace ecologylab.semantics.metametadata 
{
	/// <summary>
	/// missing java doc comments or could not find the source file.
	/// </summary>
	[xml_tag("arg")]
	public class Argument : ElementState, Mappable
	{
		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[simpl_scalar]
		private String value;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[simpl_scalar]
		private String name;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[simpl_scalar]
		private String check;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[simpl_scalar]
		private String context;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[simpl_scalar]
		private Boolean isNested;

		public Argument()
		{ }

		public String Value
		{
			get{return value;}
			set{this.value = value;}
		}

		public String Name
		{
			get{return name;}
			set{name = value;}
		}

		public String Check
		{
			get{return check;}
			set{check = value;}
		}

		public String Context
		{
			get{return context;}
			set{context = value;}
		}

		public Boolean IsNested
		{
			get{return isNested;}
			set{isNested = value;}
		}

		public Object key()
		{
            return name;
		}
	}
}
