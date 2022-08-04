using BlobStorage.Api.Models;
using BlobStorage.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlobStorage.Api.Controllers
{
    [Route("api/blobs")]
    [ApiController]
    public class BlobExplorerController : ControllerBase
    {
        private readonly IBlobService _blobService;

        public BlobExplorerController(IBlobService blobService)
        {
            _blobService = blobService;
        }

        [HttpGet("{blobName}")]
        public async Task<IActionResult> GetBlob(string blobName)
        {
            var data = await _blobService.GetBlobAsync(blobName);
            return File(data.Content, data.ContentType);
        }

        [HttpGet("list")]
        public async Task<IActionResult> ListBlobs()
        {
            return Ok(await _blobService.ListBlobsAsync());
        }

        /// <summary>
        /// Esta metodo faz upload a partir de um caminho completo. 
        /// Não é utilizado para upload de arquivos feito no front
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("uploadFile")]
        public async Task<IActionResult> UploadFile([FromBody] UploadFileRequest request)
        {
            await _blobService.UploadFileBlobAsync(request.FilePath, request.FileName);
            return Ok();
        }

        [HttpPost("uploadcontent")]
        public async Task<IActionResult> UploadContent([FromBody] UploadContentRequest request)
        {
            await _blobService.UploadContentBlobAsync(request.Content, request.FileName);
            return Ok();
        }

        [HttpDelete("{blobName}")]
        public async Task<IActionResult> DeleteFile(string blobName)
        {
            await _blobService.DeleteBlobAsync(blobName);
            return Ok();
        }

        [HttpGet("size")]
        public async Task<IActionResult> GeContainerSize()
        {
            return Ok(await _blobService.GetContainerSize(""));
        }
    }
}
