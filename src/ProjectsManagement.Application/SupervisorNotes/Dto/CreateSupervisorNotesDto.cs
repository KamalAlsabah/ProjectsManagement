
namespace ProjectsManagement.SupervisorNotes.Dto
{
    public class CreateSupervisorNotesDto
    {
        public string Note { get; set; }
        public long JobId { get; set; }
        public long SupervisorId { get; set; } = 0;
    }
}
