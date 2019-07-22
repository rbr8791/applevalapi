﻿using System;
using applevalApi.DTO.MapperObjects;

namespace applevalApi.DTO.ViewModels
{
    public class OrderDetailViewModel : BaseViewModel
    {
        public int Id { get; set; }

        public ProductDto product { get; set; }

        public int Quantity { get; set; }


    }
}