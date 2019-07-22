using System;

namespace applevalApi.DTO.ViewModels
{
    public class AuditLogBaseViewModel : BaseViewModel
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string RawParams { get; set; }

        public string EventType { get; set; }

        public DateTime CreationDate { get; set; }

        public string UserAudited { get; set; }
    }
}