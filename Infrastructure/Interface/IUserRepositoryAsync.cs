using Domains;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interface
{
    public interface IUserRepositoryAsync : IBaseRepositoryAsync<User>
    {
        Task<IReadOnlyList<User>> GetListById(Guid RegionId, Language language, int page, int pageSize);
        int GetCount(Guid RegionId);
    }
}
