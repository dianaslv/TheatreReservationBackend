using System;
using Theatre.Web.Core.Helpers.Interfaces.Commons;

namespace Theatre.Web.Core.Models.Entities
{
    public class Ticket : IIdentifier
    {
        public Guid Id { get; set; }
        public int Row { get; set; }
        public int Section { get; set; }
        public int SeatNumber { get; set; }
        public Guid ReservationId { get; set; }
        public float Price { get; set; }
        public Reservation Reservation { get; set; }
    }
}