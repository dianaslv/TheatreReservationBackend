using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Theatre.Web.Core.Helpers.Interfaces.Commons;

namespace Theatre.Web.Core.Models.Entities
{
    public class TheatrePlay : IIdentifier
    {
        public Guid Id { get; set; }
        [Column(TypeName = "varchar(256)")] public string Name { get; set; }

        public List<Reservation> Reservations { get; set; }
    }
}