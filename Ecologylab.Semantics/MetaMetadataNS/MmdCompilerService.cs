using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simpl.Serialization;

namespace Ecologylab.Semantics.MetaMetadataNS 
{
    ///<summary>
    /// Make it possible for MetaMetadataField objects to use compiler services.
    /// 
    /// @author quyin
    ///</summary>
    public interface MmdCompilerService
    {

	void AddGlobalDependency(String name);

	void AddCurrentClassDependency(ClassDescriptor dependency);

	void AddLibraryTScopeDependency(String name);
	
//	void appendGenericTypeVarParameterizations(Appendable appendable, Collection<MmdGenericTypeVar> mmdGenericTypeVars, MetaMetadataRepository repository);
    }
}
