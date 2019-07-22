using System;
using applevalApi.Entities;

namespace applevalApi.DTO.ViewModels
{
    public class AuditLogPurchaseViewModel : BaseViewModel
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string RawParams { get; set; }

        public string EventType { get; set; }

        public User Customer { get; set; }
        public DateTime? PurchaseDate { get; set; } = null;

        public int? Quantity { get; set; } = null;

        public Product product { get; set; }

        public string UserAudited { get; set; }
    }
}