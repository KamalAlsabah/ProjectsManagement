using AutoMapper;

namespace ProjectsManagement.Sprints.Dto
{
    public class SprintMapProfile : Profile
    {
        public SprintMapProfile()
        {
            CreateMap<CreateSprintDto, ProjectDatabase.Sprint.Sprints>().ReverseMap();
            CreateMap<SprintsDto, ProjectDatabase.Sprint.Sprints>().ReverseMap();
            CreateMap<EditSprintDto, ProjectDatabase.Sprint.Sprints>().ReverseMap();
            CreateMap<ListSprintDto, ProjectDatabase.Sprint.Sprints>().ReverseMap();
            CreateMap<UpdateInputDto, ProjectDatabase.Sprint.Sprints>().ReverseMap();
        }
    }
}
