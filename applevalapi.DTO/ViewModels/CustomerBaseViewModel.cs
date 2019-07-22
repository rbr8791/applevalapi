using System;
using applevalApi.DTO.MapperObjects;

namespace applevalApi.DTO.ViewModels
{
    public class CustomerBaseViewModel : BaseViewModel
    {
        public int Id { get; set; }

        public UserDto user { get; set; }

        public CountryDto country { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string PaymentMethod { get; set; }

        public bool Enabled { get; set; }
    }
}