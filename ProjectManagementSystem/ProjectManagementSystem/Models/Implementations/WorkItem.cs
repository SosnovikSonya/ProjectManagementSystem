using ProjectManagementSystem.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Models.Implementations
{
    public class WorkItem : IWorkItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CreatorId { get; set; }
        public int AppointedToId { get; set; }
        public int ProjectId { get; set; }
        public int TypeId { get; set; }
        public int StateId { get; set; }
    }
}
