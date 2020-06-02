using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Theatre.Web.Core.Helpers.Interfaces.Commons;

namespace Theatre.Web.Core.Models.Entities
{
    public class Reservation : IIdentifier
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid SpectatorId { get; set; }
        public Guid TheatrePlayId { get; set; }
        [Column(TypeName = "varchar(256)")] public string Name { get; set; }
        [Column(TypeName = "varchar(256)")] public string Email { get; set; }
        [Column(TypeName = "varchar(256)")] public string Address { get; set; }
        public Spectator Spectator { get; set; }
        public TheatrePlay TheatrePlay { get; set; }
        public List<Ticket> Tickets { get; set; }
    }
}