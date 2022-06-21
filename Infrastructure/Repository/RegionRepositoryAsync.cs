using Domains;
using Infrastructure.Data;
using Infrastructure.Interface;

namespace Infrastructure.Repository
{
    public class RegionRepositoryAsync : BaseRepositoryAsync<Region>, IRegionRepositoryAsync
    {
        public RegionRepositoryAsync(ApplicationDbContext context) : base(context)
        {
        }
    }
}
