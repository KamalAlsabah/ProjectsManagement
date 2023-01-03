using Abp.Application.Services.Dto;

namespace ProjectsManagement.SupervisorNotes.Dto
{
    public class UpDateInputSupervisorNotesDto: IEntityDto<long>
    {
        public string Note { get; set; }
        public long Id { get; set; }
    }
}
