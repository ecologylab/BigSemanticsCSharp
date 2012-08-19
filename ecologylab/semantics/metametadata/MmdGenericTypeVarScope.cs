using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simpl.Fundamental.Collections;

namespace ecologylab.semantics.metametadata 
{
    ///<summary>
    /// a scope of generic type vars. also handles type checking, generic name resolving, etc.
    /// @author quyin
    ///</summary>
    public class MmdGenericTypeVarScope : MultiAncestorScope<MmdGenericTypeVar>
    {

        public void InheritFrom(MmdGenericTypeVarScope superScope, InheritanceHandler inheritanceHandler)
        {
            if (superScope == null)
                return;

            foreach (MmdGenericTypeVar superGenericTypeVar in superScope.Values)
            {
                String name = superGenericTypeVar.Name;
                MmdGenericTypeVar localGenericTypeVar = this.Get(name);

                if (localGenericTypeVar == null)
                {
                    this.Add(name, superGenericTypeVar);
                }
                else
                {
                    localGenericTypeVar.ResolveArgAndBounds(this);

                    if (superGenericTypeVar.IsAssignment() && localGenericTypeVar.IsAssignment()
                        && !superGenericTypeVar.Arg.Equals(localGenericTypeVar.Arg))
                    {
                        throw new MetaMetadataException("incompatiable assignments to a generic type var: " + name);
                    }
                    else if (superGenericTypeVar.IsAssignment() && localGenericTypeVar.IsBound())
                    {
                        throw new MetaMetadataException("generic type already assigned: " + name);
                    }
                    else if (superGenericTypeVar.IsBound() && localGenericTypeVar.IsAssignment())
                    {
                        CheckAssignmentWithBounds(name, localGenericTypeVar, superGenericTypeVar, inheritanceHandler);
                    }
                    else
                    {
                        CheckBoundsWithBounds(name, localGenericTypeVar, superGenericTypeVar, inheritanceHandler);
                    }
                }
            }
        }

        private void CheckAssignmentWithBounds(String name, MmdGenericTypeVar argGtv,
            MmdGenericTypeVar boundGtv, InheritanceHandler inheritanceHandler)
        {
            MetaMetadata argMmd = inheritanceHandler.ResolveMmdName(argGtv.Arg);
            argMmd.InheritMetaMetadata(null);

            MetaMetadata lowerBoundMmd = inheritanceHandler.ResolveMmdName(boundGtv.ExtendsAttribute);
            lowerBoundMmd.InheritMetaMetadata(null);
            bool satisfyLowerBound = lowerBoundMmd == null || argMmd.IsDerivedFrom(lowerBoundMmd);

            // MetaMetadata upperBoundMmd = inheritanceHandler.resolveMmdName(localGtv.getSuperAttribute());
            // boolean satisfyUpperBound = upperBoundMmd == null || upperBoundMmd.isDerivedFrom(argMmd);

            if (!satisfyLowerBound /* || !satisfyUpperBound */)
                throw new MetaMetadataException("generic type bound(s) not satisfied: " + name);
        }

        private void CheckBoundsWithBounds(String name, MmdGenericTypeVar local, MmdGenericTypeVar other,
            InheritanceHandler inheritanceHandler)
        {
            MetaMetadata lowerBoundMmdLocal = inheritanceHandler.ResolveMmdName(local.ExtendsAttribute);
            lowerBoundMmdLocal.InheritMetaMetadata(null);

            MetaMetadata lowerBoundMmdOther = inheritanceHandler.ResolveMmdName(other.ExtendsAttribute);
            lowerBoundMmdOther.InheritMetaMetadata(null);

            bool lowerBoundsCompatible = lowerBoundMmdOther == null
                || lowerBoundMmdLocal.IsDerivedFrom(lowerBoundMmdOther);

            // TODO upperBoundsCompatible

            if (!lowerBoundsCompatible /* || !upperBoundsCompatible */)
                throw new MetaMetadataException("generic type bound(s) not compatible: " + name);
        }
    }
}
