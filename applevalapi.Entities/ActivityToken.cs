using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;

namespace applevalApi.Entities
{
    public class ActiveToken : BaseAuditClass
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string CurrentToken { get; set; }
        public DateTime TokenDate { get; set; }
        public DateTime TokenExpireDate { get; set; }
        public string SourceIdentifier { get; set; }
        public bool Status { get; set; }
    }
}