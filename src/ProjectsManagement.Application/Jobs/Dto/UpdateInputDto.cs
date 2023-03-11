﻿using Abp.Application.Services.Dto;
using ProjectsManagement.ProjectDatabase.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsManagement.Jobs.Dto
{
    public class UpdateInputDto:EntityDto<long>
    {
        public string NameL { get; set; }
        public string NameF { get; set; }
        public string Description { get; set; }
        public int WieghtOfHours { get; set; }

        public long? SprintId { get; set; }
        [Range(0, 500)]
        public int ExpectedNoOfHours { get; set; }
        public int ActualNumberOfHours { get; set; }

        public JobStatus Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
