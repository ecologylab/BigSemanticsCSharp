using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecologylab.BigSemantics.Actions
{

    /// <summary>
    /// This part contains some standard semantic methods for semantic actions. This basically contains
    /// all the high level semantic actions which can be used.
    /// @author amathur
    /// </summary>
    public abstract class SemanticOperationStandardMethods : SemanticOperationKeyWords
    {

        /// <summary>
        /// conditional branching with only one branch, without <code>else</code>. to use <code>else</code>
        /// , you need to use <code>choose</code>.
        /// </summary>
        public static string If = "if";

        /// <summary>
        /// conditional branching with multiply branches.
        /// </summary>
        public static string Choose = "choose";

        /// <summary>
        /// used in <code>choose</code> as the default branch.
        /// </summary>
        public static string Otherwise = "otherwise";

        /// <summary>
        /// looping.
        /// </summary>
        public static string ForEach = "for_each";

        /// <summary>
        /// get field of an object and put it in the variable map so that following actions can access it.
        /// </summary>
        public static string GetFieldAction = "get_field";

        /// <summary>
        /// set field of an object.
        /// </summary>
        public static string SetFieldAction = "set_field";

        /// <summary>
        /// set metadata for an object
        /// </summary>
        public static string SetMetadata = "set_metadata";

        /// <summary>
        /// link handling action.
        /// </summary>
        public static string ParseDocument = "parse_document";

        /// <summary>
        /// image handling action.
        /// </summary>
        public static string CreateAndVisualizeImgSurrogate = "create_and_visualize_img_surrogate";

        /// <summary>
        /// text handling action.
        /// </summary>
        public static string CreateAndVisualizeTextSurrogate = "create_and_visualize_text_surrogate";

        /// <summary>
        /// text handling action.
        /// </summary>
        public static string VisualizeClippings = "visualize_clippings";

        public static string FilterLocation = "filter_location";

        /// <summary>
        /// start a new search.
        /// </summary>
        public static string Search = "search";

        /// <summary>
        /// prevent info collector from obtaining info from a site.
        /// </summary>
        public static string BackOffFromSite = "back_off_from_site";

        public static string CreateSemanticAnchor = "create_semantic_anchor";

        public static string EvaluateRankWeight = "eval_rank_wt";

        public static string GetLinkedMetadata = "get_linked_metadata";

        public static string ReselectMetametadataAndExtract = "reselect_meta_metadata_and_extract";

        public static string AddMixin = "add_mixin";
    }
}
