﻿using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsManagement.WorkersDashboards.Dto
{
    public class WorkersDashboardDto : EntityDto<long>
    {
        public string WorkerName { get; set; }
        public double WorkerJobsCount { get; set; }
    }
}
