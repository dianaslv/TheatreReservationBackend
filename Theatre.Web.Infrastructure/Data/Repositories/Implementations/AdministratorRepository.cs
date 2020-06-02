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
    public class AdministratorRepository : IAdministratorRepository
    {
        private readonly DataContext m_dataContext;

        public AdministratorRepository(DataContext dataContext)
        {
            m_dataContext = dataContext;
        }

        public async Task CreateAsync(Administrator entity)
        {
            await m_dataContext.Administrators.AddAsync(entity);
            await m_dataContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(List<Administrator> entities)
        {
            m_dataContext.Administrators.UpdateRange(entities);
            await m_dataContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Administrator entity)
        {
            m_dataContext.Administrators.Remove(entity);
            await m_dataContext.SaveChangesAsync();
        }
        
        public async Task<Tuple<int, List<Administrator>>> SearchAsync(Pagination pagination, IFilter<Administrator> filtering)
        {
            return await filtering
                .Filter(m_dataContext.Administrators.AsQueryable())
                .WithPaginationAsync(pagination);
        }
    }
}