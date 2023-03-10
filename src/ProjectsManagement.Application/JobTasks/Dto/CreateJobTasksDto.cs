using ProjectsManagement.ProjectDatabase.Enums;

namespace ProjectsManagement.JobTasks.Dto
{
    public class CreateJobTasksDto
    {
        public string NameF { get; set; }
        public string NameL { get; set; }
        public JobTaskStatus JobTaskStatus { get; set; }
        public string Description { get; set; }
        public long? JobId { get; set; }

    }
}
