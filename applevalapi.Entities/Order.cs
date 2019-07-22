using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using System.ComponentModel.DataAnnotations;
using applevalApi.Entities;

namespace applevalApi.Entities
{
    public class Order : BaseAuditClass
    {
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        public string OrderNumber { get; set; } = null;
        public bool Status { get; set; } = true;

        public IEnumerable<OrderDetail> Details { get; set; } = new Collection<OrderDetail>();
        //public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new Collection<OrderDetail>();

    }
}