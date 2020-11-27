using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Models.Interfaces
{
    public interface IWorkItemRelation: IIdenitifiable
    {
        int ParentId { get; set; }
        int ChildId { get; set; }
    }
}
