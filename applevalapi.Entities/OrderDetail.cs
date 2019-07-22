using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using System.ComponentModel.DataAnnotations;
using applevalApi.Entities;

namespace applevalApi.Entities
{
    public class OrderDetail : BaseAuditClass
    {
        public int Id { get; set; }
        [Required]
        public Product product { get; set; }
        [Required]
        public int Quantity { get; set; }
        

    }
}