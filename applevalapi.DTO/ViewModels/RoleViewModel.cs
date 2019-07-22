using System;
using applevalApi.DTO.MapperObjects;

namespace applevalApi.DTO.ViewModels
{
    public class RoleViewModel : BaseViewModel
    {
        public int Id { get; set; }

        public string RoleName { get; set; }

        public bool Status { get; set; }
    }
}