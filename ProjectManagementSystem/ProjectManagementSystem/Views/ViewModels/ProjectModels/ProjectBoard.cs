using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using ProjectManagementSystem.Views.ViewModels.WorkItemModels;

namespace ProjectManagementSystem.Views.ViewModels.ProjectModels
{
    public class ProjectBoard
    {
        public Project Project { get; set; }
        public List<WorkItem> WorkItems { get; set; }
    }
}
