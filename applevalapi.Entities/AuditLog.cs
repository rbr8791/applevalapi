using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;

namespace applevalApi.Entities
{
    public class AuditLog : BaseAuditClass
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string RawParams { get; set; } = null;
        public string EventType { get; set; }
        public DateTime CreationDate { get; set; }
        public string UserAudited { get; set; }
    }
}