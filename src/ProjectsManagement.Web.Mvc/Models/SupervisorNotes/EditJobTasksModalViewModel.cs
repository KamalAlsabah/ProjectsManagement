using Abp;
using ProjectsManagement.SupervisorNotes.Dto;
using System.Collections.Generic;

namespace ProjectsManagement.Web.Models.SupervisorNotes
{
    public class EditSupervisorNotesModalViewModel
    {
        public EditSupervisorNotesDto EditSupervisorNotesDto { get; set; }

        public List<NameValue<long>> Jobs { get; set; }

    }
}
