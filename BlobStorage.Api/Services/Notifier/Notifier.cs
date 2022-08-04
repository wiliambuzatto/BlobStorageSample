namespace BlobStorage.Api.Services.Notifier
{
    public class Notifier : INotifier
    {
        private List<Notice> _notices;

        public Notifier()
        {
            _notices = new List<Notice>();
        }

        public void Handle(string message)
        {
            _notices.Add(new Notice(message));
        }

        public List<Notice> GetNotices()
        {
            return _notices;
        }

        public bool hasNotices()
        {
            return _notices.Any();
        }

        public void ClearNotices()
        {
            _notices.Clear();
        }
    }
}
