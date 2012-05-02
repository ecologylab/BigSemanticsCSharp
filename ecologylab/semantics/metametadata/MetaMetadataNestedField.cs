//
//  MetaMetadataNestedField.cs
//  s.im.pl serialization
//
//  Generated by DotNetTranslator on 11/16/10.
//  Copyright 2010 Interface Ecology Lab. 
//

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Simpl.Fundamental.Collections;
using Simpl.Fundamental.Generic;
using Simpl.Serialization;
using Simpl.Serialization.Attributes;
using ecologylab.semantics.metadata;


namespace ecologylab.semantics.metametadata 
{
    [SimplInherit]
    public abstract class MetaMetadataNestedField : MetaMetadataField
    {
        private const string AsemblyQualifiedNameForGeneratedSemantics = ", ecologylabGeneratedSemantics, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";

        [SimplScalar]
        [SimplTag("package")] 
        private String packageName;

        [SimplComposite]
        [SimplTag("field_parser")]
        private FieldParserElement fieldParserElement;

        [SimplScalar]
        private Boolean promoteChildren;

        [SimplScalar]
        private String polymorphicScope;

        [SimplScalar]
        private String polymorphicClasses;

        [SimplScalar]
        private String schemaOrgItemtype;

        [SimplCollection("generic_type_var")]
        [SimplNoWrap]
        private List<MetaMetadataGenericTypeVar> genericTypeVars;

        [SimplScalar]
        private bool showExpandedInitially;

        /**
         * the mmd used by this nested field. corresponding attributes: (child_)type/extends. could be a
         * generated one for inline definitions.
         */
        private MetaMetadata inheritedMmd;

        /**
         * should we generate a metadata class descriptor for this field. used by the compiler.
         */
        private bool _isNewMetadataClass;

        private bool    mmdScopeTraversed;
        
        protected bool  inheritMetaMetadataFinished = false;
        private bool    _inheritInProcess;

        /*
         * Delegate called when inheritance is finished. Used when we have cycles, and must wait for until inheritance is finished.
         * e.g. References in ScholaryArticle.
         */
        public delegate void InheritFinishedEventHandler(MetaMetadataNestedField sender, EventArgs e);

        public event InheritFinishedEventHandler InheritFinished;

        protected Stack<MetaMetadataNestedField> _waitingToInheritFrom;

        protected MetaMetadataNestedField()
        {
        }

        protected abstract String GetMetaMetadataTagToInheritFrom();

        public void InheritMetaMetadata()
        {
            if (inheritFinished || _inheritInProcess) return;

            //Debug.WriteLine("inheriting " + this);
            _inheritInProcess = true;
            if(InheritMetaMetadataHelper())
                FinishInheritance();
        }

        protected void FinishInheritance()
        {
            this.SortForDisplay();

            _inheritInProcess = false;
            inheritFinished = true;

            if (InheritFinished != null)
                InheritFinished(this, EventArgs.Empty);
        }

        public void AddInheritanceFinishHandler(MetaMetadataNestedField inheritingField, InheritFinishedEventHandler eventHandler)
        {
            if (_waitingToInheritFrom == null)
                _waitingToInheritFrom = new Stack<MetaMetadataNestedField>();

            _waitingToInheritFrom.Push(inheritingField);

            inheritingField.InheritFinished += eventHandler;
        }

        protected abstract bool InheritMetaMetadataHelper();

        public String PackageName
        {
            get { return packageName; }
            set { packageName = value; }
        }

        public FieldParserElement FieldParserElement
        {
            get { return fieldParserElement; }
            set { fieldParserElement = value; }
        }

        public Boolean PromoteChildren
        {
            get { return promoteChildren; }
            set { promoteChildren = value; }
        }

        public bool ShowExpandedInitially
        {
            get { return showExpandedInitially; }
            set { showExpandedInitially = value; }
        }

        public String PolymorphicScope
        {
            get { return polymorphicScope; }
            set { polymorphicScope = value; }
        }

        public String PolymorphicClasses
        {
            get { return polymorphicClasses; }
            set { polymorphicClasses = value; }
        }

        public String SchemaOrgItemtype
        {
            get { return schemaOrgItemtype; }
            set { schemaOrgItemtype = value; }
        }

	    public MultiAncestorScope<MetaMetadata> MmdScope
        { get; set; }
        public MetaMetadata InheritedMmd
        {
            get { return inheritedMmd; }
            set { inheritedMmd = value; }
        }

        public virtual bool IsNewMetadataClass()
        {
            return _isNewMetadataClass;
        }

        public void SetNewMetadataClass(bool b)
        {
            _isNewMetadataClass = b;
        }

        public bool MmdScopeTraversed
        {
            get { return mmdScopeTraversed; }
            set { mmdScopeTraversed = value; }
        }

        private void InheritNonFieldComponentsFromMM(MetaMetadata inheritedMetaMetadata)
        {
            //Console.WriteLine("InheritNonFieldComponentsFromMM, doing nothing");
        }

        public abstract MetaMetadataCompositeField GetMetaMetadataCompositeField();

        public override String GetTypeName()
        {
            String result = null;
            Type thisType = GetType();
            if (thisType == typeof (MetaMetadataCompositeField))
            {
                MetaMetadataCompositeField mmcf = (MetaMetadataCompositeField) this;
                if (mmcf.Type != null)
                    result = mmcf.Type;
            }
            else if (thisType == typeof (MetaMetadataCollectionField))
            {
                MetaMetadataCollectionField mmcf = (MetaMetadataCollectionField) this;
                if (mmcf.ChildType != null)
                    result = mmcf.ChildType;
                else if (mmcf.ChildScalarType != null)
                    result = mmcf.ChildScalarType.CSharpTypeName;
            }

            if (result == null)
            {
                MetaMetadataField inherited = InheritedField;
                if (inherited != null)
                {
                    // use inherited field's type
                    result = inherited.GetTypeName();
                }
            }

            return result ?? (Name);
        }

        protected internal virtual MetadataClassDescriptor BindMetadataClassDescriptor(SimplTypesScope metadataTScope)
        {
            MetadataClassDescriptor metadataCd = this.MetadataClassDescriptor;
            if (metadataCd == null)
            {
                this.InheritMetaMetadata();

                String metadataClassSimpleName = this.GetMetadataClassSimpleName();
                // first look up by simple name, since package names for some built-ins are wrong

                metadataCd =
                    (MetadataClassDescriptor) metadataTScope.EntriesByClassSimpleName.Get(metadataClassSimpleName);
                if (metadataCd == null)
                {
                    String metadataClassName = this.GetMetadataClassName();

                    metadataCd = (MetadataClassDescriptor) metadataTScope.EntriesByClassName.Get(metadataClassName);
                    if (metadataCd == null)
                    {
                        try
                        {
                            Type metadataType = Type.GetType(metadataClassName) ??
                                                Type.GetType(metadataClassName +
                                                             AsemblyQualifiedNameForGeneratedSemantics);
                            this.metadataClass = metadataType;
                            if (MetadataClass != null)
                            {
                                metadataCd = (MetadataClassDescriptor) ClassDescriptor.GetClassDescriptor(MetadataClass);
                                metadataTScope.AddTranslation(MetadataClass);
                            }
                            else
                            {
                                Debug.WriteLine("Cannot find metadata class: " + metadataClassName);
                            }

                        }
                        catch (Exception e)
                        {
                            //						e.printStackTrace();
                            //						throw new MetaMetadataException("Cannot find metadata class: " + metadataClassName);
                            Debug.WriteLine("Cannot find metadata class: " + metadataClassName);
                        }
                    }
                }

                if (metadataCd != null)
                {
                    this.MetadataClassDescriptor = metadataCd; // early assignment to prevent infinite loop
                    this.BindMetadataFieldDescriptors(metadataTScope, metadataCd);
                }
            }
            return metadataCd;
        }

        /**
	 * bind metadata field descriptors to sub-fields of this nested field, with field names as keys,
	 * but without mixins field.
	 * <p>
	 * sub-fields that lack corresponding field descriptors will be removed from this nested field.
	 * <p>
	 * note that this field no longer uses a boolean flag to prevent multiple invocation. this should
	 * have been done by the bindClassDescriptor() method.
	 * 
	 * @param metadataTScope
	 *          the translation scope of (generated) metadata classes.
	 * @param metadataClassDescriptor
	 *          the metadata class descriptor where field descriptors can be found.
	 */

        protected void BindMetadataFieldDescriptors(SimplTypesScope metadataTScope,
                                                    MetadataClassDescriptor metadataClassDescriptor)
        {
            // copy the kids collection first to prevent modification to the collection during iteration (which may invalidate the iterator).
            List<MetaMetadataField> fields = new List<MetaMetadataField>(Kids.Values);
            foreach (MetaMetadataField thatChild in fields)
            {
                // look up by field name and bind
                MetadataFieldDescriptor metadataFieldDescriptor = thatChild.BindMetadataFieldDescriptor(metadataTScope,
                                                                                                        metadataClassDescriptor);
                if (metadataFieldDescriptor == null)
                {
                    Debug.WriteLine("Cannot bind metadata field descriptor for " + thatChild);
                    this.kids.Remove(thatChild.Name);
                    continue;
                }

                // process hide and shadows
                HashSet<String> nonDisplayedFieldNames = NonDisplayedFieldNames;
                if (thatChild.Hide)
                    nonDisplayedFieldNames.Add(thatChild.Name);
                if (thatChild.Shadows != null)
                    nonDisplayedFieldNames.Add(thatChild.Shadows);

                // recursively process sub-fields
                if (thatChild is MetaMetadataScalarField)
                {
                    // process regex filter
                    MetaMetadataScalarField scalar = (MetaMetadataScalarField) thatChild;
                    if (scalar.Filter != null)
                    {
                        MetadataFieldDescriptor fd = scalar.MetadataFieldDescriptor;
                        if (fd != null)
                        {
                            fd.FilterRegex = scalar.Filter.RegexPattern;
                            fd.FilterReplace = scalar.Filter.Replace;
                        }

                        else
                            Debug.WriteLine("Encountered null fd for scalar: " + scalar);
                    }
                }
                else if (thatChild is MetaMetadataNestedField && thatChild.HasChildren())
                {
                    // bind class descriptor for nested sub-fields
                    MetaMetadataNestedField nested = (MetaMetadataNestedField) thatChild;
                    MetadataFieldDescriptor fd = nested.MetadataFieldDescriptor;
                    if (fd.IsPolymorphic)
                    {
                        Debug.WriteLine("Polymorphic field: " + nested + ", not binding an element class descriptor.");
                    }
                    else
                    {
                        MetadataClassDescriptor elementClassDescriptor =
                            ((MetaMetadataNestedField) thatChild).BindMetadataClassDescriptor(metadataTScope);
                        if (elementClassDescriptor != null)
                        {
                            MetaMetadata mmdForThatChild = nested.InheritedMmd;
                            if (mmdForThatChild != null && mmdForThatChild.MetadataClassDescriptor == null)
                                //							mmdForThatChild.setMetadataClassDescriptor(elementClassDescriptor);
                                mmdForThatChild.BindMetadataClassDescriptor(metadataTScope);
                        }
                        else
                        {
                            Debug.WriteLine("Cannot determine elementClassDescriptor for " + thatChild);
                            this.Kids.Remove(thatChild.Name);
                        }
                    }
                }

                if (this is MetaMetadata)
                {
                    MetaMetadata mmd = (MetaMetadata) this;
                    String naturalId = thatChild.AsNaturalId;
                    if (naturalId != null)
                    {
                        mmd.NaturalIds.Put(naturalId, thatChild);
                    }
                }
            }
        }

        protected virtual string GetMetadataClassName()
        {
            return InheritedMmd.GetMetadataClassName();
        }

        protected virtual string GetMetadataClassSimpleName()
        {
            return InheritedMmd.GetMetadataClassSimpleName();
        }

        public void ClearInheritFinishedOrInProgressFlag()
        {
            inheritFinished = false;
            _inheritInProcess = false;
        }

        /// <summary>
        /// to determine if this field is polymorphic inherently, that is, a field which we don't have
	    /// prior knowledge of its specific meta-metadata type when its encompassing meta-metadata is used.
	    ///  <p />
	    /// NOTE THAT this is different from {@code isPolymorphicInDescendantFields()} which determines if
    	/// this field is used for extended types in descendant fields. in that case although the field is
	    /// polymorphic, too, but we can determine the specific meta-metadata type for this field if the
        /// encompassing meta-metadata is used.
        /// </summary>
        public Boolean IsPolymorphicInherently
        {
            get
            {
                return !string.IsNullOrEmpty(polymorphicScope) || !string.IsNullOrEmpty(polymorphicClasses); 
            }
        }

        public Boolean InheritInProcess
        {
            get { return this._inheritInProcess; }
            set { this._inheritInProcess = value; }
        }

}
}
