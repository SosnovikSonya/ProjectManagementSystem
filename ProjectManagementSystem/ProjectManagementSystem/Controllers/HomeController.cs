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
    public class HomeController : Controller
    {
        IRepository Repository { get; set; }

        private IMapper Mapper { get; set; }

        public HomeController(IMapper mapper, IRepository repository)
        {
            Mapper = mapper;
            Repository = repository;
        }

        public IActionResult Index()
        {
            var currentUser = UserManagement.GetCurrentUser(Repository, Mapper, User.Identity.Name);

            ViewBag.CurrentUser = currentUser;

            var projects = Repository.Projects
                .Search(project => true)
                .Select(project => Mapper.Map<Project>(project))
                .ToList();

            ViewBag.Projects = projects;

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
        
    }
}
