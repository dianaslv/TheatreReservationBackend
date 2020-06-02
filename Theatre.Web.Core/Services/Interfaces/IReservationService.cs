using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Theatre.Web.Core.Helpers.Commons;
using Theatre.Web.Core.Helpers.Commons.Filters.Interfaces;
using Theatre.Web.Core.Models.Entities;

namespace Theatre.Web.Core.Services.Interfaces
{
    public interface IReservationService
    {
        Task CreateReservationAsync(Reservation captain);
        Task UpdateReservationAsync(Reservation captain);
        Task DeleteReservationAsync(Guid id);
        Task<Tuple<int, List<Reservation>>> SearchReservationAsync(Pagination pagination, IFilter<Reservation> filter);
    }
}