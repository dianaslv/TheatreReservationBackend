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
    public class ReservationService : IReservationService
    {
        private readonly ILogger<ReservationService> m_logger;
        private readonly IReservationRepository m_repository;

        public ReservationService(ILogger<ReservationService> logger, IReservationRepository repository)
        {
            m_logger = logger;
            m_repository = repository;
        }

        public async Task CreateReservationAsync(Reservation reservation)
        {
            try
            {
                await m_repository.CreateAsync(reservation);
            }
            catch (ValidationException e)
            {
                m_logger.LogWarning(e, "A validation failed");
                throw;
            }
            catch (Exception e) when (e.GetType() != typeof(ValidationException))
            {
                m_logger.LogCritical(e, $"Unexpected Exception while trying to create a Reservation with the properties : {JsonConvert.SerializeObject(reservation, Formatting.Indented)}");
                throw;
            }
        }

        public async Task UpdateReservationAsync(Reservation reservation)
        {
            try
            {
                await m_repository.UpdateAsync(new List<Reservation> {reservation});
            }
            catch (ValidationException e)
            {
                m_logger.LogWarning(e, "A validation failed");
                throw;
            }
            catch (Exception e) when (e.GetType() != typeof(ValidationException))
            {
                m_logger.LogCritical(e, $"Unexpected Exception while trying to update a Reservation with the properties : {JsonConvert.SerializeObject(reservation, Formatting.Indented)}");
                throw;
            }
        }

        public async Task DeleteReservationAsync(Guid id)
        {
            try
            {
                var reservation = await SearchReservationAsync(new Pagination(), new ReservationFilter
                {
                    SearchTerm = id.ToString()
                });
                await m_repository.DeleteAsync(reservation.Item2.First());
            }
            catch (ValidationException e)
            {
                m_logger.LogWarning(e, "A validation failed");
                throw;
            }
            catch (Exception e) when (e.GetType() != typeof(ValidationException))
            {
                m_logger.LogCritical(e, $"Unexpected Exception while trying to delete a Reservation for id : {id}");
                throw;
            }
        }

        public async Task<Tuple<int, List<Reservation>>> SearchReservationAsync(Pagination pagination, IFilter<Reservation> filter)
        {
            try
            {
                return await m_repository.SearchAsync(pagination, filter);
            }
            catch (Exception e)
            {
                m_logger.LogCritical(e, "Unexpected Exception while trying to search for Reservations");
                throw;
            }
        }
    }
}