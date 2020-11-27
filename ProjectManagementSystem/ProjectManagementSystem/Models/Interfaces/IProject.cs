using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Models.Interfaces
{
    public interface IProject : IIdenitifiable
    {
        string Title { get; set; }
        string Description { get; set; }
    }
}
