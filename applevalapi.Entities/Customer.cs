using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using System.ComponentModel.DataAnnotations;
using applevalApi.Entities;

namespace applevalApi.Entities
{
    public class Customer : BaseAuditClass
    {
        public int Id { get; set; }
        [Required]
        public User user { get; set; }
        [Required]
        public Country country { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string PaymentMethod { get; set; }

        public bool Enabled { get; set; } = true;

    }
}