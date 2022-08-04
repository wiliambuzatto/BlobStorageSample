using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using BlobStorage.Api.Extensions;
using System.Text;

namespace BlobStorage.Api.Services
{
    public class BlobService : IBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _containerName = "container-5";

        public BlobService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task<BlobDownloadInfo> GetBlobAsync(string blobName)
        {
            var path = "nfe";
            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blobClient = containerClient.GetBlobClient($"{path}/{blobName}");
            return await blobClient.DownloadAsync();
        }

        public async Task<IEnumerable<string>> ListBlobsAsync()
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            var items = new List<string>();

            await foreach (var blobIterm in containerClient.GetBlobsAsync())
            {
                items.Add(blobIterm.Name);
            }

            return items;
        }

        public async Task UploadFileBlobAsync(string filePath, string fileName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blobClient = containerClient.GetBlobClient(fileName);
            await blobClient.UploadAsync(filePath, new BlobHttpHeaders { ContentType = filePath.GetContentType() });
        }

        public async Task UploadContentBlobAsync(string content, string fileName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blobClient = containerClient.GetBlobClient(fileName);
            var bytes = Encoding.UTF8.GetBytes(content);
            await using var momoryStream = new MemoryStream(bytes);

            await blobClient.UploadAsync(momoryStream, new BlobHttpHeaders { ContentType = fileName.GetContentType() });
        }

        public async Task DeleteBlobAsync(string blobName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blobClient = containerClient.GetBlobClient(blobName);
            await blobClient.DeleteIfExistsAsync();
        }

        public async Task<long> GetContainerSize(string containerName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);

            var sizes = new Dictionary<AccessTier, long>
            {
                { AccessTier.Archive, 0 },
                { AccessTier.Hot, 0 },
                { AccessTier.Cool, 0 }
            };

            await foreach (var blob in containerClient.GetBlobsAsync())
                sizes[blob.Properties.AccessTier.Value] += blob.Properties.ContentLength.Value;


            return sizes[AccessTier.Hot];
        }

        public async Task UploadFile(IFormFile formFile, string newName, string containerName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(newName);

            using var stream = formFile.OpenReadStream();
            await blobClient.UploadAsync(stream, true);
        }

        public async Task CreateContainerIfNotExist(string containerName)
        {
            var container = _blobServiceClient.GetBlobContainerClient(containerName);
            await container.CreateIfNotExistsAsync();
        }
    }
}
