using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using System.ComponentModel.DataAnnotations;

namespace applevalApi.Entities
{
    public class Stock : BaseAuditClass
    {
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public Product product { get; set; }
        [Required]
        public int Quantity { get; set; }
        public bool Status { get; set; } = true;



    }
}