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
    public class TicketController
    {
        private readonly ITicketService m_ticketService;
        private readonly ILogger<TicketController> m_logger;

        public TicketController(ITicketService ticketService, ILogger<TicketController> logger)
        {
            m_ticketService = ticketService;
            m_logger = logger;
        }

        [HttpPost("get")]
        public async Task<Response> GetTicketsAsync([FromBody] SearchRequest<Ticket, TicketFilter> request)
        {
            return new Response
            {
                IsSuccesfull = true,
                Message = JsonConvert.SerializeObject(await m_ticketService.SearchTicketAsync(request.Pagination, request.Filtering))
            };
        }

        [HttpPost]
        public async Task<Response> AddAsync([FromBody] Ticket ticket)
        {
            ticket.Id = Guid.NewGuid();
            await m_ticketService.CreateTicketAsync(ticket);
            return new Response
            {
                IsSuccesfull = true,
                Message = ticket.Id.ToString()
            };
        }

        [HttpPut]
        public async Task<Response> UpdateAsync([FromBody] IEnumerable<Ticket> tickets)
        {
            foreach (var ticket in tickets)
            {
                await m_ticketService.UpdateTicketAsync(ticket);
            }

            return new Response
            {
                IsSuccesfull = true
            };
        }

        [HttpDelete]
        public async Task<Response> DeleteAsync([FromQuery(Name = "id")] string id)
        {
            await m_ticketService.DeleteTicketAsync(Guid.Parse(id));
            return new Response
            {
                IsSuccesfull = true
            };
        }
    }
}