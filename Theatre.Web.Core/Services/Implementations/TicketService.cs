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
    public class TicketService : ITicketService
    {
        private readonly ILogger<TicketService> m_logger;
        private readonly ITicketRepository m_repository;

        public TicketService(ILogger<TicketService> logger, ITicketRepository repository)
        {
            m_logger = logger;
            m_repository = repository;
        }

        public async Task CreateTicketAsync(Ticket ticket)
        {
            try
            {
                await m_repository.CreateAsync(ticket);
            }
            catch (ValidationException e)
            {
                m_logger.LogWarning(e, "A validation failed");
                throw;
            }
            catch (Exception e) when (e.GetType() != typeof(ValidationException))
            {
                m_logger.LogCritical(e, $"Unexpected Exception while trying to create a Ticket with the properties : {JsonConvert.SerializeObject(ticket, Formatting.Indented)}");
                throw;
            }
        }

        public async Task UpdateTicketAsync(Ticket ticket)
        {
            try
            {
                await m_repository.UpdateAsync(new List<Ticket> {ticket});
            }
            catch (ValidationException e)
            {
                m_logger.LogWarning(e, "A validation failed");
                throw;
            }
            catch (Exception e) when (e.GetType() != typeof(ValidationException))
            {
                m_logger.LogCritical(e, $"Unexpected Exception while trying to update a Ticket with the properties : {JsonConvert.SerializeObject(ticket, Formatting.Indented)}");
                throw;
            }
        }

        public async Task DeleteTicketAsync(Guid id)
        {
            try
            {
                var ticket = await SearchTicketAsync(new Pagination(), new TicketFilter
                {
                    SearchTerm = id.ToString()
                });
                await m_repository.DeleteAsync(ticket.Item2.First());
            }
            catch (ValidationException e)
            {
                m_logger.LogWarning(e, "A validation failed");
                throw;
            }
            catch (Exception e) when (e.GetType() != typeof(ValidationException))
            {
                m_logger.LogCritical(e, $"Unexpected Exception while trying to delete a Ticket for id : {id}");
                throw;
            }
        }

        public async Task<Tuple<int, List<Ticket>>> SearchTicketAsync(Pagination pagination, IFilter<Ticket> filter)
        {
            try
            {
                return await m_repository.SearchAsync(pagination, filter);
            }
            catch (Exception e)
            {
                m_logger.LogCritical(e, "Unexpected Exception while trying to search for Tickets");
                throw;
            }
        }
    }
}