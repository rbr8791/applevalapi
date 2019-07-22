using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using System.ComponentModel.DataAnnotations;
using applevalApi.Entities;

namespace applevalApi.Entities
{
    public class User : BaseAuditClass
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Username { get; set; }
        public string Password { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }
        [Required]
        public Role Role { get; set; }

        public bool Enabled { get; set; } = true;

    }
}