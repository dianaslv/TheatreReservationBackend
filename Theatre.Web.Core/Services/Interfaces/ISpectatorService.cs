using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Theatre.Web.Core.Helpers.Commons;
using Theatre.Web.Core.Helpers.Commons.Filters.Interfaces;
using Theatre.Web.Core.Models.Entities;

namespace Theatre.Web.Core.Services.Interfaces
{
    public interface ISpectatorService
    {
        Task CreateSpectatorAsync(Spectator captain);
        Task UpdateSpectatorAsync(Spectator captain);
        Task DeleteSpectatorAsync(Guid id);
        Task<Tuple<int, List<Spectator>>> SearchSpectatorAsync(Pagination pagination, IFilter<Spectator> filter);
    }
}