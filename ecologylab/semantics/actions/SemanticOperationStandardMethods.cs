using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ecologylabSemantics.ecologylab.semantics.actions
{

    public abstract class SemanticOperationStandardMethods : SemanticOperationKeyWords
    {

        /**
         * StandardMethods
         * 
         * This part contains some standard semantic methods for semantic actions. This basically contains
         * all the high level semantic actions which can be used.
         * 
         * @author amathur
         * 
         */

        ///<summary>
        /// conditional branching with only one branch, without <code>else</code>. to use <code>else</code>
        /// , you need to use <code>choose</code>.
        ///</summary>
        public static String IF = "if";

        ///<summary>
        /// conditional branching with multiply branches.
        ///</summary>
        public static String CHOOSE = "choose";

        ///<summary>
        /// used in <code>choose</code> as the default branch.
        ///</summary>
        public static String OTHERWISE = "otherwise";

        ///<summary>
        /// looping.
        ///</summary>
        public static String FOR_EACH = "for_each";

        ///<summary>
        /// get field of an object and put it in the variable map so that following actions can access it.
        ///</summary>
        public static String GET_FIELD_ACTION = "get_field";

        ///<summary>
        /// set field of an object.
        ///</summary>
        public static String SET_FIELD_ACTION = "set_field";

        ///<summary>
        /// set metadata for an object
        ///</summary>
        public static String SET_METADATA = "set_metadata";

        ///<summary>
        /// link handling action.
        ///</summary>
        public static String PARSE_DOCUMENT = "parse_document";

        ///<summary>
        /// image handling action.
        ///</summary>
        public static String CREATE_AND_VISUALIZE_IMG_SURROGATE = "create_and_visualize_img_surrogate";

        ///<summary>
        /// text handling action.
        ///</summary>
        public static String CREATE_AND_VISUALIZE_TEXT_SURROGATE = "create_and_visualize_text_surrogate";

        ///<summary>
        /// text handling action.
        ///</summary>
        public static String VISUALIZE_CLIPPINGS = "visualize_clippings";

        public static String FILTER_LOCATION = "filter_location";

        ///<summary>
        /// start a new search.
        ///</summary>
        public static String SEARCH = "search";

        ///<summary>
        /// prevent info collector from obtaining info from a site.
        ///</summary>
        public static String BACK_OFF_FROM_SITE = "back_off_from_site";

        public static String CREATE_SEMANTIC_ANCHOR = "create_semantic_anchor";

        public static String EVALUATE_RANK_WEIGHT = "eval_rank_wt";

        public static String GET_LINKED_METADATA = "get_linked_metadata";

        public static String RESELECT_METAMETADATA_AND_EXTRACT = "reselect_meta_metadata_and_extract";

        public static String ADD_MIXIN = "add_mixin";
    }
}
