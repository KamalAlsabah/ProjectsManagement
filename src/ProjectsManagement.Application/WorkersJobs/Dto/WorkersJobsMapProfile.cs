using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsManagement.WorkersJobs.Dto
{
    public class WorkersJobsMapProfile : Profile
    {
        public WorkersJobsMapProfile()
        {
            CreateMap<CreateWorkersJobsDto, ProjectDatabase.WorkersJobs.WorkersJobs>().ReverseMap();
            CreateMap<WorkersJobsDto, ProjectDatabase.WorkersJobs.WorkersJobs>().ReverseMap();
            CreateMap<EditWorkersJobsDto, ProjectDatabase.WorkersJobs.WorkersJobs>().ReverseMap();
            CreateMap<ListWorkersJobsDto, ProjectDatabase.WorkersJobs.WorkersJobs>().ReverseMap();
            CreateMap<UpdateInputDto, ProjectDatabase.WorkersDashboard.WorkersDashboard>().ReverseMap();
        }
    }
}
