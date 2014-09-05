using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecologylab.BigSemantics.Actions
{
    public abstract class SemanticOperationNamedArguments
    {
        /// <summary>
        /// Argument which tells the search container for process_search semantic action. Used in 1)
        /// CreateContainerForSearch 2) ProcessSearch 3) QueueDocumentDownload
        /// </summary>
        public static readonly string Container = "container";

        /// <summary>
        /// Argument which tells the link for a contianer. Used in 1) CreateContainerForSearch 2)
        /// CreateContainer 3) ProcessSearch
        /// </summary>
        public static readonly string Location = "location";

        /// <summary>
        /// Tells the field value to be set. Used in 1) Setter Semantic action 2) SetMetadata
        /// </summary>
        public static readonly string FieldValue = "field_value";

        public static readonly string ImagePurl = "image_purl";

        public static readonly string Caption = "caption";

        public static readonly string Description = "description";

        public static readonly string Href = "href";

        public static readonly string Width = "width";

        public static readonly string Height = "height";

        public static readonly string HrefMetadata = "href_metadata";

        /// <summary>
        /// 1) CreateContainer
        /// </summary>
        public static readonly string AnchorText = "anchor_text";

        /// <summary>
        /// 1) CreateContainer
        /// </summary>
        public static readonly string AnchorContext = "anchor_context";

        /// <summary>
        /// 1) CreateContainer
        /// </summary>
        public static readonly string Entity = "entity";

        /// <summary>
        /// 1) CreateContainer
        /// </summary>
        public static readonly string Document = "document";

        public static readonly string SourceDocument = "source_document";

        public static readonly string Mixin = "mixin";

        /// <summary>
        /// 1) CreateContainer
        /// </summary>
        public static readonly string CitationSignificance = "citation_sig";

        /// <summary>
        /// 1) CreateContainer
        /// </summary>
        public static readonly string SignificanceValue = "sig_value";

        /// <summary>
        /// 1) CreateContainer
        /// </summary>
        public static readonly string Traversable = "traversable";

        public static readonly string IgnoreContextForTv = "ignore_context_for_tv";

        public static readonly string Index = "index";

        public static readonly string CurrentIndex = "current_index";

        public static readonly string Size = "size";

        public static readonly string OuterLoopIndex = "outer_loop_index";

        public static readonly string OuterLoopSize = "outer_loop_size";

        public static readonly string NumberOfTopDocuments = "number_of_top_documents";

        /// <summary>
        /// CreateVisualizeTextSurrogate
        /// </summary>
        public static readonly string Text = "text";

        public static readonly string Context = "context";

        public static readonly string HtmlContext = "html_context";

        public static readonly string SemanticText = "semantic_text";

        public static readonly string Domain = "domain";

        /// <summary>
        /// Used by set_field
        /// </summary>
        public static readonly string Value = "value";

        public static readonly string LinkType = "link_type";

        /// <summary>
        /// parse_document
        /// </summary>
        public static readonly string Rank = "rank";

        public static readonly string Metadata = "metadata";

    }
}
