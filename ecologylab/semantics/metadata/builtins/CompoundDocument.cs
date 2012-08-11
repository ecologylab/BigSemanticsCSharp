using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simpl.Serialization.Attributes;
using ecologylab.semantics.metadata.builtins.declarations;
using ecologylab.semantics.metametadata;

namespace ecologylab.semantics.metadata.builtins
{
    [SimplInherit]
    public class CompoundDocument : CompoundDocumentDeclaration
    {

        public CompoundDocument() : base() { }

        public CompoundDocument(MetaMetadataCompositeField metaMetadata) : base(metaMetadata) { }

	    /**
	     * Lazy evaluation of clippings field.
	     * If rootDocument non-null, get and construct in that, as necessary; else get and construct in this, as necessary.
	     * @return
	     */
	    public List<Clipping> GetClippings()
	    {
		    return RootDocument != null ? RootDocument.SelfClippings() : SelfClippings();
	    }

	    private List<Clipping> SelfClippings()
	    {
		    List<Clipping> result = this.Clippings;
		    if (result == null)
		    {
			    result = new List<Clipping>();
			    this.Clippings = result;
		    }
		    return result;
	    }

	    public List<Clipping> GetSelfClippings()
	    {
		    return Clippings;
	    }
	
	    ///<summary>
	    /// Add to collection of clippings, representing our compound documentness.
	    ///</summary>
	    public override void AddClipping(Clipping clipping)
	    {
		    GetClippings().Add(clipping);
	    }

        ///<summary>
	    /// @return	The number of Clippings that have been collected, if any.
        ///</summary>
	    public int NumClippings()
	    {
		    return Clippings == null ? 0 : Clippings.Count;
	    }
    }
}
