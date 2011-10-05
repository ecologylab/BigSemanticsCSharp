//
//  RegexFilter.cs
//  s.im.pl serialization
//
//  Generated by DotNetTranslator on 11/16/10.
//  Copyright 2010 Interface Ecology Lab. 
//

using System;
using System.Collections.Generic;
using Simpl.Serialization.Attributes;
using Simpl.Serialization;
using System.Text.RegularExpressions;

namespace ecologylab.semantics.metametadata 
{
	/// <summary>
	/// missing java doc comments or could not find the source file.
	/// </summary>
	public class RegexFilter 	{
		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[SimplScalar]
		[SimplHints(new Hint[] { Hint.XmlAttribute })]
		private Regex regex;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[SimplScalar]
		[SimplHints(new Hint[] { Hint.XmlAttribute })]
		private String replace;

		public RegexFilter()
		{ }

		public Regex RegexPattern
		{
			get{return regex;}
			set{regex = value;}
		}

		public String Replace
		{
			get{return replace;}
			set{replace = value;}
		}
	}
}
