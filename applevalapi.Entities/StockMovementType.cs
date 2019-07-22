using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using System.ComponentModel.DataAnnotations;

namespace applevalApi.Entities
{
    public class StockMovementType : BaseAuditClass
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }




    }
}