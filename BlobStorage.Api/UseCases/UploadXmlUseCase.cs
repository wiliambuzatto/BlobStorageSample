using BlobStorage.Api.Persistence.Queries;
using BlobStorage.Api.Services;
using BlobStorage.Api.Services.Notifier;

namespace BlobStorage.Api.UseCases
{
    public class UploadXmlUseCase
    {
        private readonly IBlobService _blobService;
        private readonly INotifier _notifier;
        private readonly CompanyQueries _companyQueries;

        public UploadXmlUseCase(IBlobService blobService,
                                INotifier notifier,
                                CompanyQueries companyQueries)
        {
            _blobService = blobService;
            _notifier = notifier;
            _companyQueries = companyQueries;
        }

        public async Task Execute(IFormFile formFile, int companyId)
        {
            var company = await _companyQueries.GetCompany(companyId);

            if (company.MaximumStorageSizeReached)
            {
                _notifier.Handle("Tamanho máximo do storage atingido");
                return;
            }

            await _blobService.CreateContainerIfNotExist(company.ContainerName);

            var newName = $"xml/{formFile.FileName}";
            await _blobService.UploadFile(formFile, newName, company.ContainerName);
        }
    }
}
