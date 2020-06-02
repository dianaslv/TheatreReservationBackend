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
    public class TheatrePlayService : ITheatrePlayService
    {
        private readonly ILogger<TheatrePlayService> m_logger;
        private readonly ITheatrePlayRepository m_repository;

        public TheatrePlayService(ILogger<TheatrePlayService> logger, ITheatrePlayRepository repository)
        {
            m_logger = logger;
            m_repository = repository;
        }

        public async Task CreateTheatrePlayAsync(TheatrePlay theatrePlay)
        {
            try
            {
                await m_repository.CreateAsync(theatrePlay);
            }
            catch (ValidationException e)
            {
                m_logger.LogWarning(e, "A validation failed");
                throw;
            }
            catch (Exception e) when (e.GetType() != typeof(ValidationException))
            {
                m_logger.LogCritical(e, $"Unexpected Exception while trying to create a TheatrePlay with the properties : {JsonConvert.SerializeObject(theatrePlay, Formatting.Indented)}");
                throw;
            }
        }

        public async Task UpdateTheatrePlayAsync(TheatrePlay theatrePlay)
        {
            try
            {
                await m_repository.UpdateAsync(new List<TheatrePlay> {theatrePlay});
            }
            catch (ValidationException e)
            {
                m_logger.LogWarning(e, "A validation failed");
                throw;
            }
            catch (Exception e) when (e.GetType() != typeof(ValidationException))
            {
                m_logger.LogCritical(e, $"Unexpected Exception while trying to update a TheatrePlay with the properties : {JsonConvert.SerializeObject(theatrePlay, Formatting.Indented)}");
                throw;
            }
        }

        public async Task DeleteTheatrePlayAsync(Guid id)
        {
            try
            {
                var theatrePlay = await SearchTheatrePlayAsync(new Pagination(), new TheatrePlayFilter
                {
                    SearchTerm = id.ToString()
                });
                await m_repository.DeleteAsync(theatrePlay.Item2.First());
            }
            catch (ValidationException e)
            {
                m_logger.LogWarning(e, "A validation failed");
                throw;
            }
            catch (Exception e) when (e.GetType() != typeof(ValidationException))
            {
                m_logger.LogCritical(e, $"Unexpected Exception while trying to delete a TheatrePlay for id : {id}");
                throw;
            }
        }

        public async Task<Tuple<int, List<TheatrePlay>>> SearchTheatrePlayAsync(Pagination pagination, IFilter<TheatrePlay> filter)
        {
            try
            {
                return await m_repository.SearchAsync(pagination, filter);
            }
            catch (Exception e)
            {
                m_logger.LogCritical(e, "Unexpected Exception while trying to search for TheatrePlays");
                throw;
            }
        }
    }
}