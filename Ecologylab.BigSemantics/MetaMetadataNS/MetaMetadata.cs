//
//  MetaMetadata.cs
//  s.im.pl serialization
//
//  Generated by DotNetTranslator on 11/16/10.
//  Copyright 2010 Interface Ecology Lab. 
//

using System;
using System.Collections.Generic;
using System.IO;
using Ecologylab.BigSemantics.Actions;
using Ecologylab.BigSemantics.MetadataNS;
using Ecologylab.BigSemantics.PlatformSpecifics;
using Simpl.Fundamental.Generic;
using Simpl.Serialization.Attributes;

using Simpl.Serialization;
using Simpl.Serialization.Types.Element;
using Ecologylab.BigSemantics.MetadataNS.Builtins;

namespace Ecologylab.BigSemantics.MetaMetadataNS 
{
	
	[SimplInherit]
    [SimplDescriptorClasses(new[] { typeof(MetaMetadataClassDescriptor), typeof(MetaMetadataFieldDescriptor) })]
	public class MetaMetadata : MetaMetadataCompositeField, IMappable<String>
	{
	    private const string ROOT_MMD_NAME = "metadata";

	    [SimplScalar]
	    protected string ormInheritanceStrategy;
		
        [SimplNoWrap]
        [SimplCollection("selector")]
		private List<MetaMetadataSelector> selectors;

		[SimplScalar]
		private Boolean dontGenerateClass;

        [SimplCollection]
        [SimplScope(SemanticOperationTranslationScope.ScopeName)]
	    private List<SemanticOperation> beforeSemanticActions;

        [SimplCollection]
        [SimplScope(SemanticOperationTranslationScope.ScopeName)]
        private List<SemanticOperation> semanticActions;

        [SimplCollection]
        [SimplScope(SemanticOperationTranslationScope.ScopeName)]
        private List<SemanticOperation> afterSemanticActions;

        [SimplScalar]
        [MmDontInherit]
	    private Boolean builtIn;

        [SimplScalar]
	    private RedirectHandling redirectHandling;

        [SimplCollection("mixins")]
		[SimplNoWrap]
		private List<String> mixins;

		[SimplScalar]
		private String collectionOf;

	    [SimplCollection("url_generator")] 
        [SimplNoWrap]
        private List<UrlGenerator> urlGenerators;

        [SimplMap("link_with")]
        [SimplNoWrap]
        private Dictionary<String, LinkWith> linkWiths;

        [SimplScalar]
        [SimplHints(new Hint[] { Hint.XmlAttribute })]
        private Visibility visibility;

        private Dictionary<String, MetaMetadataField> naturalIds = new Dictionary<String, MetaMetadataField>();

	    private object file;
	    private SimplTypesScope localMetadataTranslationScope;


	    public MetaMetadata()
		{ }

        protected override string GetMetaMetadataTagToInheritFrom()
        {
            return ExtendsAttribute ?? base.GetMetaMetadataTagToInheritFrom();
        }

        #region Properties


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

	    public List<MetaMetadataSelector> Selectors
	    {
	        get { return selectors; }
	        set { selectors = value; }
	    }

        public Visibility Visibility
	    {
	        get { return visibility; }
	        set { visibility = value; }
	    }

        // FileInfo changed to object to accommodate windows 8
        public object File { get; set; }

	    public Dictionary<string, MetaMetadataField> NaturalIds
	    {
	        get { return naturalIds; }
	        set { naturalIds = value; }
	    }

	    public override bool IsBuiltIn
	    {
	        get { return builtIn; }
	    }

	    public RedirectHandling RedirectHandling
	    {
            get { return redirectHandling; }
            set { redirectHandling = value; }
	    }

	    public List<SemanticOperation> BeforeSemanticActions 
        {
            get { return beforeSemanticActions; }
        }

        public List<SemanticOperation> SemanticActions
        {
            get { return semanticActions; }
        }

        public List<SemanticOperation> AfterSemanticActions
        {
            get { return afterSemanticActions; }
        }

	    #endregion

	    String IMappable<String>.Key()
        {
            return Name;
        }

		public bool IsDerivedFrom(MetaMetadata baseMmd)
	    {
		    MetaMetadata mmd = this;
		    while (mmd != null)
		    {
                if (mmd == baseMmd)
				    return true;
			    mmd = mmd.SuperMmd;
		    }
		    return false;
	    }

        protected override bool InheritMetaMetadataHelper(InheritanceHandler inheritanceHandler)
        {
            inheritanceHandler = new InheritanceHandler(this);

            // init each field's declaringMmd to this (some of them may change during inheritance)
		    foreach (MetaMetadataField field in Kids.Values)
			    field.DeclaringMmd = this;
            
            return base.InheritMetaMetadataHelper(inheritanceHandler);
        }

        protected override void InheritNonFieldElements(MetaMetadata inheritedMmd, InheritanceHandler inheritanceHandler)
        {
            base.InheritNonFieldElements(inheritedMmd, inheritanceHandler);
            InheritAttributes(inheritedMmd);

            if (this.genericTypeVars != null)
                this.genericTypeVars.InheritFrom(inheritedMmd.genericTypeVars, inheritanceHandler);

            //InheritSemanticActions(inheritedMmd);
        }

        protected override void InheritFrom(MetaMetadataRepository repository, MetaMetadataCompositeField inheritedStructure, InheritanceHandler inheritanceHandler)
	    {
            base.InheritFrom(repository, inheritedStructure, inheritanceHandler);
		
		    // for fields referring to this meta-metadata type
		    // need to do inheritMetaMetadata() again after copying fields from this.getInheritedMmd()
/*		    foreach (MetaMetadataField f in this.Kids.Values)
		    {
			    if (f.GetType() ==  typeof(MetaMetadataNestedField))
			    {
				    MetaMetadataNestedField nested = (MetaMetadataNestedField) f;
				    if (nested.InheritedMmd == this)
				    {
					    nested.ClearInheritFinishedOrInProgressFlag();
                        nested.InheritMetaMetadata();
				    }
			    }
		    }
*/
	    }

	    public new MetadataClassDescriptor BindMetadataClassDescriptor(SimplTypesScope metadataTScope)
	    {
            if (metadataClassDescriptor != null)
                return metadataClassDescriptor;

            // create a temporary local metadata translation scope
            SimplTypesScope localMetadataTScope = SimplTypesScope.Get("mmd_local_tscope:" + Name,  metadataTScope );

            // record the initial number of classes in the local translation scope

	        int initialLocalTScopeSize = localMetadataTScope.EntriesByClassName.Count;

	        // do actual stuff ...
	        base.BindMetadataClassDescriptor(localMetadataTScope);

	        // if tag overlaps, or there are fields using classes not in metadataTScope, use localTScope
	        MetadataClassDescriptor thisCd = this.MetadataClassDescriptor;
	        if (thisCd != null)
	        {
	            MetadataClassDescriptor thatCd = (MetadataClassDescriptor)metadataTScope.GetClassDescriptorByTag(thisCd.TagName);
	            if (thisCd != thatCd)
	            {
	                localMetadataTScope.AddTranslation(thisCd);
	                this.localMetadataTranslationScope = localMetadataTScope;
	            }
	            else if (localMetadataTScope.EntriesByClassName.Count > initialLocalTScopeSize)
	                this.localMetadataTranslationScope = localMetadataTScope;
	            else
	                this.localMetadataTranslationScope = metadataTScope;
	        }

	        // return the bound metadata class descriptor
	        return thisCd;
	    }

        protected override MetaMetadata FindOrGenerateInheritedMetaMetadata(MetaMetadataRepository repository, InheritanceHandler inheritanceHandler)
        {
            if (MetaMetadata.IsRootMetaMetadata(this))
                return null;

            MetaMetadata inheritedMmd = this.TypeMmd;
            if (inheritedMmd == null)
            {
                String inheritedMmdName = this.Type;
                if (inheritedMmdName == null)
                {
                    inheritedMmdName = this.ExtendsAttribute;
                    SetNewMetadataClass(true);
                }
                if (inheritedMmdName == null)
                    throw new MetaMetadataException("no type/extends specified: " + this);
                inheritedMmd = (MetaMetadata) this.Scope[inheritedMmdName];
                if (inheritedMmd == null)
                    throw new MetaMetadataException("meta-metadata '" + inheritedMmdName + "' not found.");
                TypeMmd = inheritedMmd;
            }
            return inheritedMmd;
        }

	    private static bool IsRootMetaMetadata(MetaMetadata mmd)
	    {
	        return mmd.Name.Equals(ROOT_MMD_NAME);

	    }


	    protected override String GetMetadataClassName()
        {
            return this.CSharpPackageName + "." + GetMetadataClassSimpleName();
        }

        protected override string GetMetadataClassSimpleName()
	    {
            if (IsBuiltIn || IsNewMetadataClass())
            {
                // new definition
                return XmlTools.CamelCaseFromXMLElementName(Name, true);// ClassNameFromElementName(Name);
            }
            else
            {
                // re-using existing type
                // do not use this.type directly because we don't know if that is a definition or just re-using exsiting type
                MetaMetadata inheritedMmd = SuperMmd;
//                if (inheritedMmd == null)
//                    InheritMetaMetadata(null);//edit // currently, this should never happend because we call this method after inheritance process.
                return inheritedMmd == null ? null : inheritedMmd.GetMetadataClassSimpleName();
            }
	    }

	    public override bool IsNewMetadataClass()
	    {
	        return base.IsNewMetadataClass() && !IsBuiltIn;
	    }

	    public MetadataNS.Metadata ConstructMetadata(SimplTypesScope metadataTScope = null)
	    {
            if (metadataTScope == null)
                metadataTScope = Repository.MetadataTScope;

	        MetadataNS.Metadata result = null;
	        Type metadataClass = GetMetadataClass(metadataTScope);
	        if (metadataClass != null)
	        {
	            Type[] argClasses = new Type[] { typeof(MetaMetadataCompositeField) };
	            object[] argObjects = new object[] { this };
                //result = metadataClass.GetConstructor(argClasses).Invoke(argObjects) as Metadata.Metadata;
                
                //result = SemanticsPlatformSpecifics.Get().InvokeInstance(metadataClass, argClasses, argObjects) as MetadataNS.Metadata;
	            result = ReflectionTools.GetInstance<Metadata>(metadataClass, argObjects);
	            // TODO handle mixins.
	        }
	        return result;
	    }

        public MetaMetadata SuperMmd
        {
            get { return (MetaMetadata) SuperField; }
            set { SuperField = value; }
        }

    }

    public enum RedirectHandling
    {
	    REDIRECT_USUAL,
	    REDIRECT_FOLLOW_DONT_RESET_LOCATION,
	    REDIRECT_FOLLOW_GET_NEW_MM,
	
    }

    public enum Visibility
    {
        GLOBAL,
        PACKAGE,
    }


}
