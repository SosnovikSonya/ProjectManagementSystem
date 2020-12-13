using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Repository;
using ProjectManagementSystem.Views.ViewModels.UserModels;
using ProjectManagementSystem.Views.ViewModels.ProjectModels;

namespace ProjectManagementSystem.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        public HomeController(IMapper mapper, IRepository repository) : base(mapper, repository) { }

        public IActionResult Index()
        {
            return View();
        }        
    }
}
