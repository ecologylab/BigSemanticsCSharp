namespace ecologylab.semantics.collecting
{
    public enum DownloadStatus
    {
        UNPROCESSED,
        QUEUED,
        CONNECTING,
        PARSING,
        DOWNLOAD_DONE,
        IOERROR,
        RECYCLED,

        //for semantic service
        REQUESTED,
        RECEIVED,
        ERROR,
        DONE
    }
}