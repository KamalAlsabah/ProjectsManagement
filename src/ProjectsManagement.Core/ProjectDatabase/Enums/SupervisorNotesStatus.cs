using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsManagement.ProjectDatabase.Enums
{
    public enum SupervisorNotesStatus
    {
        [Display(Name = "Under_review")]
        Under_review,

        [Display(Name = "Rejected")]
        Rejected, 
        
        [Display(Name = "Done")]
        Done, 
        
        [Display(Name = "Open_discussion")]
        Open_discussion

    }
}
