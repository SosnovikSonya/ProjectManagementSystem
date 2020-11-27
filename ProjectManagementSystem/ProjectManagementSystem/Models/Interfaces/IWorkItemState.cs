using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Models.Interfaces
{
    public interface IWorkItemState : IIdenitifiable
    {
        string StateName { get; set; }
    }
}
