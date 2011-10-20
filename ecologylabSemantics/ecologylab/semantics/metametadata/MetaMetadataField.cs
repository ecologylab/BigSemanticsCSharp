//
//  MetaMetadataField.cs
//  s.im.pl serialization
//
//  Generated by DotNetTranslator on 11/16/10.
//  Copyright 2010 Interface Ecology Lab. 
//

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Simpl.Fundamental.Generic;
using Simpl.Serialization.Attributes;
using Simpl.Serialization;
using Simpl.Serialization.Types;
using Simpl.Serialization.Types.Element;
using ecologylab.semantics.metadata;


using System.Text.RegularExpressions;
using ecologylabSemantics.ecologylab.semantics.metametadata;

namespace ecologylab.semantics.metametadata 
{
	[SimplInherit]
    [SimplDescriptorClasses(new[] { typeof(MetaMetadataClassDescriptor), typeof(MetaMetadataFieldDescriptor)})]
	public abstract class MetaMetadataField : ElementState, IMappable, IEnumerable<MetaMetadataField>
    {
        #region Variables

        private static readonly List<MetaMetadataField> EMPTY_COLLECTION        = new List<MetaMetadataField>(0);
        private static readonly IEnumerator<MetaMetadataField> EMPTY_ITERATOR   = EMPTY_COLLECTION.GetEnumerator();
        
        /// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[SimplScalar]
		private String name;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[SimplScalar]
        [MmDontInherit]
		private String comment;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[SimplScalar]
		private String tag;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[SimplScalar]
		private String xpath;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[SimplScalar]
		private String contextNode;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[SimplMap]
		// [SimplClasses(new Type[] { typeof(MetaMetadataField), typeof(MetaMetadataScalarField), typeof(MetaMetadataCompositeField), typeof(MetaMetadataCollectionField) })]
		[SimplScope(MetaMetadataFieldTranslationScope.NAME)]
		[SimplNoWrap]
		protected DictionaryList<String, MetaMetadataField> kids;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[SimplScalar]
		private Boolean hide;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[SimplScalar]
		private Boolean alwaysShow;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[SimplScalar]
		private String style;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[SimplScalar]
		private Single layer;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[SimplScalar]
		private String navigatesTo;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[SimplScalar]
		private String shadows;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[SimplScalar]
		private String label;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[SimplScalar]
		private Boolean isFacet;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[SimplScalar]
		private Boolean ignoreInTermVector;

        /// <summary>
        /// 
        ///  The name of natural id if this field is used as one.
        /// </summary>
        [SimplScalar]
        private String asNaturalId;

        protected MetadataClassDescriptor metadataClassDescriptor;

        protected bool inheritMetaMetadataFinished = false;
	    protected bool inheritInProcess;

        private bool fieldsSortedForDisplay = false;

        protected Type metadataClass;

	    protected MetadataFieldDescriptor metadataFieldDescriptor;

	    private HashSet<String> nonDisplayedFieldNames = new HashSet<String>();

        private String _displayedLabel = null;

        private bool _bindFieldDescriptorsFinished;

       
        /**
	    * from which field this one inherits. could be null if this field is declared for the first time.
	    */
        private MetaMetadataField inheritedField = null;

	    protected bool inheritFinished;

        /**
         * in which meta-metadata this field is declared.
         */
        private MetaMetadata declaringMmd = null;
	

        public MetaMetadataRepository Repository { get; set; }

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

//        protected void InheritForField(MetaMetadataField fieldToInheritFrom)
//        {
//            String fieldName = fieldToInheritFrom.Name;
//            // this is for the case when meta_metadata has no meta_metadata fields of its own. It just
//            // inherits from super class.
//
//            DictionaryList<String, MetaMetadataField> childMetaMetadata = Kids;
//            if (childMetaMetadata == null)
//            {
//                childMetaMetadata = InitializeChildMetaMetadata();
//            }
//
//            // *do not* override fields in here with fields from super classes.
//
//            MetaMetadataField fieldToInheritTo;
//            childMetaMetadata.TryGetValue(fieldName, out fieldToInheritTo);
//
//            if (fieldToInheritTo is MetaMetadataCollectionField)
//		    {
//			    MetaMetadataCompositeField childComposite = ((MetaMetadataCollectionField) fieldToInheritTo).GetMetaMetadataCompositeField();
//			    if (childComposite != null)
//			    {
//                    MetaMetadataCompositeField inheritedChildComposite = ((MetaMetadataCollectionField)fieldToInheritFrom).GetMetaMetadataCompositeField();
//				
//				    if (MetaMetadataCollectionField.UNRESOLVED_NAME == childComposite.Name)
//				    {
//					    fieldToInheritTo.kids.Remove(MetaMetadataCollectionField.UNRESOLVED_NAME);
//					    childComposite.InheritNonDefaultAttributes(inheritedChildComposite);
//					    childComposite.Name = inheritedChildComposite.Name;
//					    fieldToInheritTo.kids.Add(childComposite.Name, childComposite);
//				    }
//			    }
//		    }
//
//            if (fieldToInheritTo == null)
//            {
//                childMetaMetadata.Add(fieldName, fieldToInheritFrom);
//                fieldToInheritTo = fieldToInheritFrom;
//            }
//            else
//            {
//                fieldToInheritTo.InheritNonDefaultAttributes(fieldToInheritFrom);
//            }
//
//            DictionaryList<String, MetaMetadataField> inheritedChildMetaMetadata = fieldToInheritFrom.Kids;
//            if (inheritedChildMetaMetadata != null)
//            {
//                foreach (MetaMetadataField grandChildMetaMetadataField in inheritedChildMetaMetadata.Values)
//                {
//                    fieldToInheritTo.InheritForField(grandChildMetaMetadataField);
//                }
//            }
//        }

        #region binders

        internal virtual bool GetClassAndBindDescriptors(SimplTypesScope metadataTScope)
        {
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
//	    protected bool BindClassDescriptor(Type metadataClass, SimplTypesScope metadataTScope)
//	    {
//		    MetadataClassDescriptor metadataClassDescriptor = this.metadataClassDescriptor;
//		    if (metadataClassDescriptor == null)
//		    {
//                lock (this)
//                {
//				    metadataClassDescriptor = this.metadataClassDescriptor;
//				    if (metadataClassDescriptor == null)
//				    {
//                        metadataClassDescriptor = (MetadataClassDescriptor)ClassDescriptor.GetClassDescriptor(metadataClass);
//
//                        if (metadataClassDescriptor != null)
//                        {
//                            BindMetadataFieldDescriptors(metadataTScope, metadataClassDescriptor);
//                            this.metadataClassDescriptor = metadataClassDescriptor;
//                        }
//                        else
//                            return false;
//				    }
//                }
//		    }
//            return true;
//	    }

//	    protected void BindMetadataFieldDescriptors(SimplTypesScope metadataTScope, MetadataClassDescriptor metadataClassDescriptor)
//        {
//            if (Kids == null && _bindFieldDescriptorsFinished)
//                return;
//
//            List<MetaMetadataField> nonBindingFields = new List<MetaMetadataField>();
//
//            foreach (MetaMetadataField thatChild in Kids.Values)
//		    {
//			    bool binded = thatChild.bindMetadataFieldDescriptor(metadataTScope, metadataClassDescriptor);
//
//                if (binded)
//                {
//                    if (thatChild is MetaMetadataScalarField)
//                    {
//                        MetaMetadataScalarField scalar = (MetaMetadataScalarField)thatChild;
//                        if (scalar.Filter != null)
//                        {
//                            MetadataFieldDescriptor fd = scalar.MetadataFieldDescriptor;
//                            //fd.RegexPattern = scalar.Filter.RegexPattern;
//                            //fd.RegexReplacement = scalar.Filter.Replace;
//                        }
//                    }
//
//                    if (thatChild.hide)
//                        nonDisplayedFieldNames.Add(thatChild.name);
//                    if (thatChild.shadows != null)
//                        nonDisplayedFieldNames.Add(thatChild.shadows);
//
//                    // recursive descent
//                    if (thatChild.HasChildren() && !thatChild.GetClassAndBindDescriptors(metadataTScope))
//                        nonBindingFields.Add(thatChild);
//                }
//                else
//                    nonBindingFields.Add(thatChild);
//
//                _bindFieldDescriptorsFinished = true;
//		    }
//
//            foreach (MetaMetadataField field in nonBindingFields)
//                Kids.Remove(field.Name);
//        }

        public Boolean HasChildren()
        {
            return kids != null && kids.Count > 0;
        }

//        protected virtual bool bindMetadataFieldDescriptor(SimplTypesScope metadataTScope, MetadataClassDescriptor metadataClassDescriptor)
//        {
//            String fieldName = this.GetFieldNameInCamelCase(false);// TODO -- is this the correct tag?
//            MetadataFieldDescriptor = (MetadataFieldDescriptor)metadataClassDescriptor.GetFieldDescriptorByFieldName(fieldName);
//
//            if (MetadataFieldDescriptor != null)
//            {
//                return true;
//            }
//            else
//            {
//                if(!MetaMetadataRepository.stopTheConsoleDumping)
//                    Console.WriteLine("Ignoring <" + fieldName + "> because no corresponding MetadataFieldDescriptor can be found.");
//                return false;
//            }
//
//        }

        

        #endregion

        public int GetFieldType()
        {
            if (metadataFieldDescriptor != null)
                return metadataFieldDescriptor.FdType;
            Type thisType = GetType();
            if (thisType == typeof(MetaMetadataCompositeField))
                return FieldTypes.CompositeElement;
            if (thisType == typeof(MetaMetadataCollectionField))
            {
                MetaMetadataCollectionField coll = (MetaMetadataCollectionField)this;
                if (coll.ChildScalarType != null)
                    return FieldTypes.CollectionScalar;
                else
                    return FieldTypes.CollectionElement;
            }
            else
                return FieldTypes.Scalar;
        }

        internal Type GetMetadataClass(SimplTypesScope metadataTScope)
        {
            Type result = metadataClass;

		
		    if (result == null)
		    {
			    MetadataClassDescriptor descriptor = this.MetadataClassDescriptor;
			    result= (descriptor == null ? null : descriptor.DescribedClass);
                metadataClass = result;
		    }
            return result;
        }

//        internal void InheritNonDefaultAttributes(MetaMetadataField inheritFrom)
//        {
//            ClassDescriptor classDescriptor = ClassDescriptor;
//
//            foreach (FieldDescriptor fieldDescriptor in classDescriptor.AttributeFieldDescriptors)
//            {
//                ScalarType scalarType = null;//TODO FIXME fieldDescriptor.GetScalarType();
//                try
//                {
//                    if (scalarType != null && scalarType.IsDefaultValue(fieldDescriptor.Field, this)
//                            && !scalarType.IsDefaultValue(fieldDescriptor.Field, inheritFrom))
//                    {
//                        Object value = fieldDescriptor.Field.GetValue(inheritFrom);
//                        fieldDescriptor.Field.SetValue(this, value);
//                        //Console.WriteLine("inherit\t" + this.Name + "." + fieldDescriptor.FieldName + "\t= " + value);
//                    }
//                }
//                catch (Exception e)
//                {
//                    // TODO Auto-generated catch block
//                    Console.WriteLine("inherit\t" + this.Name + "." + fieldDescriptor.Name + " Failed, ignore it.\n" + e);
//                }
//            }
//
//        }

//        private String GetTagForTranslationScope()
//        {
//            return Tag ?? Name;
//        }

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

        public DictionaryList<String, MetaMetadataField> Kids
		{
			get { return kids ?? (kids = new DictionaryList<string, MetaMetadataField>()); }
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

//        public String Type
//        {
//            get { return null; }
//        }

        public MetadataClassDescriptor MetadataClassDescriptor
        {
            get { return metadataClassDescriptor; }
        }

        #endregion
        public object Key()
		{
            return name;
		}

        public MetadataFieldDescriptor MetadataFieldDescriptor 
        {
            get { return metadataFieldDescriptor; } 
            set{ metadataFieldDescriptor = value; } 
        }

	    public MetaMetadataField InheritedField
	    {
	        get { return inheritedField; }
	        set { inheritedField = value; }
	    }

	    public MetaMetadata DeclaringMmd
	    {
	        get { return declaringMmd; }
	        set { declaringMmd = value; }
	    }

	    public HashSet<string> NonDisplayedFieldNames
	    {
	        get { return nonDisplayedFieldNames; }
	        set { nonDisplayedFieldNames = value; }
	    }

	    public string AsNaturalId
	    {
	        get { return asNaturalId; }
	        set { asNaturalId = value; }
	    }

	    public MetaMetadataField LookupChild(String name)
        {
            MetaMetadataField result;
            kids.TryGetValue(name, out result);
            return result;
        }

        public IEnumerator<MetaMetadataField> GetEnumerator()
        {
            return (kids != null) ? kids.Values.GetEnumerator() : EMPTY_ITERATOR;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public String GetDisplayedLabel()
        {
            String result = _displayedLabel;
            if (result == null)
            {
                if (label != null)
                    result = label;
                else
                    result = name.Replace("_", " ");

                _displayedLabel = result;
            }
            return result;
        }

        public bool IsChildFieldDisplayed(String childName)
        {
            return NonDisplayedFieldNames == null ? true : !NonDisplayedFieldNames.Contains(childName);
        }

        /**
         * get the type name of this field, in terms of meta-metadata.
         * 
         * TODO redefining this.
         * 
         * @return the type name.
         */
        abstract public String GetTypeName();

	    public void InheritAttributes(MetaMetadataField inheritFrom)
        {
            var classDescriptor = ClassDescriptor.GetClassDescriptor(this);

            foreach (MetaMetadataFieldDescriptor fieldDescriptor in classDescriptor.AllFieldDescriptors)
            {
                if (fieldDescriptor.IsInheritable)
                {
                    ScalarType scalarType = fieldDescriptor.ScalarType;
                    try
                    {
                        if (scalarType != null
                                && scalarType.IsDefaultValue(fieldDescriptor.Field, this)
                                && !scalarType.IsDefaultValue(fieldDescriptor.Field, inheritFrom))
                        {
                            Object value = fieldDescriptor.Field.GetValue(inheritFrom);
                            fieldDescriptor.Field.SetValue(this, value);
                            //						debug("inherit\t" + this.getName() + "." + fieldDescriptor.getFieldName() + "\t= "
                            //								+ value);
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(inheritFrom.Name + " doesn't have field " + fieldDescriptor.Name + ", ignore it.");
                        //					e.printStackTrace();
                    }
                }
            }
        }


	    public MetadataFieldDescriptor BindMetadataFieldDescriptor(SimplTypesScope metadataTScope, MetadataClassDescriptor metadataClassDescriptor)
	    {
	        MetadataFieldDescriptor metadataFieldDescriptor = this.metadataFieldDescriptor;
		    if (metadataFieldDescriptor == null)
		    {
				metadataFieldDescriptor = this.metadataFieldDescriptor;
				String fieldName = this.GetFieldName(false);
				if (metadataFieldDescriptor == null)
				{
					metadataFieldDescriptor = (MetadataFieldDescriptor) metadataClassDescriptor.GetFieldDescriptorByFieldName(fieldName);
					if (metadataFieldDescriptor != null)
					{
						// FIXME is the following "if" statement still useful? I never see the condition is
						// true. can we remove it? -- yin 7/26/2011
						// if we don't have a field, then this is a wrapped collection, so we need to get the
						// wrapped field descriptor
						if (metadataFieldDescriptor.Field == null)
							metadataFieldDescriptor = (MetadataFieldDescriptor) metadataFieldDescriptor.WrappedFd;

						this.metadataFieldDescriptor = metadataFieldDescriptor;
						
						// this method handles polymorphic type / changing tags
                        //Note FIXME !! Proxies and cloning not yet implemented
//						if (this.metadataFieldDescriptor != null)
//							CustomizeFieldDescriptor(metadataTScope, fieldDescriptorProxy);
						if (this.metadataFieldDescriptor != metadataFieldDescriptor)
						{
							String tagName = this.metadataFieldDescriptor.TagName;
							int fieldType = this.metadataFieldDescriptor.FdType;
							if (fieldType == FieldTypes.CollectionElement || fieldType == FieldTypes.MapElement)
								tagName = this.metadataFieldDescriptor.CollectionOrMapTagName;
							metadataClassDescriptor.AllFieldDescriptorsByTagNames.Put(tagName, this.metadataFieldDescriptor);
						}
					}
				}
				else
				{
					Debug.WriteLine("Ignoring <" + fieldName + "> because no corresponding MetadataFieldDescriptor can be found.");
				}
		    }
		    return metadataFieldDescriptor;
	    }

//        private MetadataFieldDescriptorProxy fieldDescriptorProxy = new MetadataFieldDescriptorProxy();
//
//        private void CustomizeFieldDescriptor(SimplTypesScope metadataTScope, MetadataFieldDescriptorProxy fieldDescriptorProxy)
//	    {
//	        fieldDescriptorProxy.setTagName(Tag ?? Name);
//	    }

	    private string GetFieldName(bool capitalized)
	    {
            if (capitalized)
                return GetCapFieldName();

	        string result = _fieldNameInCSharp;

            if(result == null)
            {
                result = XmlTools.FieldNameFromElementName(Name);
                _fieldNameInCSharp = result;
            }

	        return _fieldNameInCSharp;

	    }

        private String _fieldNameInCSharp = null;
        private String _capFieldNameInCSharp = null;

        private String GetCapFieldName()
        {
            String rst = _capFieldNameInCSharp;
            if (rst == null)
            {
                rst = XmlTools.CamelCaseFromXMLElementName(Name, true);
                _capFieldNameInCSharp = rst;
            }
            return _capFieldNameInCSharp;
        }

        public override string ToString()
        {
            return "MetaMetadata [" + Name + "]";
        }

        /**
	     * this class encapsulate the clone-on-write behavior of metadata field descriptor associated
	     * with this field.
	     * 
	     * @author quyin
	     *
	     */
//        protected internal class MetadataFieldDescriptorProxy
//        {
//            private MetaMetadataField outer;
//            
//            public MetadataFieldDescriptorProxy(MetaMetadataField outer)
//            {
//                this.outer = outer;
//            }
//
//            private void cloneFieldDescriptorOnWrite()
//		    {
//			    if (outer.metadataFieldDescriptor.getDescriptorClonedFrom() == null)
//				    outer.metadataFieldDescriptor = outer.metadataFieldDescriptor.
//		    }
//
//            public void setTagName(String newTagName)
//		    {
//			    if (newTagName != null && !newTagName.equals(MetaMetadataField.this.metadataFieldDescriptor.getTagName()))
//			    {
//				    cloneFieldDescriptorOnWrite();
//				    MetaMetadataField.this.metadataFieldDescriptor.setTagName(newTagName);
//			    }
//		    }
//
//            public void setElementClassDescriptor(MetadataClassDescriptor metadataClassDescriptor)
//		    {
//			    if (metadataClassDescriptor != MetaMetadataField.this.metadataFieldDescriptor.getElementClassDescriptor())
//			    {
//				    cloneFieldDescriptorOnWrite();
//				    MetaMetadataField.this.metadataFieldDescriptor.setElementClassDescriptor(metadataClassDescriptor);
//			    }
//		    }
//
//            public void setCollectionOrMapTagName(String childTag)
//		    {
//			    if (childTag != null && !childTag.equals(MetaMetadataField.this.metadataFieldDescriptor.getCollectionOrMapTagName()))
//			    {
//				    cloneFieldDescriptorOnWrite();
//				    MetaMetadataField.this.metadataFieldDescriptor.setCollectionOrMapTagName(childTag);
//			    }
//		    }
//
//            public void setWrapped(boolean wrapped)
//		    {
//			    if (wrapped != MetaMetadataField.this.metadataFieldDescriptor.isWrapped())
//			    {
//				    cloneFieldDescriptorOnWrite();
//				    MetaMetadataField.this.metadataFieldDescriptor.setWrapped(wrapped);
//			    }
//		    }
//
//        }
    }
    
	
	

}
