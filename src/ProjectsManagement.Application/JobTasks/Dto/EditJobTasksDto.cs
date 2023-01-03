using Abp.Application.Services.Dto;

namespace ProjectsManagement.JobTasks.Dto
{
    public class EditJobTasksDto: IEntityDto<long>
    {
        public long Id { get; set; }

        public string NameF { get; set; }
        public string NameL { get; set; }

        public string Description { get; set; }
        public long? JobId { get; set; }

    }
}
