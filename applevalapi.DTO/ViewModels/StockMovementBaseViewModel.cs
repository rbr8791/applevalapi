using System;
using applevalApi.DTO.MapperObjects;

namespace applevalApi.DTO.ViewModels
{
    public class StockMovementViewModel : BaseViewModel
    {
        public int Id { get; set; }

        public ProductDto product { get; set; }

        public string DocumentID { get; set; }

        public int Quantity { get; set; }

        public StockMovementTypeDto StockMovementType { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public string createdBy { get; set; }
    }
}