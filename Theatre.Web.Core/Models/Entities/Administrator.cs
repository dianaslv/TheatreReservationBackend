using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Theatre.Web.Core.Models.Entities
{
    public class Administrator : User
    {
        [Column(TypeName = "varchar(256)")]public string PhoneNumber { get; set; }
        public DateTime LastLogged { get; set; } 
    }
}