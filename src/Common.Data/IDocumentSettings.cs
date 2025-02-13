namespace StatementIQ.Common.Data
{
    public interface IDocumentSettings
    {
        long AllowedFileSize { get; }
        string UploadDocumentLocation { get; }
    }
}