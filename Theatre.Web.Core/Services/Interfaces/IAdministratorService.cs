using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Theatre.Web.Core.Helpers.Commons;
using Theatre.Web.Core.Helpers.Commons.Filters.Interfaces;
using Theatre.Web.Core.Models.Entities;

namespace Theatre.Web.Core.Services.Interfaces
{
    public interface IAdministratorService
    {
        Task CreateAdministratorAsync(Administrator captain);
        Task UpdateAdministratorAsync(Administrator captain);
        Task DeleteAdministratorAsync(Guid id);
        Task<Tuple<int, List<Administrator>>> SearchAdministratorAsync(Pagination pagination, IFilter<Administrator> filter);
    }
}