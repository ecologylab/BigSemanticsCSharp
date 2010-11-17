//
//  MetaMetadata.cs
//  s.im.pl serialization
//
//  Generated by DotNetTranslator on 11/16/10.
//  Copyright 2010 Interface Ecology Lab. 
//

using System;
using System.Collections.Generic;
using ecologylab.attributes;
using ecologylab.serialization.types.element;
using ecologylab.serialization;

namespace ecologylab.semantics.metametadata 
{
	/// <summary>
	/// missing java doc comments or could not find the source file.
	/// </summary>
	[simpl_inherit]
	public class MetaMetadata : MetaMetadataCompositeField, Mappable
	{
		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[simpl_composite]
		private MetaMetadataSelector selector;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[xml_tag("package")]
		[simpl_scalar]
		private String packageAttribute;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[simpl_scalar]
		private Boolean dontGenerateClass;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[simpl_collection("mixins")]
		[simpl_nowrap]
		private List<String> mixins;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[simpl_scalar]
		private String collectionOf;

		public MetaMetadata()
		{ }

        protected override string GetMetaMetadataTagToInheritFrom()
        {
            return extendsAttribute ?? base.GetMetaMetadataTagToInheritFrom();
        }

        #region Properties
        public MetaMetadataSelector Selector
        {
            get { return selector ?? MetaMetadataSelector.NULL_SELECTOR; }
            set { selector = value; }
        }

        public String PackageAttribute
        {
            get { return packageAttribute; }
            set { packageAttribute = value; }
        }

        public Boolean DontGenerateClass
        {
            get { return dontGenerateClass; }
            set { dontGenerateClass = value; }
        }

        public List<String> Mixins
        {
            get { return mixins; }
            set { mixins = value; }
        }

        public String CollectionOf
        {
            get { return collectionOf; }
            set { collectionOf = value; }
        }
        #endregion
    }
}
