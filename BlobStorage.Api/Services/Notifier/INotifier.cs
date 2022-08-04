namespace BlobStorage.Api.Services.Notifier
{
    public interface INotifier
    {
        bool hasNotices();
        List<Notice> GetNotices();
        void Handle(string message);
        void ClearNotices();
    }
}
