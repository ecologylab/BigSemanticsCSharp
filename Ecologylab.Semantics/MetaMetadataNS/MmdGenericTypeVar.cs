using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ecologylab.Semantics.MetadataNS;
using Simpl.Serialization.Attributes;
using Simpl.Serialization;
using System.Collections.ObjectModel;

namespace Ecologylab.Semantics.MetaMetadataNS 
{
    ///<summary>
    ///the generic variable in meta-metadata.
    /// 
    ///@author quyin
    ///</summary> 
    [SimplTag("generic_type_var")]
    [SimplInherit]
    public class MmdGenericTypeVar : ElementState
    {

	    /**
	     * the name of the generic type variable. should be all capitalized.
	     */
	    [SimplScalar]
	    private String									name;

	    /**
	     * the (covariant) bound of the generic type. it could be either a concrete meta-metadata type
	     * name, or an already defined generic type variable name.
	     */
	    [SimplScalar]
	    [SimplTag("extends")]
	    private String									extendsAttribute;

        // TODO [SimplScalar] [SimplTag("super")] private String superAttribute;

	    /**
	     * the type used to instantiate this generic type variable. it could be either a concrete
	     * meta-metadata type name, or an already defined generic type variable name.
	     */
	    [SimplScalar]
	    private String									arg;

	    /**
	     * a scope of nested generic type variables. e.g. A, B in &lt;M extends Media&lt;A, B&gt;&gt;.
	     */
	    [SimplMap("generic_type_var")]
	    [SimplMapKeyField("name")]
	    [SimplNoWrap]
	    private MmdGenericTypeVarScope	nestedGenericTypeVars;

	    public String Name
	    {
            get { return name; }
            set { name = value; }
	    }

	    public String ExtendsAttribute
	    {
            get { return extendsAttribute; }
            set { extendsAttribute = value; }
	    }

	    public String Arg
	    {
            get { return arg; }
            set { arg = value; }
	    }

	    public MmdGenericTypeVarScope NestedGenericTypeVarScope
	    {
            get { return nestedGenericTypeVars; }
	    }
	
	    static ICollection<MmdGenericTypeVar> EMPTY_COLLECTION = new Collection<MmdGenericTypeVar>();

        public ICollection<MmdGenericTypeVar> GetNestedGenericTypeVars()
	    {
            return nestedGenericTypeVars == null ? EMPTY_COLLECTION : nestedGenericTypeVars.Values; 
	    }

        public void SetNestedGenericTypeVars(MmdGenericTypeVarScope nestedGenericTypeVars)
        {
            this.nestedGenericTypeVars = nestedGenericTypeVars;
        }

	    public static String GetMdClassNameFromMmdOrNoChange(String mmdName,
			    MetaMetadataRepository repository, MmdCompilerService compilerService)
	    {
		    MetaMetadata mmd = repository.GetMMByName(mmdName);
		    if (mmd == null)
		    {
			    return mmdName;
		    }
		    else
		    {
			    MetadataClassDescriptor metadataClassDescriptor = mmd.MetadataClassDescriptor;
			    if (compilerService != null)
				    compilerService.AddCurrentClassDependency(metadataClassDescriptor);
			    return metadataClassDescriptor.DescribedClassSimpleName;
		    }
	    }

	    public bool IsAssignment()
	    {
		    return arg != null;
	    }

	    public bool IsBound()
	    {
		    return extendsAttribute != null /* || superAttribute != null */;
	    }

	    public void ResolveArgAndBounds(MmdGenericTypeVarScope genericTypeVarScope)
	    {
		    if (IsAssignment())
		    {
			    MmdGenericTypeVar gtv = genericTypeVarScope.Get(arg);
			    if (gtv != null)
			    {
				    gtv.ResolveArgAndBounds(genericTypeVarScope);

				    if (gtv.IsAssignment())
				    {
					    arg = gtv.arg;
				    }
				    else if (gtv.IsBound())
				    {
					    arg = null;
					    extendsAttribute = gtv.extendsAttribute;
					    // superAttribute = gtv.superAttribute;
				    }
			    }
		    }
		    else if (IsBound())
		    {
			    MmdGenericTypeVar extendsGtv = genericTypeVarScope.Get(extendsAttribute);
			    if (extendsGtv != null)
			    {
				    extendsGtv.ResolveArgAndBounds(genericTypeVarScope);
				    extendsAttribute = extendsGtv.arg != null ? extendsGtv.arg : extendsGtv.extendsAttribute;
			    }
			
			    // TODO superAttribute
		    }
		    else
			    throw new MetaMetadataException(
					    "wrong meta-metadata generic type var type! must either be an assignment or a bound.");
	    }

    }
}
