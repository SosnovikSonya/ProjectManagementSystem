using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectManagementSystem.Models.Interfaces;
using ProjectManagementSystem.Repository;
using ProjectManagementSystem.Views.ViewModels.UserModels;
using ProjectManagementSystem.Views.ViewModels.WorkItemModels;
using ProjectManagementSystem.Views.ViewModels.ProjectModels;

namespace ProjectManagementSystem.Controllers
{
    [Route("workitem")]
    public class WorkItemController : Controller
    {
        IRepository Repository { get; set; }

        private IMapper Mapper { get; set; }

        public WorkItemController(IMapper mapper, IRepository repository)
        {
            Mapper = mapper;
            Repository = repository;
        }

        [HttpGet("create")]
        public IActionResult GetWorkItemCreationView()
        {
            var projects = Repository.Projects
                .Search(project => true)
                .Select(project => Mapper.Map<Project>(project))
                .ToList();
            var projTitles = new List<string>();
            foreach (var item in projects)
            {
                projTitles.Add(item.Title);
            }
            ViewBag.Projects = new SelectList(projTitles);

            var types = Repository.WorkItemTypes
              .Search((type) => true)
              .Select(type => Mapper.Map<WorkItemType>(type))
              .ToList();
            var typeNames = new List<string>();
            foreach (var item in types)
            {
                typeNames.Add(item.TypeName);
            }
            ViewBag.Types = new SelectList(typeNames);

            var users = Repository.Users
                .Search((user) => true)
                .Where(user => user.Email != User.Identity.Name && user.UserRole.RoleName != "Administrator")
                .Select(user => Mapper.Map<User>(user))
                .ToList();
            var userNames = new List<string>();
            foreach (var item in users)
            {
                userNames.Add(item.FirstName + " " +  item.LastName);
            }
            ViewBag.Users = new SelectList(userNames);


            RedirectToAction("CreateWorkItem", "WorkItem");
            return View("CreateWorkItem");
        }

        [HttpPost("create")]
        public IActionResult CreateWorkItem(WorkItem createWorkItem)
        {
            var workItem = Mapper.Map<IWorkItem>(createWorkItem);

            workItem.ProjectId = Repository.Projects.Search(proj => proj.Title == createWorkItem.Project.Title)
                .FirstOrDefault().Id;
            workItem.StateId = Repository.WorkItemStates.Search(state => state.StateName == "To Do")
                .FirstOrDefault().Id;
            workItem.TypeId = Repository.WorkItemTypes.Search(type => type.TypeName == createWorkItem.Type.TypeName)
                .FirstOrDefault().Id;
            workItem.CreatorId = Repository.Users.Search(user => user.Email == User.Identity.Name)
                .FirstOrDefault().Id;
            workItem.AppointedToId = Repository.Users.Search(user => createWorkItem.AppointedTo.FirstName.Contains(user.FirstName) && createWorkItem.AppointedTo.FirstName.Contains(user.LastName))
                .FirstOrDefault().Id;

            Repository.WorkItems.Add(workItem);
            Repository.Save();

            var projectId = workItem.ProjectId;
            return RedirectToAction("GetProjectBoardView", "Project", projectId);
        }

        [HttpGet("update")]
        public IActionResult GetUpdateWorkItemView(int id)
        {
            var workItem = Mapper.Map<WorkItem>(Repository.WorkItems.FindById(id));


            return View("UpdateWorkItem", workItem);
        }

        [HttpPost("update")]
        public IActionResult UpdateWorkItem(WorkItem workItem)
        {
          

            //Repository.WorkItems.Modify(existingWorkItem);
            //Repository.Save();

            return RedirectToAction("GetProjectBoardView", "Project" /*, projectId*/);
        }
    }
}