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
        public IUser Creator { get; set; }
        public IUser AppointedTo { get; set; }
        public IProject Project { get; set; }
        public IWorkItemType Type { get; set; }
        public IWorkItemState State { get; set; }
    }
}
