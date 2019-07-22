using System;

namespace applevalApi.Entities
{
    public class BaseAuditClass 
    {
        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }

        public string createdBy { get; set; } = "";
    }
}