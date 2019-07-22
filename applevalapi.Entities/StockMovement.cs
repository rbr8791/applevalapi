using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using System.ComponentModel.DataAnnotations;

namespace applevalApi.Entities
{
    public class StockMovement : BaseAuditClass
    {
        public int Id { get; set; }
        [Required]
        public Product product { get; set; }
        [Required]
        public string DocumentID { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public StockMovementType StockMovementType { get; set; }
       



    }
}