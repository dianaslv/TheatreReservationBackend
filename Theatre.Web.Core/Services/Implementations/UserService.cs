using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Theatre.Web.Core.Helpers.Commons;
using Theatre.Web.Core.Helpers.Commons.Filters.Implementations;
using Theatre.Web.Core.Helpers.Commons.Filters.Interfaces;
using Theatre.Web.Core.Models.Entities;
using Theatre.Web.Core.Repository.Interfaces;
using Theatre.Web.Core.Services.Interfaces;

namespace Theatre.Web.Core.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> m_logger;
        private readonly IUserRepository m_repository;

        public UserService(ILogger<UserService> logger, IUserRepository repository)
        {
            m_logger = logger;
            m_repository = repository;
        }

        public async Task CreateUserAsync(User user)
        {
            try
            {
                await m_repository.CreateAsync(user);
            }
            catch (ValidationException e)
            {
                m_logger.LogWarning(e, "A validation failed");
                throw;
            }
            catch (Exception e) when (e.GetType() != typeof(ValidationException))
            {
                m_logger.LogCritical(e, $"Unexpected Exception while trying to create a User with the properties : {JsonConvert.SerializeObject(user, Formatting.Indented)}");
                throw;
            }
        }

        public async Task UpdateUserAsync(User user)
        {
            try
            {
                await m_repository.UpdateAsync(new List<User> {user});
            }
            catch (ValidationException e)
            {
                m_logger.LogWarning(e, "A validation failed");
                throw;
            }
            catch (Exception e) when (e.GetType() != typeof(ValidationException))
            {
                m_logger.LogCritical(e, $"Unexpected Exception while trying to update a User with the properties : {JsonConvert.SerializeObject(user, Formatting.Indented)}");
                throw;
            }
        }

        public async Task DeleteUserAsync(Guid id)
        {
            try
            {
                var user = await SearchUserAsync(new Pagination(), new UserFilter
                {
                    SearchTerm = id.ToString()
                });
                await m_repository.DeleteAsync(user.Item2.First());
            }
            catch (ValidationException e)
            {
                m_logger.LogWarning(e, "A validation failed");
                throw;
            }
            catch (Exception e) when (e.GetType() != typeof(ValidationException))
            {
                m_logger.LogCritical(e, $"Unexpected Exception while trying to delete a User for id : {id}");
                throw;
            }
        }

        public async Task<Tuple<int, List<User>>> SearchUserAsync(Pagination pagination, IFilter<User> filter)
        {
            try
            {
                return await m_repository.SearchAsync(pagination, filter);
            }
            catch (Exception e)
            {
                m_logger.LogCritical(e, "Unexpected Exception while trying to search for Users");
                throw;
            }
        }
    }
}