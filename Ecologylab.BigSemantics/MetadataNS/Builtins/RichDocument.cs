using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ecologylab.BigSemantics.MetaMetadataNS;
using Ecologylab.BigSemantics.MetadataNS.Builtins.Declarations;
using Simpl.Serialization.Attributes;

namespace Ecologylab.BigSemantics.MetadataNS.Builtins
{
    [SimplInherit]
    public class RichDocument : RichDocumentDeclaration
    {

        public RichDocument() : base() { }

        public RichDocument(MetaMetadataCompositeField metaMetadata) : base(metaMetadata) { }

	    /**
	     * Lazy evaluation of clippings field.
	     * If rootDocument non-null, get and construct in that, as necessary; else get and construct in this, as necessary.
	     * @return
	     */
	    public List<IClipping<Metadata>> GetClippings()
	    {

		    List<IClipping<Metadata>> result = this.Clippings;
		    if (result == null)
		    {
			    result = new List<IClipping<Metadata>>();
			    this.Clippings = result;
		    }
		    return result;
	    }

	    private List<IClipping<Metadata>> SelfClippings()
	    {
		    List<IClipping<Metadata>> result = this.Clippings;
		    if (result == null)
		    {
			    result = new List<IClipping<Metadata>>();
			    this.Clippings = result;
		    }
		    return result;
	    }

	    public List<IClipping<Metadata>> GetSelfClippings()
	    {
		    return Clippings;
	    }
	
	    ///<summary>
	    /// Add to collection of clippings, representing our compound documentness.
	    ///</summary>
	    public override void AddClipping(IClipping<Metadata> clipping)
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

        public override String ToString()
        {
            return (Title != null) ? Title.Value : base.ToString();
        }
    }
}
