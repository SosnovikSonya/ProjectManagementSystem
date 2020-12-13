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
    public class WorkItemController : BaseController
    {
        public WorkItemController(IMapper mapper, IRepository repository) : base(mapper, repository) { }

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
                .Where(user => user.UserRole.RoleName != "Administrator")
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

            workItem.Project = Repository.Projects.Search(proj => proj.Title == createWorkItem.Project.Title)
                .FirstOrDefault();
            workItem.State = Repository.WorkItemStates.Search(state => state.StateName == "To Do")
                .FirstOrDefault();
            workItem.Type = Repository.WorkItemTypes.Search(type => type.TypeName == createWorkItem.Type.TypeName)
                .FirstOrDefault();
            workItem.Creator = Repository.Users.Search(user => user.Email == User.Identity.Name)
                .FirstOrDefault();
            workItem.AppointedTo = Repository.Users.Search(user => createWorkItem.AppointedTo.FirstName.Contains(user.FirstName) && createWorkItem.AppointedTo.FirstName.Contains(user.LastName))
                .FirstOrDefault();

            Repository.WorkItems.Add(workItem);
            Repository.Save();

            var projectId = workItem.Project.Id;
            return RedirectToAction("GetProjectBoardView", "Project", projectId);
        }

        [HttpGet("update")]
        public IActionResult GetUpdateWorkItemView(int id)
        {
            var workItem = Mapper.Map<WorkItem>(Repository.WorkItems.FindById(id));

            var states = Repository.WorkItemStates
              .Search((state) => true)
              .Select(state => Mapper.Map<WorkItemState>(state))
              .ToList();
            var stateNames = new List<string>();
            foreach (var item in states)
            {
                stateNames.Add(item.StateName);
            }
            ViewBag.States = new SelectList(stateNames);

            var users = Repository.Users
                .Search((user) => true)
                .Where(user => user.UserRole.RoleName != "Administrator")
                .Select(user => Mapper.Map<User>(user))
                .ToList();
            var userNames = new List<string>();
            foreach (var item in users)
            {
                userNames.Add(item.FirstName + " " + item.LastName);
            }
            ViewBag.Users = new SelectList(userNames);

            return View("UpdateWorkItem", workItem);
        }

        [HttpPost("update")]
        public IActionResult UpdateWorkItem(WorkItem workItem)
        {
          

            //Repository.WorkItems.Modify(existingWorkItem);
            //Repository.Save();

            return RedirectToAction("GetProjectBoardView", "Project" /*, projectId*/);
        }

        [HttpGet("all")]
        public IActionResult AllWorkItems()
        {
            var allWorkItems = Repository.WorkItems.Search(wi => true)
               .Select(wi => Mapper.Map<WorkItem>(wi));

            return View("AllWorkItems", allWorkItems);

        }

        
    }
}