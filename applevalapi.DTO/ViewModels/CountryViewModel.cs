using System;

namespace applevalApi.DTO.ViewModels
{
    public class CountryViewModel : BaseViewModel
    {
        public int Id { get; set; }

        public string ISOCountryCode { get; set; }

        public string Name { get; set; }

        public bool Enabled { get; set; }
    }
}