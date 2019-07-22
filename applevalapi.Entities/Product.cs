using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using System.ComponentModel.DataAnnotations;

namespace applevalApi.Entities
{
    public class Product : BaseAuditClass
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public float? Price { get; set; } = null;
        public int? Likes { get; set; } = null;
        public bool? Status { get; set; } = null;



    }
}