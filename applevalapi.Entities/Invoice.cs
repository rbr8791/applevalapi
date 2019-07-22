using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using System.ComponentModel.DataAnnotations;

namespace applevalApi.Entities
{
    public class Invoice : BaseAuditClass
    {
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        public string createdBy { get; set; }
        public bool Status { get; set; } = true;
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new Collection<InvoiceDetail>();

    }
}