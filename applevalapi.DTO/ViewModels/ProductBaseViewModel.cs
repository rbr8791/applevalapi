using System;
using applevalApi.DTO.MapperObjects;

namespace applevalApi.DTO.ViewModels
{
    public class ProductBaseViewModel : BaseViewModel
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public float? Price { get; set; }
        public int? Likes { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public bool? Status { get; set; }
    }
}