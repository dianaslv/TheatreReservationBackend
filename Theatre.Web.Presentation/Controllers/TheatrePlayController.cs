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
    public class TheatrePlayController
    {
        private readonly ITheatrePlayService m_theatrePlayService;
        private readonly ILogger<TheatrePlayController> m_logger;

        public TheatrePlayController(ITheatrePlayService theatrePlayService, ILogger<TheatrePlayController> logger)
        {
            m_theatrePlayService = theatrePlayService;
            m_logger = logger;
        }

        [HttpPost("get")]
        public async Task<Response> GetTheatrePlaysAsync([FromBody] SearchRequest<TheatrePlay, TheatrePlayFilter> request)
        {
            return new Response
            {
                IsSuccesfull = true,
                Message = JsonConvert.SerializeObject(await m_theatrePlayService.SearchTheatrePlayAsync(request.Pagination, request.Filtering))
            };
        }

        [HttpPost]
        public async Task<Response> AddAsync([FromBody] TheatrePlay theatrePlay)
        {
            theatrePlay.Id = Guid.NewGuid();
            await m_theatrePlayService.CreateTheatrePlayAsync(theatrePlay);
            return new Response
            {
                IsSuccesfull = true,
                Message = theatrePlay.Id.ToString()
            };
        }

        [HttpPut]
        public async Task<Response> UpdateAsync([FromBody] IEnumerable<TheatrePlay> theatrePlays)
        {
            foreach (var theatrePlay in theatrePlays)
            {
                await m_theatrePlayService.UpdateTheatrePlayAsync(theatrePlay);
            }

            return new Response
            {
                IsSuccesfull = true
            };
        }

        [HttpDelete]
        public async Task<Response> DeleteAsync([FromBody] Guid id)
        {
            await m_theatrePlayService.DeleteTheatrePlayAsync(id);
            return new Response
            {
                IsSuccesfull = true
            };
        }
    }
}