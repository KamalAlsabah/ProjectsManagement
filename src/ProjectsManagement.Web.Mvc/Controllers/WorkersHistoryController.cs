﻿using Microsoft.AspNetCore.Mvc;
using ProjectsManagement.Controllers;
using ProjectsManagement.Web.Models.WorkersHistory;
using ProjectsManagement.WorkersHistory;
using System.Threading.Tasks;

namespace ProjectsManagement.Web.Controllers
{
    public class WorkersHistoryController : ProjectsManagementControllerBase
    {
        private readonly IWorkersHistoryAppService _service;
        public WorkersHistoryController(IWorkersHistoryAppService service)
        {
                _service= service;
        }
        public async Task<IActionResult> Index(long WorkerId)
        {
            IndexWorkersHistoryModalViewModel model = new IndexWorkersHistoryModalViewModel() {WorkerId=WorkerId };
            model.TodayTotalHours =await _service.GetTodayTotalHours(WorkerId);
            return View(model);
        }
    }
}
