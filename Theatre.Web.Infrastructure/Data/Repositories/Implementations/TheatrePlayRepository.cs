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
    public class TheatrePlayRepository : ITheatrePlayRepository
    {
        private readonly DataContext m_dataContext;

        public TheatrePlayRepository(DataContext dataContext)
        {
            m_dataContext = dataContext;
        }

        public async Task CreateAsync(TheatrePlay entity)
        {
            await m_dataContext.TheatrePlays.AddAsync(entity);
            await m_dataContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(List<TheatrePlay> entities)
        {
            m_dataContext.TheatrePlays.UpdateRange(entities);
            await m_dataContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(TheatrePlay entity)
        {
            m_dataContext.TheatrePlays.Remove(entity);
            await m_dataContext.SaveChangesAsync();
        }
        
        public async Task<Tuple<int, List<TheatrePlay>>> SearchAsync(Pagination pagination, IFilter<TheatrePlay> filtering)
        {
            return await filtering
                .Filter(m_dataContext.TheatrePlays.AsQueryable())
                .WithPaginationAsync(pagination);
        }
    }
}