using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using applevalApi.DTO.MapperObjects;
using applevalApi.Entities;

namespace applevalApi.DTO.ViewModels
{
    public class OrderBaseViewModel : BaseViewModel
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string OrderNumber { get; set; } = null;
        public IEnumerable<OrderDetail> Details { get; set; } = new Collection<OrderDetail>();

        public DateTime CreationDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public string createdBy { get; set; }

        public bool Status { get; set; }


    }
}