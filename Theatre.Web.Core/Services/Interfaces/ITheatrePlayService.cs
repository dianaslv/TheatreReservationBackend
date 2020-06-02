using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Theatre.Web.Core.Helpers.Commons;
using Theatre.Web.Core.Helpers.Commons.Filters.Interfaces;
using Theatre.Web.Core.Models.Entities;

namespace Theatre.Web.Core.Services.Interfaces
{
    public interface ITheatrePlayService
    {
        Task CreateTheatrePlayAsync(TheatrePlay captain);
        Task UpdateTheatrePlayAsync(TheatrePlay captain);
        Task DeleteTheatrePlayAsync(Guid id);
        Task<Tuple<int, List<TheatrePlay>>> SearchTheatrePlayAsync(Pagination pagination, IFilter<TheatrePlay> filter);
    }
}