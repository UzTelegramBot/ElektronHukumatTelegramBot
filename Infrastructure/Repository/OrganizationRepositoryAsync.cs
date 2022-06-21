using Domains;
using Infrastructure.Data;
using Infrastructure.Interface;

namespace Infrastructure.Repository
{
    public class OrganizationRepositoryAsync : BaseRepositoryAsync<Organization>, IOrganizationRepostioryAsync
    {
        public OrganizationRepositoryAsync(ApplicationDbContext context) : base(context)
        {
        }
    }
}
