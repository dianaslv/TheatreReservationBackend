using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Theatre.Web.Core.Helpers.Commons.Filters.Implementations;
using Theatre.Web.Core.Helpers.Network.Models;
using Theatre.Web.Core.Models.Entities;
using Theatre.Web.Core.Services.Interfaces;

namespace Theatre.Web.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpectatorController
    {
        private readonly ISpectatorService m_spectatorService;
        private readonly ILogger<SpectatorController> m_logger;

        public SpectatorController(ISpectatorService spectatorService, ILogger<SpectatorController> logger)
        {
            m_spectatorService = spectatorService;
            m_logger = logger;
        }

        [HttpPost("get")]
        public async Task<Response> GetSpectatorsAsync([FromBody] SearchRequest<Spectator, SpectatorFilter> request)
        {
            return new Response
            {
                IsSuccesfull = true,
                Message = JsonConvert.SerializeObject(await m_spectatorService.SearchSpectatorAsync(request.Pagination, request.Filtering))
            };
        }

        [HttpPost]
        public async Task<Response> AddAsync([FromBody] Spectator spectator)
        {
            spectator.Id = Guid.NewGuid();
            await m_spectatorService.CreateSpectatorAsync(spectator);
            return new Response
            {
                IsSuccesfull = true,
                Message = spectator.Id.ToString()
            };
        }

        [HttpPut]
        public async Task<Response> UpdateAsync([FromBody] IEnumerable<Spectator> spectators)
        {
            foreach (var spectator in spectators)
            {
                await m_spectatorService.UpdateSpectatorAsync(spectator);
            }

            return new Response
            {
                IsSuccesfull = true
            };
        }

        [HttpDelete]
        public async Task<Response> DeleteAsync([FromBody] string id)
        {
            await m_spectatorService.DeleteSpectatorAsync(Guid.Parse((id)));
            return new Response
            {
                IsSuccesfull = true
            };
        }
    }
}