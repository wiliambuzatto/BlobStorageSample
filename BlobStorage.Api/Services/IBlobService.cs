using Azure.Storage.Blobs.Models;

namespace BlobStorage.Api.Services
{
    public interface IBlobService
    {
        Task<BlobDownloadInfo> GetBlobAsync(string blobName);
        Task<IEnumerable<string>> ListBlobsAsync();
        Task UploadFileBlobAsync(string filePath, string fileName);
        Task UploadContentBlobAsync(string content, string fileName);
        Task DeleteBlobAsync(string blobName);
        Task<long> GetContainerSize(string containerName);
        Task UploadFile(IFormFile formFile, string newName, string containerName);
        Task CreateContainerIfNotExist(string containerName);
    }
}
