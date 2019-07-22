using System;
using applevalApi.DTO.MapperObjects;

namespace applevalApi.DTO.ViewModels
{
    public class InvoiceDetailViewModel : BaseViewModel
    {
        public int Id { get; set; }

        public ProductDto product { get; set; }

        public InvoiceDto invoice { get; set; }

        public int Quantity { get; set; }

        public float Price { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public string createdBy { get; set; }

    }
}