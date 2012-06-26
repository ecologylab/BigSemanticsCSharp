using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simpl.Fundamental.Collections;

namespace ecologylab.semantics.metametadata 
{

    ///<summary
    /// <para>
    /// Note:
    /// </para>
    ///
    /// <para>
    /// this class is added to separated inheritance logics from MetaMetadata*Field classes, or at least
    /// part of them. implementor of this class should carefully design the separation of different
    /// concerns during the inheritance process, including:
    /// </para>
    /// <para>
    /// map from name to meta-metadata objects
    /// </para>
    /// <para>
    /// inheriting attributes and elements
    /// </para>
    /// </para>
    /// generics
    /// </para>
    /// </para>
    /// ad hoc meta-metadata types
    /// </para>
    /// </para>
    /// local SimplTypesScopes to handle conflicting meta-metadata tag names for direct binding
    /// </para>
    /// </para>
    /// extraction of individual elements in a collection
    /// </para>
    /// <para>
    /// 
    /// this class is now incomplete.
    /// 
    /// @author quyin
    /// </para>
    ///</summary>

    public enum NameType { NONE, MMD, GENERIC };

    public class InheritanceHandler
    {
        //InheritanceComponentNames from java
        public static String MMD_SCOPE = ":mmd_scope:";

        public static String GENERIC_TYPE_VAR_SCOPE = ":generic_type_var_scope:";

	    MetaMetadataRepository	repository;
	
	    MetaMetadata rootMmd;

	    ///<summary>
	    ///this keeps track of meta-metadata field objects.
	    ///</summary>
	    private Stack<MetaMetadataField> mmStack = new Stack<MetaMetadataField>();

	    ///<summary>
	    /// this maintains a stack of scopes containing things that pass from upper level structures to
	    /// lower level structures, e.g. meta-metadata types, generic type vars, etc. somewhat similar
	    /// concept to a lexical scope.
	    ///</summary>
        private Stack<MultiAncestorScope<Object>> scopeStack = new Stack<MultiAncestorScope<Object>>();
	
	    public InheritanceHandler(MetaMetadata rootMmd)
	    {
		    this.rootMmd = rootMmd;
		    this.repository = rootMmd.Repository;
	    }

	    void Push(MetaMetadataField mmField)
	    {
		    Console.Out.WriteLine("pushing " + mmField);
		    mmStack.Push(mmField);

		    // put mmd scope
		    MultiAncestorScope<Object> scope = new MultiAncestorScope<Object>();
		    if (scopeStack.Count > 0)
			    scope.AddAncestor(scopeStack.Peek());
		    scopeStack.Push(scope);

		    // put generic type var scope
		    MmdGenericTypeVarScope existingMmdGenericTypeVarScope = (MmdGenericTypeVarScope) scope.Get(GENERIC_TYPE_VAR_SCOPE);
		    MmdGenericTypeVarScope currentMmdGenericTypeVarScope = mmField.GenericTypeVars;
		    if (currentMmdGenericTypeVarScope != null && existingMmdGenericTypeVarScope != null)
		    {
			    currentMmdGenericTypeVarScope.InheritFrom(existingMmdGenericTypeVarScope, this);
		    }
		    scope.AddIfValueNotNull(GENERIC_TYPE_VAR_SCOPE, mmField.GenericTypeVars);
	    }

	    void Pop(MetaMetadataField mmField)
	    {
		    MetaMetadataField field = mmStack.Pop();
		    Console.Out.WriteLine("popping " + field);
		    scopeStack.Pop();
		    if (mmField != field)
                throw new MetaMetadataException("mmField != field");
	    }

	    public bool CanReset()
	    {
		    // TODO sometimes the object may not be reset.
		    return true;
	    }

	    ///<summary>
	    /// reset the state of the inheritance handler, as if it is newly created. this allows for pooling
	    /// and reusing of the object.
        ///</summary>
	    public void Reset()
	    {
		    // TODO
	    }

	    public MetaMetadata ResolveMmdName(String mmdName)
	    {
		    return ResolveMmdName(mmdName, null);
	    }
	
	    public MetaMetadata ResolveMmdName(String mmdName, NameType[] nameType)
	    {
		    if (mmdName == null)
			    return null;
		    MetaMetadata result = null;
		    MetaMetadataField field = mmStack.Peek();
		    if (nameType != null && nameType.Length > 0)
			    nameType[0] = NameType.NONE;
		
		    // step 1: try to resolve the name as a concrete meta-metadata name, using the mmdScope.
		    if (field is MetaMetadataNestedField)
		    {
			    MetaMetadataNestedField nested = (MetaMetadataNestedField) field;
			    result = nested.MmdScope.Get(mmdName);
			    if (result != null)
				    if (nameType != null && nameType.Length > 0)
					    nameType[0] = NameType.MMD;
		    }

		    // step 2: if step 1 failed, try to use it as a generic type var name
		    if (result == null && mmdName.ToUpper().Equals(mmdName))
		    {
			    List<Object> gtvScopes = scopeStack.Peek().GetAll(GENERIC_TYPE_VAR_SCOPE);
			    foreach (MmdGenericTypeVarScope gtvScope_object in gtvScopes)
			    {
                    if (! (gtvScope_object is MmdGenericTypeVarScope))
                         throw new MetaMetadataException( "Object is not instance of MmdGenericTypeVarScope");
                    MmdGenericTypeVarScope gtvScope = gtvScope_object;

				    MmdGenericTypeVar gtv = gtvScope.Get(mmdName);
				    if (gtv != null)
				    {
					    if (gtv.Arg != null)
						    result = ResolveMmdName(gtv.Arg);
					    else if (gtv.ExtendsAttribute != null)
						    result = ResolveMmdName(gtv.ExtendsAttribute);
					    // TODO superAttribute?
				    }
			    }
			    if (result != null)
				    if (nameType != null && nameType.Length > 0)
					    nameType[0] = NameType.GENERIC;
		    }

		    return result;
	    }
	
	    public bool IsUsingGenerics(MetaMetadataField field)
	    {
		    if (field.GenericTypeVars != null && field.GenericTypeVars.Count > 0)
			    return true;
		    MmdGenericTypeVarScope gtvScope = (MmdGenericTypeVarScope) scopeStack.Peek().Get(
				    GENERIC_TYPE_VAR_SCOPE);
		    if (gtvScope == null)
			    return false;
		    if (gtvScope.ContainsKey(field.GetMmdType()) || field.GetMmdType() == null
				    && gtvScope.ContainsKey(field.Name)
				    || gtvScope.ContainsKey(field.GetMmdExtendsAttribute()))
			    return true;
		    return false;
	    }

	    public override String ToString()
	    {
		    return this.GetType().Name + "[" + rootMmd.Name + "]";
	    }
	
	    public InheritanceHandler clone()
	    {
		    InheritanceHandler cloned = new InheritanceHandler(rootMmd);
		    cloned.repository = this.repository;
		    cloned.mmStack = new Stack<MetaMetadataField>(this.mmStack);
            //cloned.mmStack.addAll (this.mmStack);
            cloned.scopeStack = new Stack<MultiAncestorScope<Object>>(this.scopeStack);
		    //cloned.scopeStack.addAll(this.scopeStack);
		    return cloned;
	    }

    //	public void addGenericTypeVarScope(MmdGenericTypeVarScope genericTypeVarScope)
    //	{
    //		if (scopeStack.size() > 0)
    //		{
    //			MultiAncestorScope scope = scopeStack.peek();
    //			scope.put(GENERIC_TYPE_VAR_SCOPE, genericTypeVarScope);
    //		}
    //	}

    }

}
