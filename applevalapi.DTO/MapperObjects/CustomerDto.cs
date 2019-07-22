using System;
using System.Collections.Generic;
using System.Text;
using applevalApi.DTO.Helpers.Infrastructure;
using applevalApi.Entities;
using applevalApi.DTO.MapperObjects;
using System.Linq.Expressions;

namespace applevalApi.DTO.MapperObjects
{
    public class CustomerDto
    {
        public int Id { get; set; }

        public UserDto user { get; set; }

        public CountryDto country { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string PaymentMethod { get; set; }

        public bool Enabled { get; set; }

        public static CustomerDto FromModel(Customer model)
        {
            return new CustomerDto()
            {
                Id = model.Id,
                user = UserDto.FromModel(model.user),
                country = CountryDto.FromModel(model.country),
                Address1 = model.Address1,
                Address2 = model.Address2,
                PaymentMethod = model.PaymentMethod,
                Enabled = model.Enabled,
            };
        }

        public Customer ToModel()
        {
            return new Customer()
            {
                Id = Id,
                user = user.ToModel(),
                country = country.ToModel(),
                Address1 = Address1,
                Address2 = Address2,
                PaymentMethod = PaymentMethod,
                Enabled = Enabled,
            };
        }
    }
}
