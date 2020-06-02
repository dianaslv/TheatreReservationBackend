using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Theatre.Web.Core.Helpers.Commons;
using Theatre.Web.Core.Helpers.Commons.Filters.Interfaces;
using Theatre.Web.Core.Helpers.Interfaces.Commons;
using Theatre.Web.Core.Models.Entities;

namespace Theatre.Web.Core.Services.Interfaces
{
    public interface IUserService
    {
        Task CreateUserAsync(User captain);
        Task UpdateUserAsync(User captain);
        Task DeleteUserAsync(Guid id);
        Task<Tuple<int, List<User>>> SearchUserAsync(Pagination pagination, IFilter<User> filter);
    }
}