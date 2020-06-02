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
    public class SpectatorRepository : ISpectatorRepository
    {
        private readonly DataContext m_dataContext;

        public SpectatorRepository(DataContext dataContext)
        {
            m_dataContext = dataContext;
        }

        public async Task CreateAsync(Spectator entity)
        {
            await m_dataContext.Spectators.AddAsync(entity);
            await m_dataContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(List<Spectator> entities)
        {
            m_dataContext.Spectators.UpdateRange(entities);
            await m_dataContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Spectator entity)
        {
            m_dataContext.Spectators.Remove(entity);
            await m_dataContext.SaveChangesAsync();
        }
        
        public async Task<Tuple<int, List<Spectator>>> SearchAsync(Pagination pagination, IFilter<Spectator> filtering)
        {
            return await filtering
                .Filter(m_dataContext.Spectators.AsQueryable())
                .WithPaginationAsync(pagination);
        }
    }
}