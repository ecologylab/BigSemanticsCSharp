//
//  MetaMetadataCompositeField.cs
//  s.im.pl serialization
//
//  Generated by DotNetTranslator on 11/16/10.
//  Copyright 2010 Interface Ecology Lab. 
//

using System;
using System.Collections.Generic;
using Simpl.Fundamental.Generic;
using Simpl.Serialization.Attributes;
using ecologylab.semantics.actions;
using ecologylab.semantics.connectors;
using ecologylab.semantics.metadata;

using Simpl.Serialization;

namespace ecologylab.semantics.metametadata 
{
	/// <summary>
	/// missing java doc comments or could not find the source file.
	/// </summary>
	[SimplInherit]
	[SimplTag("composite")]
	public class MetaMetadataCompositeField : MetaMetadataNestedField
	{
		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[SimplScalar]
		private String type;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[SimplScalar]
		private Boolean entity;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[SimplScalar]
		private String userAgentName;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[SimplScalar]
		private String userAgentString;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[SimplScalar]
		private String parser;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[SimplCollection]
		[SimplScope("semantic_action_translation_scope")]
		private List<SemanticAction<InfoCollector, SemanticActionHandler>> semanticActions;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[SimplCollection("def_var")]
		[SimplNoWrap]
		private List<DefVar> defVars;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[SimplScalar]
		private Boolean reloadPageFirstTime;

		public MetaMetadataCompositeField()
		{ }

        public MetaMetadataCompositeField(String name, DictionaryList<String, MetaMetadataField> kids)
        {
            this.Name = name;
            this.Kids = new DictionaryList<String, MetaMetadataField>();
            if (kids != null)
                this.kids.PutAll(kids);
        }

        protected override string GetMetaMetadataTagToInheritFrom()
        {
            if (Entity)
                return DocumentParserTagNames.ENTITY;
            else if (type != null)
                return type;
            else
                return null;
        }

		public String Type
		{
			get{return type;}
			set{type = value;}
		}

		public Boolean Entity
		{
			get{return entity;}
			set{entity = value;}
		}

		public String UserAgentName
		{
			get{return userAgentName;}
			set{userAgentName = value;}
		}

		public String UserAgentString
		{
			get{return userAgentString;}
			set{userAgentString = value;}
		}

		public String Parser
		{
			get{return parser;}
			set{parser = value;}
		}

        public List<SemanticAction<InfoCollector, SemanticActionHandler>> SemanticActions
		{
			get{return semanticActions;}
			set{semanticActions = value;}
		}

		public List<DefVar> DefVars
		{
			get{return defVars;}
			set{defVars = value;}
		}

		public Boolean ReloadPageFirstTime
		{
			get{return reloadPageFirstTime;}
			set{reloadPageFirstTime = value;}
		}

        public String GetTypeOrName()
        {
            return Type ?? Name;
        }

        public override MetaMetadataCompositeField getMetaMetadataCompositeField()
        {
            return this;
        }

        internal override bool GetClassAndBindDescriptors(SimplTypesScope metadataTScope)
        {
            Type metadataClass = GetMetadataClass(metadataTScope);
            if (metadataClass == null)
            {
                //ElementState parent = Parent;
                //Type parentType = parent.GetType();
                //if (parent is MetaMetadataField)
                //{
                //    //(((MetaMetadataField)parent).kids).Remove(this.Name);
                //}
                //else if (parent is MetaMetadataRepository)
                //{
                //    // TODO remove from the repository level
                //}
                return false;
            }
            //
            return BindClassDescriptor(metadataClass, metadataTScope);
        }
	}
}
