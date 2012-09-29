using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecologylab.Semantics.Actions
{
    /// <summary>
    ///TODO Probably remane this class.
    ///@author amathur
    /// </summary>
    public abstract class SemanticOperationKeyWords : SemanticOperationNamedArguments
    {
        public static string DocumentType = "documentType";

        public static string Container = "container";

        public static string InfoCollector = "infoCollector";

        public static string InfoCollectorDataType = "InfoCollector";

        public static string Metadata = "metadata";

        public static string NotNullCheck = "NOT_NULL";

        /// <summary>
        /// Used for methods with boolean value
        /// </summary>
        public static string MethodCheck = "METHOD_CHECK";

        /// <summary>
        /// To specify that we have to take action on a collection
        /// </summary>
        public static string Collection = "collection";

        /// <summary>
        /// Key word for the current collection object of the loop. ie the kth object. This is not
        /// avaiableto user.
        /// </summary>
        public static string CurrentCollectionIndex = "current-collection-index";

        public static string NodeSet = "node_list";

        /// <summary>
        /// Root Node of the document
        /// </summary>
        public static string DocumentRootNode = "__DocumentRootNode__";

        public static string Node = "node";

        public static string True = "true";

        public static string False = "false";

        public static string Null = "null";

        public static string TruePurl = "TRUE_PURL";

        public static string DirectBindingParser = "direct";

        public static string XpathParser = "xpath";

        public static string HtmlImageDomTextParser = "html_dom_image_text";

        public static string FileDirectoryParser = "file_directory";

        public static string FeedParser = "feed";

        public static string PdfParser = "pdf";

        public static string ImageParser = "image";

        public static string PurlconnectionMime = "purl_connect_mime";

        public static string DocumentCaller = "document_caller";

        public static string SurroundingMetaMetadataStack = "__SurroundingMetaMetadataStack__";

    }
}
