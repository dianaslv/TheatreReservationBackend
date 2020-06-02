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
    public class SpectatorService : ISpectatorService
    {
        private readonly ILogger<SpectatorService> m_logger;
        private readonly ISpectatorRepository m_repository;

        public SpectatorService(ILogger<SpectatorService> logger, ISpectatorRepository repository)
        {
            m_logger = logger;
            m_repository = repository;
        }

        public async Task CreateSpectatorAsync(Spectator spectator)
        {
            try
            {
                await m_repository.CreateAsync(spectator);
            }
            catch (ValidationException e)
            {
                m_logger.LogWarning(e, "A validation failed");
                throw;
            }
            catch (Exception e) when (e.GetType() != typeof(ValidationException))
            {
                m_logger.LogCritical(e, $"Unexpected Exception while trying to create a Spectator with the properties : {JsonConvert.SerializeObject(spectator, Formatting.Indented)}");
                throw;
            }
        }

        public async Task UpdateSpectatorAsync(Spectator spectator)
        {
            try
            {
                await m_repository.UpdateAsync(new List<Spectator> {spectator});
            }
            catch (ValidationException e)
            {
                m_logger.LogWarning(e, "A validation failed");
                throw;
            }
            catch (Exception e) when (e.GetType() != typeof(ValidationException))
            {
                m_logger.LogCritical(e, $"Unexpected Exception while trying to update a Spectator with the properties : {JsonConvert.SerializeObject(spectator, Formatting.Indented)}");
                throw;
            }
        }

        public async Task DeleteSpectatorAsync(Guid id)
        {
            try
            {
                var spectator = await SearchSpectatorAsync(new Pagination(), new SpectatorFilter
                {
                    SearchTerm = id.ToString()
                });
                await m_repository.DeleteAsync(spectator.Item2.First());
            }
            catch (ValidationException e)
            {
                m_logger.LogWarning(e, "A validation failed");
                throw;
            }
            catch (Exception e) when (e.GetType() != typeof(ValidationException))
            {
                m_logger.LogCritical(e, $"Unexpected Exception while trying to delete a Spectator for id : {id}");
                throw;
            }
        }

        public async Task<Tuple<int, List<Spectator>>> SearchSpectatorAsync(Pagination pagination, IFilter<Spectator> filter)
        {
            try
            {
                return await m_repository.SearchAsync(pagination, filter);
            }
            catch (Exception e)
            {
                m_logger.LogCritical(e, "Unexpected Exception while trying to search for Spectators");
                throw;
            }
        }
    }
}