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
    public class AdministratorController
    {
        private readonly IAdministratorService m_administratorService;
        private readonly ILogger<AdministratorController> m_logger;

        public AdministratorController(IAdministratorService administratorService, ILogger<AdministratorController> logger)
        {
            m_administratorService = administratorService;
            m_logger = logger;
        }

        [HttpPost("get")]
        public async Task<Response> GetAdministratorsAsync([FromBody] SearchRequest<Administrator, AdministratorFilter> request)
        {
            return new Response
            {
                IsSuccesfull = true,
                Message = JsonConvert.SerializeObject(await m_administratorService.SearchAdministratorAsync(request.Pagination, request.Filtering))
            };
        }

        [HttpPost]
        public async Task<Response> AddAsync([FromBody] Administrator administrator)
        {
            administrator.Id = Guid.NewGuid();
            await m_administratorService.CreateAdministratorAsync(administrator);
            return new Response
            {
                IsSuccesfull = true,
                Message = administrator.Id.ToString()
            };
        }

        [HttpPut]
        public async Task<Response> UpdateAsync([FromBody] IEnumerable<Administrator> administrators)
        {
            foreach (var administrator in administrators)
            {
                await m_administratorService.UpdateAdministratorAsync(administrator);
            }

            return new Response
            {
                IsSuccesfull = true
            };
        }

        [HttpDelete]
        public async Task<Response> DeleteAsync([FromBody] Guid id)
        {
            await m_administratorService.DeleteAdministratorAsync(id);
            return new Response
            {
                IsSuccesfull = true
            };
        }
    }
}