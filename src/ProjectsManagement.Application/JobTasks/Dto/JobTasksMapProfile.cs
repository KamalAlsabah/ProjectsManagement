using AutoMapper;

namespace ProjectsManagement.JobTasks.Dto
{
    public class JobTasksMapProfile : Profile
    {
        public JobTasksMapProfile()
        {


            CreateMap<ProjectsManagement.JobTasks.Dto.CreateJobTasksDto, ProjectsManagement.ProjectDatabase.JobTask.JobTasks>().ReverseMap();

            CreateMap<ProjectsManagement.JobTasks.Dto.JobTasksDto, ProjectsManagement.ProjectDatabase.JobTask.JobTasks>().ReverseMap();


            CreateMap<ProjectsManagement.JobTasks.Dto.EditJobTasksDto, ProjectsManagement.ProjectDatabase.JobTask.JobTasks>().ReverseMap();

            CreateMap<ProjectsManagement.JobTasks.Dto.ListJobTasksDto, ProjectsManagement.ProjectDatabase.JobTask.JobTasks>().ReverseMap();



            CreateMap<ProjectsManagement.JobTasks.Dto.UpDateInputJobTasksDto, ProjectsManagement.ProjectDatabase.JobTask.JobTasks>().ReverseMap();

        }
    }
}
