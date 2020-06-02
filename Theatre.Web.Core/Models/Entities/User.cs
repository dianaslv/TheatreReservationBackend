using System;
using System.ComponentModel.DataAnnotations.Schema;
using Theatre.Web.Core.Enums;
using Theatre.Web.Core.Helpers.Interfaces.Commons;

namespace Theatre.Web.Core.Models.Entities
{
    public abstract class User : IIdentifier
    {
        public Guid Id { get; set; }
        [Column(TypeName = "varchar(256)")] public string Username { get; set; }
        [Column(TypeName = "varchar(256)")] public string Password { get; set; }
        public UserType Type { get; set; }
    }
}