namespace BlobStorage.Api.Entities
{
    public class Company
    {
        public int CompanyId { get; set; }
        public string ContainerName { get; set; }
        public long MaximumStorageSize { get; set; }
        public long ActualStorageSize { get; set; }
        public bool MaximumStorageSizeReached { get; set; }
    }
}
