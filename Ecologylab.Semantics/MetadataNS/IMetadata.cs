using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ecologylab.Semantics.MetadataNS;
using Ecologylab.Semantics.Model.Text;
using Simpl.Serialization;

namespace Ecologylab.Semantics.MetadataNS
{
    public interface IMetadata : IEnumerable<FieldDescriptor>
    {
        /**
	     * This is actually the real composite term vector.
	     * 
	     * @return	Null for scalars.
	     */
        ITermVector TermVector { get; set; }

	    ITermVector GetTermVector(HashSet<Metadata> visitedMetadata);

	    void Recycle();

        /**
	     * Rebuilds the composite TermVector from the individual TermVectors, when there is one.
	     * This implementation, in the base class, does nothing.
	     */
	    void RebuildCompositeTermVector();

        /**
	     * Says that a type should never contribute terms.
	     * 
	     * @return
	     */
        bool IgnoreInTermVector { get; set; }
    }
}
