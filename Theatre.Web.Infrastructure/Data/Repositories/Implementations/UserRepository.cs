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
    public class UserRepository : IUserRepository
    {
        private readonly DataContext m_dataContext;

        public UserRepository(DataContext dataContext)
        {
            m_dataContext = dataContext;
        }

        public async Task CreateAsync(User entity)
        {
            await m_dataContext.Users.AddAsync(entity);
            await m_dataContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(List<User> entities)
        {
            m_dataContext.Users.UpdateRange(entities);
            await m_dataContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(User entity)
        {
            m_dataContext.Users.Remove(entity);
            await m_dataContext.SaveChangesAsync();
        }
        
        public async Task<Tuple<int, List<User>>> SearchAsync(Pagination pagination, IFilter<User> filtering)
        {
            return await filtering
                .Filter(m_dataContext.Users.AsQueryable())
                .WithPaginationAsync(pagination);
        }
    }
}