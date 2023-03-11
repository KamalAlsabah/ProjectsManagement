using Abp;
using ProjectsManagement.SupervisorNotes.Dto;
using System.Collections.Generic;

namespace ProjectsManagement.Web.Models.SupervisorNotes
{
    public class CreateSupervisorNotesModalViewModel
    {
        public CreateSupervisorNotesDto CreateSupervisorNotesDto { get; set; }
        public List<NameValue<long>> Jobs { get; set; }
    }
}
