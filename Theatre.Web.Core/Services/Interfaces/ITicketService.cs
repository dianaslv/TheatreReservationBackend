using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Theatre.Web.Core.Helpers.Commons;
using Theatre.Web.Core.Helpers.Commons.Filters.Interfaces;
using Theatre.Web.Core.Models.Entities;

namespace Theatre.Web.Core.Services.Interfaces
{
    public interface ITicketService
    {
        Task CreateTicketAsync(Ticket captain);
        Task UpdateTicketAsync(Ticket captain);
        Task DeleteTicketAsync(Guid id);
        Task<Tuple<int, List<Ticket>>> SearchTicketAsync(Pagination pagination, IFilter<Ticket> filter);
    }
}