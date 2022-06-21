using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interface
{
    public interface IUnitOfWork :IDisposable
    {
      IManagerRepositoryAsync ManagerRepository { get; }
      IRegionRepositoryAsync RegionRepository { get; }
      IOrganizationRepostioryAsync OrganizationRepostiory { get; }
      Task SaveChangesAsync();
    }
}
