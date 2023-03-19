using AutoMapper;
using ProjectsManagement.ProjectDatabase.Project;
using ProjectsManagement.Projects.Dto;

namespace ProjectsManagement.Project.Dto
{
    public class ProjectsMapProfile : Profile
    {
        public ProjectsMapProfile()
        {


            CreateMap<ProjectsManagement.Project.Dto.CreateProjectsDto,ProjectsManagement.ProjectDatabase.Project.Projects>().ReverseMap();

            CreateMap<ProjectsManagement.Project.Dto.ProjectsDto, ProjectsManagement.ProjectDatabase.Project.Projects>().ReverseMap();


            CreateMap<ProjectsManagement.Project.Dto.EditProjectsDto, ProjectsManagement.ProjectDatabase.Project.Projects>().ReverseMap();

            CreateMap<ProjectsManagement.Project.Dto.ListProjectsDto, ProjectsManagement.ProjectDatabase.Project.Projects>().ReverseMap();
            CreateMap<UpdateInputDto, ProjectsManagement.ProjectDatabase.Project.Projects>().ReverseMap();




        }
    }
}
