using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsManagement.ProjectDatabase.Enums
{
    public enum  ProjectHistoryColumns
    {
        [Display(Name = "Project")]
        Project,

        [Display(Name = "Sprint")]
        Sprint,

        [Display(Name = "Job")]
        Job ,
        [Display(Name = "JobTasks")]
        JobTasks,
        [Display(Name = "ProjectWorkers")]
        ProjectWorkers,
        [Display(Name = "ProjectSupervisors")]
        ProjectSupervisors,
        [Display(Name = "SupervisorNotes")]
        SupervisorNotes
    }
}
