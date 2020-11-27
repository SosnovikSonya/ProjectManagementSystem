using ProjectManagementSystem.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Repository
{
    public interface IRepository
    {
        ITableList<IUser> Users { get; set; }
        ITableList<IUserRole> UserRoles { get; set; }
        ITableList<IProject> Projects { get; set; }
        ITableList<IWorkItemType> WorkItemTypes { get; set; }
        ITableList<IWorkItemState> WorkItemStates { get; set; }
        ITableList<IWorkItem> WorkItems { get; set; }
        ITableList<IWorkItemRelation> WorkItemRelations { get; set; }


        void Save();
        void Refresh();
    }
}
