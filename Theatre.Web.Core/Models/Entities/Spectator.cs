using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Theatre.Web.Core.Models.Entities
{
    public class Spectator : User
    {
        [Column(TypeName = "varchar(256)")] public string Name { get; set; }
        [Column(TypeName = "varchar(256)")] public string Email { get; set; }
        [Column(TypeName = "varchar(256)")] public string Address { get; set; }
        public List<Reservation> Reservations { get; set; }
    }
}