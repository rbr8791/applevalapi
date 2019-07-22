using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using System.ComponentModel.DataAnnotations;

namespace applevalApi.Entities
{
    public class InvoiceDetail : BaseAuditClass
    {
        public int Id { get; set; }
        [Required]
        public Product product { get; set; }
        [Required]
        public Invoice invoice { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public float Price { get; set; }
      

    }
}