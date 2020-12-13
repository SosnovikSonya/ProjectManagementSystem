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
        IUser Creator { get; set; }
        IUser AppointedTo { get; set; }
        IProject Project { get; set; }
        IWorkItemType Type { get; set; }
        IWorkItemState State { get; set; }
    }
}
