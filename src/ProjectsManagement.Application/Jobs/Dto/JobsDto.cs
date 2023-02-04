﻿using Abp.Application.Services.Dto;
using ProjectsManagement.ProjectDatabase.Enums;
using ProjectsManagement.ProjectDatabase.Sprint;
using System;

namespace ProjectsManagement.Jobs.Dto
{
    public class JobsDto : EntityDto<long>
    {
        public string Name { get; set; }
        public string WorkerFullName { get; set; }
        public string Description { get; set; }
        public string SprintName { get; set; }
        public int ExpectedNoOfHours { get; set; }
        public int ActualNumberOfHours { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public long? ProjectId { get; set; }


    }
}
