using ProjectManagementSystem.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Models.Implementations
{
    public class WorkItemType : IWorkItemType
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
    }
}
