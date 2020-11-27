using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Models.Interfaces
{
    public interface IWorkItem: IIdenitifiable
    {
        string Title { get; set; }
        string Description { get; set; }
        int CreatorId { get; set; }
        int AppointedToId { get; set; }
        int ProjectId { get; set; }
        int TypeId { get; set; }
        int StateId { get; set; }
    }
}
