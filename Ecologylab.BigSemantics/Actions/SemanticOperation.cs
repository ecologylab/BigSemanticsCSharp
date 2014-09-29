//
//  SemanticOperation.cs
//  s.im.pl serialization
//
//  Generated by DotNetTranslator on 11/16/10.
//  Copyright 2010 Interface Ecology Lab. 
//

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Ecologylab.BigSemantics.Collecting;
//using Ecologylab.Semantics.Documentparsers;
using Ecologylab.BigSemantics.Documentparsers;
using Ecologylab.BigSemantics.MetadataNS.Builtins;
using Ecologylab.BigSemantics.MetaMetadataNS;
using Ecologylab.Collections;
using Ecologylab.BigSemantics.MetadataNS;
using Simpl.Fundamental.Net;
using Simpl.Serialization.Attributes;
using Ecologylab.BigSemantics.Connectors;
using Ecologylab.BigSemantics.MetadataNS.Scalar;
using Simpl.Serialization;
using Ecologylab.BigSemantics.Actions;

namespace Ecologylab.BigSemantics.Actions 
{
	/// <summary>
	/// missing java doc comments or could not find the source file.
	/// </summary>
	public abstract class SemanticOperation : ElementState
	{
		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[SimplCollection]
		[SimplScope("condition_scope")]
		[SimplNoWrap]
		private List<Condition> checks;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[SimplNoWrap]
		[SimplMap("arg")]
		private Dictionary<String, Argument> args;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[SimplScalar]
		[SimplTag("object")]
		private String objectStr;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[SimplScalar]
		private String name;

		/// <summary>
		/// missing java doc comments or could not find the source file.
		/// </summary>
		[SimplScalar]
		private String error;

		public List<Condition> Checks
		{
			get{return checks;}
			set{checks = value;}
		}

		public Dictionary<String, Argument> Args
		{
			get{return args;}
			set{args = value;}
		}

		public String ObjectStr
		{
			get{return objectStr;}
			set{objectStr = value;}
		}

		public String Name
		{
			get{return name;}
			set{name = value;}
		}

		public String Error
		{
			get{return error;}
			set{error = value;}
		}



	    protected SemanticsGlobalScope sessionScope;

	    protected SemanticOperationHandler semanticOperationHandler;

	    protected DocumentParser documentParser;

	    public SemanticOperation()
	    {
		    args = new Dictionary<string, Argument>();
	    }

	    public String GetReturnObjectName()
	    {
		    return name;
	    }

	    public bool HasArguments()
	    {
		    return args != null && args.Count > 0;
	    }

	    public String GetArgumentValueName(String argName)
	    {
		    String result = null;
		    if (args != null)
		    {
			    Argument argument = args[argName]; //edit
			    if (argument != null) 
			    {
				    result	= argument.Value;
			    }
		    }
		    return result;
	    }

	    public String GetArgumentAltValueName(String argName)
	    {
		    String result = null;
		    if (args != null)
		    {
			    Argument argument = args[argName]; //edit
			    if (argument != null) 
			    {
				    result	= argument.AltValue;
			    }
		    }
		    return result;
	    }

	    public Object GetArgumentObject(String argName)
	    {
		    Object result			= null;
		    if (args != null)
		    {
			    if (args.ContainsKey(argName))
			    {
                    Argument argument = args[argName];
				    String argumentValueName 					= argument.Value;
				    if (argumentValueName != null)
				    {
                        Scope<Object> semanticOperationVariableMap = semanticOperationHandler.SemanticOperationVariableMap;
                        result = semanticOperationVariableMap.Get(argumentValueName); //edit				
					    if (result == null)
					    {
						    argumentValueName = argument.AltValue;
						    if (argumentValueName != null)
						    {
                                result = semanticOperationVariableMap.Get(argumentValueName); //edit				
						    }					
					    }
				    }
				    if (result != null && result is MetadataScalarBase<Object>)
					    result = ((MetadataScalarBase<Object>) result).Value; //edit
			    }
		    }
		    return result;
	    }

	    public int GetArgumentInteger(String argName, int defaultValue)
	    {
		    int? value = (int?) GetArgumentObject(argName);
		    return value.GetValueOrDefault();
	    }

	    public bool GetArgumentBoolean(String argName, bool defaultValue)
	    {
		    bool? value = (bool?) GetArgumentObject(argName);
		    return value.GetValueOrDefault();
	    }

	    public float GetArgumentFloat(String argName, float defaultValue)
	    {
		    float? value = (float?) GetArgumentObject(argName);
		    return value.GetValueOrDefault();
	    }

	    public SemanticsGlobalScope SessionScope
	    {
		    set { this.sessionScope = value; }
	    }

        public SemanticOperationHandler SemanticOperationHandler
	    {
            get { return this.semanticOperationHandler; }
            set { this.semanticOperationHandler = value; }
	    }

	    public DocumentParser DocumentParser
	    {
		    set { this.documentParser = documentParser; }
	    }

	    ///<summary>
        /// return the name of the operation.
	    ///</summary>
	    public abstract String GetOperationName();

	    ///<summary>
        /// handle error during operation performing.
	    ///<summary>
	    public abstract void HandleError();

	    ///<summary>
	    /// Perform this semantic operation. User defined semantic operations should override this method.
	    /// 
	    /// @param obj
	    ///          The object the operation operates on.
        /// @return The result of this semantic operation (if any), or null.
	    ///<summary>
	    public abstract Object Perform(Object obj); //throw IOException;

	    ///<summary>
	    /// Register a user defined semantic operation to the system. This method should be called before
	    /// compiling or using the MetaMetadata repository.
	    /// <p />
	    /// To override an existing semantic operation, subclass your own semantic operation class, use the same
	    /// tag (indicated in @simpl_tag), and override perform().
	    /// 
        /// @param semanticOperationClass
	    /// @param canBeNested
	    ///          indicates if this semantic operation can be nested by other semantic operations, like
	    ///          <code>for</code> or <code>if</code>. if so, it will also be registered to
        ///          NestedSemanticOperationTranslationScope.
        ///</summary>
        public static void Register(Type[] semanticOperationClasses) //edit
	    {
            foreach (Type semanticOperationClass in semanticOperationClasses)
		    {
                SemanticOperationTranslationScope.Get().AddTranslation(semanticOperationClass);
		    }
	    }
	
	    protected MetaMetadata GetMetaMetadata()
	    {
		    return GetMetaMetadata(this);
	    }
	
	    static public MetaMetadata GetMetaMetadata(ElementState that)
	    {
		    if (that is MetaMetadata)
		    {
			    return (MetaMetadata) that;
		    }
		    ElementState parent	= that.Parent;
		
		    return (parent == null) ? null : GetMetaMetadata(parent);
	    }

	    protected Document ResolveSourceDocument()
	    {
            Document sourceDocument = (Document)GetArgumentObject(SemanticOperationNamedArguments.SourceDocument);
            if (sourceDocument == null)
            {
                sourceDocument = this.semanticOperationHandler.SemanticOperationVariableMap[SemanticOperationKeyWords.Metadata] as Document;
                //sourceDocument = documentParser.Document;
            }

	        return sourceDocument;
	    }

        public Document GetOrCreateDocument(DocumentParser documentParser/*, LinkType linkType*/)
        {
            Document result = (Document)GetArgumentObject(SemanticOperationNamedArguments.Document);
            // get the ancestor container
            Document sourceDocument = ResolveSourceDocument();

            // get the seed. Non null only for search types .
            //Seed seed = documentParser.getSeed();					

            if (result == null)
            {
                Object outlinkPurlObject = GetArgumentObject(SemanticOperationNamedArguments.Location);

                if (outlinkPurlObject != null)
                {
                    ParsedUri outlinkPurl = ((MetadataParsedURL)outlinkPurlObject).Value;
                    //result = sessionScope.GetOrConstructDocument(outlinkPurl);
                }
            }

            if (result == null)
                result = sourceDocument;	//direct binding?!

            if (result != null /*&& !result.IsRecycled()*/ && (result.Location != null))
            {
                result.SemanticsSessionScope = (SemanticsSessionScope)sessionScope;

                MetadataNS.Metadata mixin = (MetadataNS.Metadata)GetArgumentObject(SemanticOperationNamedArguments.Mixin);
                if (mixin != null)
                    result.AddMixin(mixin);

                /*			    if (seed != null)
                                {
                                    seed.bindToDocument(result);
                                }
                */
                MetadataString anchorText = (MetadataString)GetArgumentObject(SemanticOperationNamedArguments.AnchorText);
                // create anchor text from Document title if there is none passed in directly, and we won't
                // be setting metadata
               // if (anchorText == null)
                //    anchorText = result.Title;

                // work to avoid honey pots!
                MetadataString anchorContextString = (MetadataString)GetArgumentObject(SemanticOperationNamedArguments.AnchorContext);
                bool citationSignificance = GetArgumentBoolean(SemanticOperationNamedArguments.CitationSignificance, false);
                float significanceVal = GetArgumentFloat(SemanticOperationNamedArguments.SignificanceValue, 1);
                bool traversable = GetArgumentBoolean(SemanticOperationNamedArguments.Traversable, true);
                bool ignoreContextForTv = GetArgumentBoolean(SemanticOperationNamedArguments.IgnoreContextForTv, false);

                ParsedUri location = result.Location.Value;

                /*			    if (traversable)
                                {
                                    Seeding seeding = sessionScope.getSeeding();
                                    if (seeding != null)
                                        seeding.traversable(location);
                                }
                                bool anchorIsInSource = false;
                                if (sourceDocument != null)
                                {
                                    // Chain the significance from the ancestor
                                    SemanticInLinks sourceInLinks = sourceDocument.getSemanticInlinks();
                                    if (sourceInLinks != null)
                                    {
                                        significanceVal *= sourceInLinks.meanSignificance();
                                        anchorIsInSource = sourceInLinks.containsKey(location);
                                    }
                                }
                                if(! anchorIsInSource)
                                {
                                    //By default use the boost, unless explicitly stated in this site's MMD
                                    SemanticsSite site = result.GetSite;
                                    boolean useSemanticBoost = !site.ignoreSemanticBoost();
                                    if (citationSignificance)
                                        linkType	= LinkType.CITATION_SEMANTIC_ACTION;
                                    else if (useSemanticBoost && linkType == LinkType.OTHER_SEMANTIC_ACTION)
                                        linkType	= LinkType.SITE_BOOSTED_SEMANTIC_ACTION;
                                    SemanticAnchor semanticAnchor = new SemanticAnchor(linkType, location, null,
                                            sourceDocument.getLocation(), significanceVal);// this is not fromContentBody,
                                    // but is fromSemanticActions
                                    if(ignoreContextForTv)
                                        semanticAnchor.addAnchorContextToTV(anchorText, null);
                                    else
                                        semanticAnchor.addAnchorContextToTV(anchorText, anchorContextString);
                                    result.addSemanticInlink(semanticAnchor, sourceDocument);
                                }
                                else
                                {
                                    Console.WriteLine("Ignoring inlink, because ancestor contains the same, we don't want cycles in the graph just yet: sourceContainer -- outlinkPurl :: " + sourceDocument + " -- " + location);
                                }*/
            }
            // adding the return value to map
            Scope<Object> semanticActionVariableMap = SemanticOperationHandler.SemanticOperationVariableMap;
            if (semanticActionVariableMap != null)
            {
                if (GetReturnObjectName() != null)
                    semanticActionVariableMap.Put(GetReturnObjectName(), result);
            }
            else
                Debug.WriteLine("semanticActionVariableMap is null !! Not frequently reproducible either. Place a breakpoint here to try fixing it next time.");
            // set flags if any
            // setFlagIfAny(action, localContainer);

            // return the value. Right now no need for it. Later it might be used.
            return result;
        }

	    public static readonly int INIT = 0; // this operation has not been started
	    public static readonly int INTER = 10; // this operation has been started but not yet finished
	    public static readonly int FIN = 20; // this operation has already been finished
	
	    public virtual void SetNestedOperationState(String name, Object value)
	    {
		
	    }
	
	    public SemanticOperationHandler GetSemanticOperationHandler()
	    {
            SemanticOperationHandler result = this.semanticOperationHandler;
		    if (result == null)
		    {
			    ElementState parentES = Parent;
			    if (parentES != null && parentES is SemanticOperation)
			    {
				    SemanticOperation parent = (SemanticOperation) parentES;
				    result	= parent.GetSemanticOperationHandler();
                    this.semanticOperationHandler = result;
			    }
		    }
		    return result;
	    }
	}
}