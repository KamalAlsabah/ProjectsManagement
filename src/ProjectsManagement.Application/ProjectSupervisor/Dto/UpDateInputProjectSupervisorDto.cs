using Abp.Application.Services.Dto;

namespace ProjectsManagement.ProjectSupervisor.Dto
{
    public class UpDateInputProjectSupervisorDto: IEntityDto<long>
    {
        public long Id { get; set; }

        public long SupervisorId { get; set; }


    }
}
