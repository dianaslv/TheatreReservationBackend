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
    public class AdministratorService : IAdministratorService
    {
        private readonly ILogger<AdministratorService> m_logger;
        private readonly IAdministratorRepository m_repository;

        public AdministratorService(ILogger<AdministratorService> logger, IAdministratorRepository repository)
        {
            m_logger = logger;
            m_repository = repository;
        }

        public async Task CreateAdministratorAsync(Administrator administrator)
        {
            try
            {
                await m_repository.CreateAsync(administrator);
            }
            catch (ValidationException e)
            {
                m_logger.LogWarning(e, "A validation failed");
                throw;
            }
            catch (Exception e) when (e.GetType() != typeof(ValidationException))
            {
                m_logger.LogCritical(e, $"Unexpected Exception while trying to create a Administrator with the properties : {JsonConvert.SerializeObject(administrator, Formatting.Indented)}");
                throw;
            }
        }

        public async Task UpdateAdministratorAsync(Administrator administrator)
        {
            try
            {
                await m_repository.UpdateAsync(new List<Administrator> {administrator});
            }
            catch (ValidationException e)
            {
                m_logger.LogWarning(e, "A validation failed");
                throw;
            }
            catch (Exception e) when (e.GetType() != typeof(ValidationException))
            {
                m_logger.LogCritical(e, $"Unexpected Exception while trying to update a Administrator with the properties : {JsonConvert.SerializeObject(administrator, Formatting.Indented)}");
                throw;
            }
        }

        public async Task DeleteAdministratorAsync(Guid id)
        {
            try
            {
                var administrator = await SearchAdministratorAsync(new Pagination(), new AdministratorFilter
                {
                    SearchTerm = id.ToString()
                });
                await m_repository.DeleteAsync(administrator.Item2.First());
            }
            catch (ValidationException e)
            {
                m_logger.LogWarning(e, "A validation failed");
                throw;
            }
            catch (Exception e) when (e.GetType() != typeof(ValidationException))
            {
                m_logger.LogCritical(e, $"Unexpected Exception while trying to delete a Administrator for id : {id}");
                throw;
            }
        }

        public async Task<Tuple<int, List<Administrator>>> SearchAdministratorAsync(Pagination pagination, IFilter<Administrator> filter)
        {
            try
            {
                return await m_repository.SearchAsync(pagination, filter);
            }
            catch (Exception e)
            {
                m_logger.LogCritical(e, "Unexpected Exception while trying to search for Administrators");
                throw;
            }
        }
    }
}