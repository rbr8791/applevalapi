using System;
using applevalApi.DTO.MapperObjects;

namespace applevalApi.DTO.ViewModels
{
    public class InvoiceViewModel : BaseViewModel
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public string createdBy { get; set; }

        public bool Status { get; set; }

    }
}