//
//  MetaMetadataField.cs
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
using ecologylab.semantics.metadata;
using ecologylab.generic;
using ecologylab.serialization.types;
using System.Text.RegularExpressions;

namespace ecologylab.semantics.metametadata 
{
	/// <summary>
	/// missing java doc comments or could not find the source file.
	/// </summary>
	[simpl_inherit]
	public class MetaMetadataField : ElementState, Mappable
    {
        #region Variables
        
        /// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[simpl_scalar]
		private String name;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[simpl_scalar]
		private String comment;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[simpl_scalar]
		private String tag;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[simpl_scalar]
		private String xpath;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[simpl_scalar]
		private String contextNode;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[xml_tag("extends")]
		[simpl_scalar]
		protected String extendsAttribute;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[simpl_map]
		[simpl_classes(new Type[] { typeof(MetaMetadataField), typeof(MetaMetadataScalarField), typeof(MetaMetadataCompositeField), typeof(MetaMetadataCollectionField) })]
		[simpl_nowrap]
		private DictionaryList<String, MetaMetadataField> kids;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[simpl_scalar]
		private Boolean hide;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[simpl_scalar]
		private Boolean alwaysShow;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[simpl_scalar]
		private String style;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[simpl_scalar]
		private Single layer;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[simpl_scalar]
		private String navigatesTo;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[simpl_scalar]
		private String shadows;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[simpl_scalar]
		private String label;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[simpl_scalar]
		private Boolean isFacet;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[simpl_scalar]
		private Boolean ignoreInTermVector;

        protected MetadataClassDescriptor metadataClassDescriptor;

        protected bool inheritMetaMetadataFinished = false;

        private bool fieldsSortedForDisplay = false;

        private Type metadataClass;

        MetadataFieldDescriptor metadataFieldDescriptor;

        HashSet<String> nonDisplayedFieldNames = new HashSet<String>();
        #endregion
        
        public MetaMetadataField()
		{ }

        protected void SortForDisplay()
        {

            DictionaryList<String, MetaMetadataField> childMetaMetadata = Kids;
            if (childMetaMetadata != null)
            {
                childMetaMetadata.Values.Sort(delegate(MetaMetadataField f1, MetaMetadataField f2) { return -Math.Sign(f1.Layer - f2.Layer); });
            }

            fieldsSortedForDisplay = true;

        }

        public DictionaryList<String, MetaMetadataField> InitializeChildMetaMetadata()
        {
            this.kids = new DictionaryList<string, MetaMetadataField>();
            return this.kids;
        }

        protected void InheritForField(MetaMetadataField fieldToInheritFrom)
        {
            String fieldName = fieldToInheritFrom.Name;
            // this is for the case when meta_metadata has no meta_metadata fields of its own. It just
            // inherits from super class.

            DictionaryList<String, MetaMetadataField> childMetaMetadata = Kids;
            if (childMetaMetadata == null)
            {
                childMetaMetadata = InitializeChildMetaMetadata();
            }

            // *do not* override fields in here with fields from super classes.

            MetaMetadataField fieldToInheritTo;
            childMetaMetadata.TryGetValue(fieldName, out fieldToInheritTo);
            if (fieldToInheritTo == null)
            {
                childMetaMetadata.Add(fieldName, fieldToInheritFrom);
                fieldToInheritTo = fieldToInheritFrom;
            }
            else
            {
                fieldToInheritTo.InheritNonDefaultAttributes(fieldToInheritFrom);
            }

            DictionaryList<String, MetaMetadataField> inheritedChildMetaMetadata = fieldToInheritFrom.Kids;
            if (inheritedChildMetaMetadata != null)
            {
                foreach (MetaMetadataField grandChildMetaMetadataField in inheritedChildMetaMetadata.Values)
                {
                    fieldToInheritTo.InheritForField(grandChildMetaMetadataField);
                }
            }
        }

        #region binders

        internal bool GetClassAndBindDescriptors(TranslationScope metadataTScope)
        {
            Type metadataClass = GetMetadataClass(metadataTScope);
		    if (metadataClass == null)
		    {
			    ElementState parent = Parent;
                Type parentType = parent.GetType();
                if (parentType.IsInstanceOfType(typeof(MetaMetadataField)))
                    (((MetaMetadataField)parent).kids).Remove(this.Name);

                else if (parentType.IsInstanceOfType(typeof(MetaMetadataRepository)))
                {
                    // TODO remove from the repository level
                }
			    return false;
		    }
		    //
		    BindClassDescriptor(metadataClass, metadataTScope);
		    return true;
        }

        /**
	     * Obtain a map of FieldDescriptors for this class, with the field names as key, but with the
	     * mixins field removed. Use lazy evaluation, caching the result by class name.
	     * 
	     * @param metadataTScope
	     *          TODO
	     * 
	     * @return A map of FieldDescriptors, with the field names as key, but with the mixins field
	     *         removed.
	     */
	    void BindClassDescriptor(Type metadataClass, TranslationScope metadataTScope)
	    {
		    MetadataClassDescriptor metadataClassDescriptor = this.metadataClassDescriptor;
		    if (metadataClassDescriptor == null)
		    {
				metadataClassDescriptor = this.metadataClassDescriptor;
				if (metadataClassDescriptor == null)
				{
                    try
                    {
                        metadataClassDescriptor = (MetadataClassDescriptor)ClassDescriptor.GetClassDescriptor(metadataClass);
                    }
                    catch { }
					BindMetadataFieldDescriptors(metadataTScope, metadataClassDescriptor);
					this.metadataClassDescriptor = metadataClassDescriptor;
				}
		    }
	    }

        private void BindMetadataFieldDescriptors(TranslationScope metadataTScope, MetadataClassDescriptor metadataClassDescriptor)
        {
            if (Kids == null)
                return;
            foreach (MetaMetadataField thatChild in Kids.Values)
		    {
			    thatChild.bindMetadataFieldDescriptor(metadataTScope, metadataClassDescriptor);

			    if (thatChild.GetType().IsInstanceOfType(typeof(MetaMetadataScalarField)))
			    {
				    MetaMetadataScalarField scalar = (MetaMetadataScalarField) thatChild;
				    if (scalar.Filter != null)
				    {
                        MetadataFieldDescriptor fd = scalar.MetadataFieldDescriptor;
                        fd.RegexPattern = scalar.Filter.RegexPattern;
                        fd.RegexReplacement =  scalar.Filter.Replace;
				    }
			    }

			    if (thatChild.hide)
				    nonDisplayedFieldNames.Add(thatChild.name);
			    if (thatChild.shadows != null)
				    nonDisplayedFieldNames.Add(thatChild.shadows);

			    // recursive descent
			    if (thatChild.HasChildren())
				    thatChild.GetClassAndBindDescriptors(metadataTScope);
		    }
        }

        public Boolean HasChildren()
        {
            return kids != null && kids.Count > 0;
        }

        void bindMetadataFieldDescriptor(TranslationScope metadataTScope,
            MetadataClassDescriptor metadataClassDescriptor)
        {
            String tagName = this.GetTagForTranslationScope(); // TODO -- is this the correct tag?
            MetadataFieldDescriptor metadataFieldDescriptor = (MetadataFieldDescriptor)metadataClassDescriptor.GetFieldDescriptorByTag(tagName);
            if (metadataFieldDescriptor != null)
            {
                // if we don't have a field, then this is a wrapped collection, so we need to get the wrapped
                // field descriptor
                if (metadataFieldDescriptor.Field == null)
                    metadataFieldDescriptor = (MetadataFieldDescriptor)metadataFieldDescriptor.WrappedFieldDescriptor;

                this.MetadataFieldDescriptor = metadataFieldDescriptor;
            }
            else
            {
                Console.WriteLine("Ignoring <" + tagName + "> because no corresponding MetadataFieldDescriptor can be found.");
            }

        }

        #endregion

        internal Type GetMetadataClass(TranslationScope metadataTScope)
        {
            Type result = metadataClass;

            if (result == null)
            {
                String tagForTS = GetTagForTranslationScope();
                result = metadataTScope.GetClassByTag(tagForTS);
                if (result == null)
                {
                    if (typeof(MetaMetadataCompositeField).IsAssignableFrom(this.GetType()))
                    {
                        MetaMetadataCompositeField mmCF = ((MetaMetadataCompositeField)this);
                        String tagToUse = mmCF.GetTypeOrName();
                        
                        result = metadataTScope.GetClassByTag(tagToUse);
                    }
                    if(result == null && ExtendsAttribute != null)
                        result = metadataTScope.GetClassByTag(ExtendsAttribute);
                }
                if (result != null)
                    metadataClass = result;
                else
                    Console.WriteLine("Cant' resolve metadata for " + this.Name + " using " + tagForTS);
            }
            return result;
        }

        internal void InheritNonDefaultAttributes(MetaMetadataField inheritFrom)
        {
            ClassDescriptor classDescriptor = ElementClassDescriptor;

            foreach (FieldDescriptor fieldDescriptor in classDescriptor.AttributeFieldDescriptors)
            {
                ScalarType scalarType = fieldDescriptor.GetScalarType();
                try
                {
                    if (scalarType != null && scalarType.IsDefaultValue(fieldDescriptor.Field, this)
                            && !scalarType.IsDefaultValue(fieldDescriptor.Field, inheritFrom))
                    {
                        Object value = fieldDescriptor.Field.GetValue(inheritFrom);
                        fieldDescriptor.Field.SetValue(this, value);
                        //Console.WriteLine("inherit\t" + this.Name + "." + fieldDescriptor.FieldName + "\t= " + value);
                    }
                }
                catch (Exception e)
                {
                    // TODO Auto-generated catch block
                    Console.WriteLine("inherit\t" + this.Name + "." + fieldDescriptor.FieldName + " Failed, ignore it.\n" + e);
                }
            }

        }

        private String GetTagForTranslationScope()
        {
            return Tag ?? Name;
        }

        #region Properties
        
        public String Name
		{
			get{return name;}
			set{name = value;}
		}

		public String Comment
		{
			get{return comment;}
			set{comment = value;}
		}

		public String Tag
		{
			get{return tag;}
			set{tag = value;}
		}

		public String Xpath
		{
			get{return xpath;}
			set{xpath = value;}
		}

		public String ContextNode
		{
			get{return contextNode;}
			set{contextNode = value;}
		}

		public String ExtendsAttribute
		{
			get{return extendsAttribute;}
			set{extendsAttribute = value;}
		}

		public DictionaryList<String, MetaMetadataField> Kids
		{
			get{return kids;}
			set{kids = value;}
		}

		public Boolean Hide
		{
			get{return hide;}
			set{hide = value;}
		}

		public Boolean AlwaysShow
		{
			get{return alwaysShow;}
			set{alwaysShow = value;}
		}

		public String Style
		{
			get{return style;}
			set{style = value;}
		}

		public Single Layer
		{
			get{return layer;}
			set{layer = value;}
		}

		public String NavigatesTo
		{
			get{return navigatesTo;}
			set{navigatesTo = value;}
		}

		public String Shadows
		{
			get{return shadows;}
			set{shadows = value;}
		}

		public String Label
		{
			get{return label;}
			set{label = value;}
		}

		public Boolean IsFacet
		{
			get{return isFacet;}
			set{isFacet = value;}
		}

		public Boolean IgnoreInTermVector
		{
			get{return ignoreInTermVector;}
			set{ignoreInTermVector = value;}
		}

        public MetadataClassDescriptor MetadataClassDescriptorP
        {
            get { return metadataClassDescriptor; }
        }

        #endregion
        public Object key()
		{
            return name;
		}



        public MetadataFieldDescriptor MetadataFieldDescriptor 
        {
            get { return metadataFieldDescriptor; } 
            set{ metadataFieldDescriptor = value; } 
        }
    }
}
