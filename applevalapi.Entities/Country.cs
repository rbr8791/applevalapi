using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using System.ComponentModel.DataAnnotations;

namespace applevalApi.Entities
{
    public class Country : BaseAuditClass
    {
        public int Id { get; set; }
        public string ISOCountryCode { get; set; }
        [Required]
        public string Name { get; set; }
        public bool Enabled { get; set; } = true;

    }
}