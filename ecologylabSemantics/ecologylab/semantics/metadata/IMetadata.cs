using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ecologylab.semantics.metadata;
using ecologylabSemantics.ecologylab.semantics.model.text;
using Simpl.Serialization;

namespace ecologylabSemantics.ecologylab.semantics.metadata
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
