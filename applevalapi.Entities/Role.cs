using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using System.ComponentModel.DataAnnotations;

namespace applevalApi.Entities
{
    public class Role : BaseAuditClass
    {
        public int Id { get; set; }
        [Required]
        public string RoleName { get; set; }
        [Required]
        public bool Status { get; set; } = true;

    }
}