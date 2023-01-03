using AutoMapper;

namespace ProjectsManagement.SupervisorNotes.Dto
{
    public class SupervisorNotesMapProfile : Profile
    {
        public SupervisorNotesMapProfile()
        {


            CreateMap<ProjectsManagement.SupervisorNotes.Dto.CreateSupervisorNotesDto, ProjectsManagement.ProjectDatabase.SupervisorNotes.SupervisorNotes>().ReverseMap();

            CreateMap<ProjectsManagement.SupervisorNotes.Dto.SupervisorNotesDto, ProjectsManagement.ProjectDatabase.SupervisorNotes.SupervisorNotes>().ReverseMap();


            CreateMap<ProjectsManagement.SupervisorNotes.Dto.EditSupervisorNotesDto, ProjectsManagement.ProjectDatabase.SupervisorNotes.SupervisorNotes>().ReverseMap();

            CreateMap<ProjectsManagement.SupervisorNotes.Dto.ListSupervisorNotesDto, ProjectsManagement.ProjectDatabase.SupervisorNotes.SupervisorNotes>().ReverseMap();



            CreateMap<ProjectsManagement.SupervisorNotes.Dto.UpDateInputSupervisorNotesDto, ProjectsManagement.ProjectDatabase.SupervisorNotes.SupervisorNotes>().ReverseMap();

        }
    }
}
