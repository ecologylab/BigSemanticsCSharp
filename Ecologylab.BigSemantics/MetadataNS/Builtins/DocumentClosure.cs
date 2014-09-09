using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Ecologylab.BigSemantics.Collecting;
//using Ecologylab.Semantics.Documentparsers;
using Ecologylab.BigSemantics.Documentparsers;
using Ecologylab.BigSemantics.MetaMetadataNS;
using Ecologylab.BigSemantics.MetadataNS.Scalar;
using Ecologylab.BigSemantics.PlatformSpecifics;
using Ecologylab.BigSemantics.Services;
using Simpl.Fundamental.Net;
using Ecologylab.BigSemantics.Actions;

namespace Ecologylab.BigSemantics.MetadataNS.Builtins
{
    using Services.Messages;

    public class DocumentClosure
    {
        public SemanticsSessionScope SemanticsSessionScope { get; private set; }

        public Document Document { get; private set; }
        private readonly object documentLock = new object();
        private ParsedUri initialUri;

        public DownloadStatus DownloadStatus { get; private set; }
        private readonly object downloadStatusLock = new object();

        public PURLConnection PURLConnection { get; private set; }

        public DocumentParser DocumentParser { get; private set; }

        public DocumentParsingDone DocumentParsingDoneHandler { get; set; }

        public TaskCompletionSource<Document> TaskCompletionSource { get; set; }

        public event EventHandler<DocumentClosureEventArgs> Continuations;

        public MetadataServicesClient MetadataServicesClient { get; set; }
        public SemanticsGlobalCollection<Document> GlobalDocumentCollection { get; set; }

        internal DocumentClosure(SemanticsSessionScope semanticsSessionScope, Document document)
        {
            SemanticsSessionScope = semanticsSessionScope;
            Document = document;
        }

        public async Task<Document> PerformDownload()
        {
            // change status
            lock (downloadStatusLock)
            {
                if (!(DownloadStatus == DownloadStatus.QUEUED || DownloadStatus == DownloadStatus.UNPROCESSED))
                    return Document; // if not queued or unprocessed, it must either hasn't enter the queue or has already been processed.
                DownloadStatus = DownloadStatus.CONNECTING;
            }

            Document.SemanticsSessionScope = SemanticsSessionScope;

            ParsedUri location = Document == null ? null : Document.Location == null ? null : Document.Location.Value;

            Debug.WriteLine("Calling connect from thread: " + Task.CurrentId);
            DateTime time = DateTime.Now;
            Connect();
            Debug.WriteLine("Completed connect: " + DateTime.Now.Subtract(time).TotalMilliseconds);

            if (PURLConnection != null && PURLConnection.Good) // && DocumentParser != null)
            {
                // parsing
                lock (downloadStatusLock)
                {
                    DownloadStatus = DownloadStatus.PARSING;
                }
                // TODO display message from DocumentParser
                MetaMetadata mmd = Document.MetaMetadata as MetaMetadata;
                // TODO before semantic actions

                //This call returns void
                //Parser is responsible for setting result in the documentClosure.TaskCompletionSource
                DocumentParser.Parse();

                await GetMetadata();

                // TODO after semantic actions

                lock (downloadStatusLock)
                {
                    DownloadStatus = DownloadStatus.DOWNLOAD_DONE;
                }
            }
            else
            {
                Debug.WriteLine("ERROR: cannot perform downloading on " + location);
            }
            // Document.DownloadDone = true;
            Document.DownloadAndParseDone();

            if (PURLConnection != null) PURLConnection.Recycle();
            PURLConnection = null;

            return Document;
        }

        public void Connect()
        {
            DocumentClosureConnectionHelper documentClosureConnectionHelper = new DocumentClosureConnectionHelper(SemanticsSessionScope, Document, this);
            Debug.WriteLine("Connect running from thread: " + Task.CurrentId);
            MetaMetadataCompositeField metaMetadata = Document.MetaMetadata;

            // then try to create a connection using the PURL
            string userAgentString = metaMetadata.UserAgentString;
            ParsedUri originalPURL = Document.Location.Value;
            PURLConnection = new PURLConnection(originalPURL);
            if (originalPURL.IsFile)
            {
                // TODO handle local files here!
                var file = PURLConnection.File;
                if (SemanticsPlatformSpecifics.Get().FileIsADictionary(file))
                {
                    // TODO FileDirectoryParser
                    // DocumentParser = DocumentParser.GetDocumentParser(FILE_DIRECTORY_PARSER);
                }
                else
                {
                    PURLConnection.FileConnect();
                    // we already have the correct meta-metadata, having used suffix to construct, or having gotten it from a restore.
                }
            }
            else
            {
                PURLConnection.NetworkConnect(documentClosureConnectionHelper, userAgentString); // HERE!
                if (PURLConnection.Good)
                {
                    Document document = this.Document; // may have changed during redirect processing
                    metaMetadata = document.MetaMetadata;

                    // check for a parser that was discovered while processing a re-direct

                    // if we made PURL connection but could not find parser using container
                    if ((PURLConnection != null) && !originalPURL.IsFile)
                    {
                        string cacheValue = PURLConnection.Response.Headers == null
                                                ? null
                                                : PURLConnection.Response.Headers["X-Cache"];
                        bool cacheHit = cacheValue != null && cacheValue.Contains("HIT");
                        if (metaMetadata.IsGenericMetadata)
                        {
                            // see if we can find more specifc meta-metadata using mimeType
                            MetaMetadataRepository repository = SemanticsSessionScope.MetaMetadataRepository;
                            string mimeType = PURLConnection.MimeType;
                            MetaMetadata mimeMmd = mimeType == null ? null : repository.GetMMByMime(mimeType);
                            if (mimeMmd != null && !mimeMmd.Equals(metaMetadata))
                            {
                                // new meta-metadata!
                                if (!mimeMmd.MetadataClass.GetTypeInfo().IsAssignableFrom(document.GetType().GetTypeInfo()))
                                //if (!mimeMmd.MetadataClass.IsAssignableFrom(document.GetType()))
                                {
                                    // more specifc so we need new metadata!
                                    document = (Document) (mimeMmd).ConstructMetadata();
                                    // set temporary on stack
                                    ChangeDocument(document);
                                }
                                metaMetadata = mimeMmd;
                            }
                        }
                    }
                }
            }

            if (DocumentParser == null)
                DocumentParser = DocumentParser.GetDocumentParser(metaMetadata.Parser);
            if (DocumentParser != null)
            {
                DocumentParser.FillValues(SemanticsSessionScope, PURLConnection, metaMetadata, this);
            }
            else
            {
                Debug.WriteLine("WARNING: no parser found: " + metaMetadata);
            }
//            else if (!DocumentParser.isRegisteredNoParser(PURLConnection.getPurl()))
//            {
//                warning("No DocumentParser found: " + metaMetadata);
//            }
        }

        public void ChangeDocument(Document newDocument)
        {
            lock (documentLock)
            {
                Document oldDocument = Document;
                Document = newDocument;
                newDocument.InheritValues(oldDocument);
                // do we need to assign semanticsInLinks here?
                // TODO recycle oldDocument
            }
        }

        public async Task<Document> GetMetadata()
        {
            if (this.DownloadStatus != DownloadStatus.RECEIVED)
            {
                this.DownloadStatus = DownloadStatus.REQUESTED;

                try
                {
                    this.Document = await this.MetadataServicesClient.RequestMetadata(Document.Location.Value);

                    this.DownloadStatus = DownloadStatus.RECEIVED;
                    if (Continuations != null)
                        Continuations(this, new DocumentClosureEventArgs(this));
                }
                catch (Exception exception)
                {
                    this.DownloadStatus = DownloadStatus.ERROR;
                    if (Continuations != null)
                        Continuations(this, new DocumentClosureEventArgs(this, exception));
                }
            }
            return this.Document;
        }
    }

    public class DocumentClosureConnectionHelper : IConnectionHelperJustRemote
    {
        private readonly SemanticsSessionScope   semanticsSessionScope;

        private readonly Document                originalDocument;

        private readonly DocumentClosure         documentClosure;

        public DocumentClosureConnectionHelper(SemanticsSessionScope semanticsSessionScope, Document originalDocument, DocumentClosure documentClosure)
        {
            this.semanticsSessionScope = semanticsSessionScope;
            this.originalDocument = originalDocument;
            this.documentClosure = documentClosure;
        }

        public void DisplayStatus(string message)
        {
            Debug.WriteLine("DocumentClosureConnectionHelper: " + message);
        }

        public bool ProcessRedirect(Uri redirectedUri)
        {
            MetadataParsedURL originalMetadataPURL = originalDocument.Location;
            ParsedUri originalPURL = originalMetadataPURL == null ? null : originalMetadataPURL.Value;
            ParsedUri redirectedPURL = new ParsedUri(redirectedUri.AbsoluteUri);
            Debug.WriteLine("try redirecting: " + originalPURL + " > " + redirectedPURL);

            Document redirectedDocument = semanticsSessionScope.GetOrConstructDocument(redirectedPURL);
            // note FIXME currently, GetOrConstructDocument does not return null!
            if (redirectedDocument != null)
            {
                // existing document: the redirected url has been visited already.

                if (originalPURL != redirectedPURL)
                    redirectedDocument.AddAdditionalLocation(originalMetadataPURL);

                // TODO -- copy metadata from originalDocument?!!

                documentClosure.ChangeDocument(redirectedDocument);

                // TODO -- reconnect

                return true;
            }
            else
            {
                // TODO -- redirect to a new location.
            }

            // TODO -- what if redirectedDocument is already in the queue or being downloaded?

            return false;
        }
    }

    public class DocumentClosureEventArgs : EventArgs
    {
        private DocumentClosure documentClosure;

        private Exception exception;

        public DocumentClosureEventArgs(DocumentClosure documentClosure)
        {
            this.documentClosure = documentClosure;
        }

        public DocumentClosureEventArgs(DocumentClosure documentClosure, Exception exception) :
            this(documentClosure)
        {
            this.exception = exception;
        }

        public DocumentClosure DocumentClosure
        {
            get { return documentClosure; }
            set { documentClosure = value; }
        }
    }
}
