﻿using System;
using applevalApi.DTO.MapperObjects;

namespace applevalApi.DTO.ViewModels
{
    public class StockMovementTypeBaseViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}