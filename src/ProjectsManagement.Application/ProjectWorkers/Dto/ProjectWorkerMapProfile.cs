using AutoMapper;
using ProjectsManagement.Project.Dto;

namespace ProjectsManagement.ProjectWorkers.Dto
{
    public class ProjectWorkerMapProfile : Profile
    {
        public ProjectWorkerMapProfile()
        {


            CreateMap<CreateProjectWorkerDto, ProjectDatabase.ProjectWorker.ProjectWorkers>().ReverseMap();

            CreateMap<ProjectWorkersDto, ProjectDatabase.ProjectWorker.ProjectWorkers>().ReverseMap();


            CreateMap<EditProjectWorkerDto, ProjectDatabase.ProjectWorker.ProjectWorkers>().ReverseMap();

            CreateMap<ListProjectWorkerDto, ProjectDatabase.ProjectWorker.ProjectWorkers>().ReverseMap();
            CreateMap<UpdateInputDto, ProjectDatabase.ProjectWorker.ProjectWorkers>().ReverseMap();
            



        }
    }
}
