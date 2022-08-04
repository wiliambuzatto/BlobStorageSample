namespace BlobStorage.Api.Services.Notifier
{
    public class Notice
    {
        public Notice(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }
}
