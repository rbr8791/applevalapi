using System;
using System.Collections.Generic;
using System.Text;
using applevalApi.DTO.Helpers.Infrastructure;
using applevalApi.Entities;
using applevalApi.DTO.MapperObjects;
using System.Linq.Expressions;

namespace applevalApi.DTO.MapperObjects
{
    public class CountryDto
    {
        public int Id { get; set; }

        public string ISOCountryCode { get; set; }

        public string Name { get; set; }

        public bool Enabled { get; set; }

        public static CountryDto FromModel(Country model)
        {
            return new CountryDto()
            {
                Id = model.Id,
                ISOCountryCode = model.ISOCountryCode,
                Name = model.Name,
                Enabled = model.Enabled,
            };
        }

        public Country ToModel()
        {
            return new Country()
            {
                Id = Id,
                ISOCountryCode = ISOCountryCode,
                Name = Name,
                Enabled = Enabled,
            };
        }
    }
}
