using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ecologylabSemantics.ecologylab.semantics.actions
{
    public abstract class SemanticOperationNamedArguments
    {
	    /**
	     * Argument which tells the search container for process_search semantic action. Used in 1)
	     * CreateContainerForSearch 2) ProcessSearch 3) QueueDocumentDownload
	     */
	    public static readonly String	CONTAINER								= "container";

	    /**
	     * Argument which tells the link for a contianer. Used in 1) CreateContainerForSearch 2)
	     * CreateContainer 3) ProcessSearch
	     */
	    public static readonly String	LOCATION								= "location";

	    /**
	     * Tells the field value to be set. Used in 1) Setter Semantic action 2) SetMetadata
	     */
	    public static readonly String	FIELD_VALUE							= "field_value";

	    public static readonly String	IMAGE_PURL							= "image_purl";

	    public static readonly String	CAPTION									= "caption";

	    public static readonly String	DESCRIPTION							= "description";

	    public static readonly String	HREF										= "href";

	    public static readonly String	WIDTH										= "width";

	    public static readonly String	HEIGHT									= "height";

	    public static readonly String	HREF_METADATA						= "href_metadata";

	    /**
	     * 1) CreateContainer
	     */
	    public static readonly String	ANCHOR_TEXT							= "anchor_text";

	    /**
	     * 1) CreateContainer
	     */
	    public static readonly String	ANCHOR_CONTEXT					= "anchor_context";

	    /**
	     * 1) CreateContainer
	     */
	    public static readonly String	ENTITY									= "entity";

	    /**
	     * 1) CreateContainer
	     */
	    public static readonly String	DOCUMENT								= "document";

	    public static readonly String	SOURCE_DOCUMENT					= "source_document";

	    public static readonly String	MIXIN										= "mixin";

	    /**
	     * 1) CreateContainer
	     */
	    public static readonly String	CITATION_SIGNIFICANCE		= "citation_sig";

	    /**
	     * 1) CreateContainer
	     */
	    public static readonly String	SIGNIFICANCE_VALUE			= "sig_value";

	    /**
	     * 1) CreateContainer
	     */
	    public static readonly String	TRAVERSABLE							= "traversable";

	    public static readonly String	IGNORE_CONTEXT_FOR_TV		= "ignore_context_for_tv";

	    public static readonly String	INDEX										= "index";

	    public static readonly String	CURRENT_INDEX						= "current_index";

	    public static readonly String	SIZE										= "size";

	    public static readonly String	OUTER_LOOP_INDEX				= "outer_loop_index";

	    public static readonly String	OUTER_LOOP_SIZE					= "outer_loop_size";

	    public static readonly String	NUMBER_OF_TOP_DOCUMENTS	= "number_of_top_documents";

	    /**
	     * CreateVisualizeTextSurrogate
	     */
	    public static readonly String	TEXT										= "text";

	    public static readonly String	CONTEXT									= "context";

	    public static readonly String	HTML_CONTEXT						= "html_context";

	    public static readonly String	SEMANTIC_TEXT						= "semantic_text";

	    public static readonly String	DOMAIN									= "domain";

	    /**
	     * Used by set_field
	     */
	    public static readonly String	VALUE										= "value";

	    public static readonly String	LINK_TYPE								= "link_type";

	    // parse_document
	    public static readonly String	RANK										= "rank";

	    public static readonly String	METADATA								= "metadata";

    }
}
