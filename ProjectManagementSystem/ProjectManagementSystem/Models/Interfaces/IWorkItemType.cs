using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Models.Interfaces
{
    public interface IWorkItemType: IIdenitifiable
    {
        string TypeName { get; set; }
    }
}
