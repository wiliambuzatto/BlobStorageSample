using BlobStorage.Api.Services.Notifier;
using BlobStorage.Api.UseCases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlobStorage.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly UploadXmlUseCase _uploadXmlUseCase;
        private readonly INotifier _notifier;
        public CompaniesController(UploadXmlUseCase uploadXmlUseCase,
                                   INotifier notifier)
        {
            _uploadXmlUseCase = uploadXmlUseCase;
            _notifier = notifier;
        }

        [HttpPost("{companyId}/uploadxml")]
        public async Task<IActionResult> UploadXml([FromForm] IFormFile file, [FromRoute] int companyId)
        {
            try
            {
                await _uploadXmlUseCase.Execute(file, companyId);

                if (_notifier.hasNotices())
                    return BadRequest(new { Errors = _notifier.GetNotices().Select(n => n.Message) });

                return Ok();
            }
            catch (Exception ex)
            {
                // registrar no log: ex
                return StatusCode(500, "Oops, uma falha ocorreu, tente novamente");
            }
        }
    }
}
