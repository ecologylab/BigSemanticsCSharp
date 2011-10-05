//
//  SearchEngine.cs
//  s.im.pl serialization
//
//  Generated by DotNetTranslator on 11/16/10.
//  Copyright 2010 Interface Ecology Lab. 
//

using System;
using System.Collections.Generic;
using Simpl.Serialization.Attributes;
using Simpl.Serialization;
using Simpl.Serialization.Types.Element;


namespace ecologylab.semantics.metametadata 
{
	/// <summary>
	/// missing java doc comments or could not find the source file.
	/// </summary>
	public class SearchEngine : ElementState, IMappable
	{
		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[SimplScalar]
		private String name;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[SimplScalar]
		private String urlPrefix;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[SimplScalar]
		private String urlSuffix;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[SimplScalar]
		private String numResultString;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[SimplScalar]
		private String startString;

		public SearchEngine()
		{ }

		public String Name
		{
			get{return name;}
			set{name = value;}
		}

		public String UrlPrefix
		{
			get{return urlPrefix;}
			set{urlPrefix = value;}
		}

		public String UrlSuffix
		{
			get{return urlSuffix;}
			set{urlSuffix = value;}
		}

		public String NumResultString
		{
			get{return numResultString;}
			set{numResultString = value;}
		}

		public String StartString
		{
			get{return startString;}
			set{startString = value;}
		}

		public object Key()
		{
            return name;
		}
	}
}
