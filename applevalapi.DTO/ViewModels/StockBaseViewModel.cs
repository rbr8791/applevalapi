using System;
using applevalApi.DTO.MapperObjects;

namespace applevalApi.DTO.ViewModels
{
    public class StockBaseViewModel : BaseViewModel
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public ProductDto product { get; set; }

        public int Quantity { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public bool Status { get; set; }
    }
}