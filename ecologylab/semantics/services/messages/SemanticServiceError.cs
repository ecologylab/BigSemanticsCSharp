using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simpl.OODSS.Messages;


namespace ecologylab.semantics.services.messages
{
    using Simpl.Serialization.Attributes;

    using ecologylab.collections;
    using ecologylab.semantics.metadata.scalar;

    /// <summary>
    /// The SemanticServiceError message is received from the semantic service when  an error/exception appeared on the server when performing the request_message sent by the client
    /// </summary>
    public class SemanticServiceError : ResponseMessage
    {
        /// <summary>
        /// The code for the error that produced this message
        /// </summary>
        [SimplScalar]
        private MetadataInteger code;

        /// <summary>
        /// A message regarding the error that appeared on the server
        /// </summary>
        [SimplScalar]
        private MetadataString errorMessage;

        public MetadataInteger Code
        {
            get
            {
                return code;
            }
            set
            {
                code = value;
            }
        }

        public MetadataString ErrorMessage
        {
            get
            {
                return errorMessage;
            }
            set
            {
                errorMessage = value;
            }
        }

        public override bool IsOK()
        {
            return true;
        }

        public void Perform()
        {
            throw new SemanticServiceErrorException((SemanticServiceErrorCodes)code.Value, errorMessage.Value);
        }
    }

    /// <summary>
    /// The error that is thrown when an SemanticServiceError message is received from the semantic service
    /// </summary>
    public class SemanticServiceErrorException : Exception
    {
        private SemanticServiceErrorCodes errorCode;

        public SemanticServiceErrorException(SemanticServiceErrorCodes code, String message) 
            : base("Error code: " + code + " Message: " + message)
        {
            this.errorCode = code;
        }
    }

    /// <summary>
    /// Meta/Metadata Error Codes
    /// </summary>
    public enum SemanticServiceErrorCodes
    {
        /// <summary>
        /// MetaMetadata could not be found for specified URL.
        /// </summary>
        MmdNotFoundUrl = 2001,

        /// <summary>
        /// MetaMetadata could not be found for specified URL / Suffix.
        /// </summary>
        MmdNotFoundSuffix,

        /// <summary>
        /// There is an internal error as document representing the location got recycled.
        /// </summary>
        DocumentRecycled,

        /// <summary>
        /// Location is found to be NULL for Document.
        /// Field X in Y Metadata. There may be an issue in MMD Wrapper.
        /// </summary>
        LocationIsNull,

        /// <summary>
        /// There is an internal error as the Site for the URL has been recycled.
        /// </summary>
        SiteRecycled,

        /// <summary>
        /// The site for requested URL is down. Please try later.
        /// If site is accessible through browser,  Contact: metametadata@googlegroups.com
        /// </summary>
        SiteIsDown,

        /// <summary>
        /// There is an internal error as Closure representing URL is recycled.
        /// </summary>
        ClosureIsRecycled,

        /// <summary>
        /// A SocketTimeoutException has occurred. Details are as follows.
        /// IF Applicable: 
        /// The site for the requested URL puts a limit on the download request and has currently blocked requests. Please try again later in X time.
        /// </summary>
        SocketTimeoutException,

        /// <summary>
        /// An IOException occurred during download. Further details are as follows.
        /// </summary>
        IoException,

        /// <summary>
        /// A FileNotFoundException occurred during download. Further details are as follows.
        /// </summary>
        FileNotFoundException,

        /// <summary>
        /// ThreadDeath in Download. Further details are as follows.
        /// </summary>
        ThreadDeath,

        /// <summary>
        /// Downloading could not be performed as System is out of memory.
        /// </summary>
        OutOfMemory,

        /// <summary>
        /// Downloading failed due to following exception.
        /// </summary>
        OtherException,

        /// <summary>
        /// No Parser found for the content of requested URL.
        /// </summary>
        ParserisNull,

        /// <summary>
        /// Downloaded document could not be parsed due to an IOException. Details are as follows.
        /// </summary>
        ParserIoException,

        /// <summary>
        /// There were exceptions in parsing. Details are as follows.
        /// </summary>
        ParserExceptions,
    }
}
