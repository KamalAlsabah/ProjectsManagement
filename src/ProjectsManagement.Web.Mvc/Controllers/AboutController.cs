﻿using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using ProjectsManagement.Controllers;

namespace ProjectsManagement.Web.Controllers
{
    [AbpMvcAuthorize]
    public class AboutController : ProjectsManagementControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}
