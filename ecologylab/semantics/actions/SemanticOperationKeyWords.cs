﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ecologylabSemantics.ecologylab.semantics.actions
{
    public abstract class SemanticOperationKeyWords : SemanticOperationNamedArguments
    {
        public static String DOCUMENT_TYPE = "documentType";

        public static String CONTAINER = "container";

        public static String INFO_COLLECTOR = "infoCollector";

        public static String INFO_COLLECTOR_DATA_TYPE = "InfoCollector";

        public static String METADATA = "metadata";

        public static String NOT_NULL_CHECK = "NOT_NULL";

        ///<summary>
        /// Used for methods with boolean value
        ///</summary>
        public static String METHOD_CHECK = "METHOD_CHECK";

        ///<summary>
        /// To specify that we have to take action on a collection
        ///</summary>
        public static String COLLECTION = "collection";

        ///<summary>
        /// Key word for the current collection object of the loop. ie the kth object. This is not
        /// avaiableto user.
        ///</summary>
        public static String CURRENT_COLLECTION_INDEX = "current-collection-index";

        public static String NODE_SET = "node_list";

        ///<summary>
        /// Root Node of the document
        ///</summary>
        public static String DOCUMENT_ROOT_NODE = "__DocumentRootNode__";

        public static String NODE = "node";

        public static String TRUE = "true";

        public static String FALSE = "false";

        public static String NULL = "null";

        public static String TRUE_PURL = "TRUE_PURL";

        public static String DIRECT_BINDING_PARSER = "direct";

        public static String XPATH_PARSER = "xpath";

        public static String HTML_IMAGE_DOM_TEXT_PARSER = "html_dom_image_text";

        public static String FILE_DIRECTORY_PARSER = "file_directory";

        public static String FEED_PARSER = "feed";

        public static String PDF_PARSER = "pdf";

        public static String IMAGE_PARSER = "image";

        public static String PURLCONNECTION_MIME = "purl_connect_mime";

        public static String DOCUMENT_CALLER = "document_caller";

        public static String SURROUNDING_META_METADATA_STACK = "__SurroundingMetaMetadataStack__";

    }
}
