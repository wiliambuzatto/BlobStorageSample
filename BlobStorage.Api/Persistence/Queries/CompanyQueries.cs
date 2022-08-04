using BlobStorage.Api.Entities;

namespace BlobStorage.Api.Persistence.Queries
{
    public class CompanyQueries
    {
        public List<Company> Companies { get; set; }

        public CompanyQueries()
        {
            // size = 1GB
            Companies = new List<Company> { 
                                new Company { CompanyId = 5, ContainerName = "container-5", MaximumStorageSize = 1_073_741_824, MaximumStorageSizeReached = false },
                                new Company { CompanyId = 7, ContainerName = "container-7", MaximumStorageSize = 1_073_741_824, MaximumStorageSizeReached = false },
                                new Company { CompanyId = 9, ContainerName = "container-9", MaximumStorageSize = 1_073_741_824, MaximumStorageSizeReached = true },
            };
        }

        public async Task<Company> GetCompany(int id)
        {
            return await Task.FromResult(Companies.First(c => c.CompanyId == id));
        }

        public async Task UpdateCompany(Company company)
        {
            Companies.Remove(company);
            await Task.FromResult(() => { Companies.Add(company); });

            //Companies[company.CompanyId] = company; 
        }
    }
}
