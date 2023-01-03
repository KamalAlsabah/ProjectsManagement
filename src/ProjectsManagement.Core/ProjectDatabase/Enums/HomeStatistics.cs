using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsManagement.ProjectDatabase.Enums
{
    public enum EHomeStatistics
    {
        [Display(Name ="ActiveProject")]
        ActiveProject,
        [Display(Name = "FinishedProject")]
        FinishedProject,
        [Display(Name = "SupervisorCount")]
        SupervisorCount,
        [Display(Name = "WorkerCount")]
        WorkerCount,
        [Display(Name = "HouresCount")]
        HouresCount,
        [Display(Name = "TasksCount")]
        TasksCount,
        [Display(Name = "JobsCount")]
        JobsCount,
        [Display(Name = "ProjectCompletionRateOnTime")]
        ProjectCompletionRateOnTime,
        [Display(Name = "SuggestionsCount")]
        SuggestionsCount,
        [Display(Name = "TeamRatings")]
        TeamRatings
    }
}
