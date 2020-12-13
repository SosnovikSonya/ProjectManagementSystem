using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Repository;
using ProjectManagementSystem.Views.ViewModels.ProjectModels;
using ProjectManagementSystem.Views.ViewModels.WorkItemModels;
using ProjectManagementSystem.Models.Interfaces;

namespace ProjectManagementSystem.Controllers
{
    [Route("project")]
    public class ProjectController : BaseController
    {
        public ProjectController(IMapper mapper, IRepository repository) : base(mapper, repository) { }

        [HttpGet("create")]
        public IActionResult GetProjectCreationView()
        {
            RedirectToAction("CreateProject", "Project");
            return View("CreateProject");
        }

        [HttpPost("create")]
        public IActionResult CreateProject(Project createProject)
        {
            IProject project = Mapper.Map<IProject>(createProject);

            Repository.Projects.Add(project);
            Repository.Save();

            var projectId = Repository.Projects.Search(pr => true).Last().Id;
            return GetProjectBoardView(projectId);
        }


        [HttpGet("board/{id}")]
        public IActionResult GetProjectBoardView([FromRoute]int id)
        {
            var projectBoard = new ProjectBoard();
            var project = Mapper.Map<Project>(Repository.Projects.FindById(id));
            var workItems = Repository.WorkItems
                .Search(workItem => workItem.Project.Id == id)
                .Select(workItem => Mapper.Map<WorkItem>(workItem))
                .ToList();

            foreach (var workItem in workItems)
            {
                var asd = Repository.WorkItems.Search(wi => wi.Id == workItem.Id).FirstOrDefault().State.Id;
                workItem.State = Mapper.Map<WorkItemState>(Repository.WorkItemStates.Search(state => state.Id == asd).FirstOrDefault());
            }
            projectBoard.Project = project;
            projectBoard.WorkItems = workItems;
            return View("ProjectBoard", projectBoard);
        }
        
    }
}