using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;

namespace applevalApi.Entities
{
    public class AuditLogPurchase : BaseAuditClass
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string RawParams { get; set; } = null;
        public string EventType { get; set; }
        public User Customer { get; set; } 
        public DateTime? PurchaseDate { get; set; } = null;
        public int? Quantity { get; set; } = null;
        public Product product { get; set; } 
        public string UserAudited { get; set; }
    }
}