using AutoMapper;
using ProjectsManagement.ProjectDatabase.Project;

namespace ProjectsManagement.Project.Dto
{
    public class ProjectsMapProfile : Profile
    {
        public ProjectsMapProfile()
        {


            CreateMap<ProjectsManagement.Project.Dto.CreateProjectsDto, Projects>().ReverseMap();

            CreateMap<ProjectsManagement.Project.Dto.ProjectsDto, Projects>().ReverseMap();


            CreateMap<ProjectsManagement.Project.Dto.EditProjectsDto, Projects>().ReverseMap();

            CreateMap<ProjectsManagement.Project.Dto.ListProjectsDto, Projects>().ReverseMap();




        }
    }
}
