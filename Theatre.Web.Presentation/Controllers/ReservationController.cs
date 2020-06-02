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
    public class ReservationController
    {
        private readonly IReservationService m_reservationService;
        private readonly ILogger<ReservationController> m_logger;

        public ReservationController(IReservationService reservationService, ILogger<ReservationController> logger)
        {
            m_reservationService = reservationService;
            m_logger = logger;
        }

        [HttpPost("get")]
        public async Task<Response> GetReservationsAsync([FromBody] SearchRequest<Reservation, ReservationFilter> request)
        {
            return new Response
            {
                IsSuccesfull = true,
                Message = JsonConvert.SerializeObject(await m_reservationService.SearchReservationAsync(request.Pagination, request.Filtering))
            };
        }

        [HttpPost]
        public async Task<Response> AddAsync([FromBody] Reservation reservation)
        {
            reservation.Id = new Guid();
            await m_reservationService.CreateReservationAsync(reservation);
            return new Response
            {
                IsSuccesfull = true,
                Message = reservation.Id.ToString()
            };
        }

        [HttpPut]
        public async Task<Response> UpdateAsync([FromBody] IEnumerable<Reservation> reservations)
        {
            foreach (var reservation in reservations)
            {
                await m_reservationService.UpdateReservationAsync(reservation);
            }

            return new Response
            {
                IsSuccesfull = true
            };
        }

        [HttpDelete]
        public async Task<Response> DeleteAsync([FromQuery(Name = "id")] string id)
        {
            await m_reservationService.DeleteReservationAsync(Guid.Parse(id));
            return new Response
            {
                IsSuccesfull = true
            };
        }
    }
}