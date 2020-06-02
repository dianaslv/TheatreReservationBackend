using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Theatre.Web.Core.Extensions;
using Theatre.Web.Core.Helpers.Commons;
using Theatre.Web.Core.Helpers.Commons.Filters.Interfaces;
using Theatre.Web.Core.Models.Entities;
using Theatre.Web.Core.Repository.Interfaces;
using Theatre.Web.Infrastructure.Data.Context;
using Theatre.Web.Infrastructure.IOC;

namespace Theatre.Web.Infrastructure.Data.Repositories.Implementations
{
    [RegistrationKind(Type = RegistrationType.Scoped)]
    public class TicketRepository : ITicketRepository
    {
        private readonly DataContext m_dataContext;

        public TicketRepository(DataContext dataContext)
        {
            m_dataContext = dataContext;
        }

        public async Task CreateAsync(Ticket entity)
        {
            await m_dataContext.Tickets.AddAsync(entity);
            await m_dataContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(List<Ticket> entities)
        {
            m_dataContext.Tickets.UpdateRange(entities);
            await m_dataContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Ticket entity)
        {
            m_dataContext.Tickets.Remove(entity);
            await m_dataContext.SaveChangesAsync();
        }
        
        public async Task<Tuple<int, List<Ticket>>> SearchAsync(Pagination pagination, IFilter<Ticket> filtering)
        {
            return await filtering
                .Filter(m_dataContext.Tickets.AsQueryable())
                .WithPaginationAsync(pagination);
        }
    }
}