//
//  ForEachSemanticAction.cs
//  s.im.pl serialization
//
//  Generated by DotNetTranslator on 11/16/10.
//  Copyright 2010 Interface Ecology Lab. 
//

using System;
using System.Collections.Generic;
using Simpl.Serialization.Attributes;
using Ecologylab.Semantics.Actions;

namespace Ecologylab.Semantics.Actions 
{
	/// <summary>
	/// missing java doc comments or could not find the source file.
	/// </summary>
	[SimplInherit]
	[SimplTag("for_each")]
    public class ForEachSemanticOperation : NestedSemanticOperation
	{
		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[SimplScalar]
		private String collection;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[SimplScalar]
		[SimplTag("as")]
		private String asStr;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[SimplScalar]
		private String start;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[SimplScalar]
		private String end;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[SimplScalar]
		private String currentIndex;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[SimplScalar]
		private String size;

		public ForEachSemanticOperation()
		{ }

		public String Collection
		{
			get{return collection;}
			set{collection = value;}
		}

		public String AsStr
		{
			get{return asStr;}
			set{asStr = value;}
		}

		public String Start
		{
			get{return start;}
			set{start = value;}
		}

		public String End
		{
			get{return end;}
			set{end = value;}
		}

		public String CurrentIndex
		{
			get{return currentIndex;}
			set{currentIndex = value;}
		}

		public String Size
		{
			get{return size;}
			set{size = value;}
		}

	    public override String GetOperationName()
	    {
		    return SemanticOperationStandardMethods.ForEach;
	    }
	}
}
