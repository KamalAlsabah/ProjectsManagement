using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsManagement.WorkersHistory.Dto
{
    public class WorkersHistoryMapProfileDto :Profile
    {
        public WorkersHistoryMapProfileDto()
        {
            CreateMap<WorkersHistoryCreateDto, ProjectDatabase.WorkersHistory.WorkersHistory>().ReverseMap();
            CreateMap<WorkersHistoryCreateDto, UpdateInputDto>().ReverseMap();
            CreateMap<WorkersHistoryDto, UpdateInputDto>().ReverseMap();
            CreateMap<WorkersHistoryDto, ProjectDatabase.WorkersHistory.WorkersHistory>().ReverseMap();
            CreateMap<WorkersHistoryEditDto, ProjectDatabase.WorkersHistory.WorkersHistory>().ReverseMap();
            CreateMap<WorkersHistoryListDto, ProjectDatabase.WorkersHistory.WorkersHistory>().ReverseMap();
            CreateMap<UpdateInputDto, ProjectDatabase.WorkersHistory.WorkersHistory>().ReverseMap();

        }
    }
}
