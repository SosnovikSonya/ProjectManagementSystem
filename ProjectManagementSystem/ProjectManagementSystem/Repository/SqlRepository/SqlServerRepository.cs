using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProjectManagementSystem.Models.Interfaces;

namespace ProjectManagementSystem.Repository.SqlRepository
{
    public class SqlServerRepository : IRepository
    {
        private IMapper Mapper { get; set; }
        private MyDbContext DbContext { get; set; }

        public SqlServerRepository(IMapper mapper, MyDbContext dbContext)
        {
            Mapper = mapper;
            DbContext = dbContext;

            Refresh();
        }

        public void Refresh()
        {
            Users = new TableList<IUser, User>(CheckDeletedAndMap<IUser, User>(DbContext.Users.Include(user => user.UserRole)));
            UserRoles = new TableList<IUserRole, UserRole>(CheckDeletedAndMap<IUserRole, UserRole>(DbContext.UserRoles));
            Projects = new TableList<IProject, Project>(CheckDeletedAndMap<IProject, Project>(DbContext.Projects));
            WorkItemTypes = new TableList<IWorkItemType, WorkItemType>(CheckDeletedAndMap<IWorkItemType, WorkItemType>(DbContext.WorkItemTypes));
            WorkItemStates = new TableList<IWorkItemState, WorkItemState>(CheckDeletedAndMap<IWorkItemState, WorkItemState>(DbContext.WorkItemStates));
            WorkItems = new TableList<IWorkItem, WorkItem>(CheckDeletedAndMap<IWorkItem, WorkItem>(DbContext.WorkItems));
            WorkItemRelations = new TableList<IWorkItemRelation, WorkItemRelation>(CheckDeletedAndMap<IWorkItemRelation, WorkItemRelation>(DbContext.WorkItemRelations));

            Users.ElementAdded += (IUser item) => HandleItemAddedOrUpdated(item, typeof(User));
            Users.ElementUpdated += (IUser item) => HandleItemAddedOrUpdated(item, typeof(User));
            Users.ElementDeleted += (IUser item) => HandeItemDeleted(item, typeof(User));

            UserRoles.ElementAdded += (IUserRole item) => HandleItemAddedOrUpdated(item, typeof(UserRole));
            UserRoles.ElementUpdated += (IUserRole item) => HandleItemAddedOrUpdated(item, typeof(UserRole));
            UserRoles.ElementDeleted += (IUserRole item) => HandeItemDeleted(item, typeof(UserRole));

            Projects.ElementAdded += (IProject item) => HandleItemAddedOrUpdated(item, typeof(Project));
            Projects.ElementUpdated += (IProject item) => HandleItemAddedOrUpdated(item, typeof(Project));
            Projects.ElementDeleted += (IProject item) => HandeItemDeleted(item, typeof(Project));

            WorkItemTypes.ElementAdded += (IWorkItemType item) => HandleItemAddedOrUpdated(item, typeof(WorkItemType));
            WorkItemTypes.ElementUpdated += (IWorkItemType item) => HandleItemAddedOrUpdated(item, typeof(WorkItemType));
            WorkItemTypes.ElementDeleted += (IWorkItemType item) => HandeItemDeleted(item, typeof(WorkItemType));

            WorkItemStates.ElementAdded += (IWorkItemState item) => HandleItemAddedOrUpdated(item, typeof(WorkItemState));
            WorkItemStates.ElementUpdated += (IWorkItemState item) => HandleItemAddedOrUpdated(item, typeof(WorkItemState));
            WorkItemStates.ElementDeleted += (IWorkItemState item) => HandeItemDeleted(item, typeof(WorkItemState));

            WorkItems.ElementAdded += (IWorkItem item) => HandleItemAddedOrUpdated(item, typeof(WorkItem));
            WorkItems.ElementUpdated += (IWorkItem item) => HandleItemAddedOrUpdated(item, typeof(WorkItem));
            WorkItems.ElementDeleted += (IWorkItem item) => HandeItemDeleted(item, typeof(WorkItem));

            WorkItemRelations.ElementAdded += (IWorkItemRelation item) => HandleItemAddedOrUpdated(item, typeof(WorkItemRelation));
            WorkItemRelations.ElementUpdated += (IWorkItemRelation item) => HandleItemAddedOrUpdated(item, typeof(WorkItemRelation));
            WorkItemRelations.ElementDeleted += (IWorkItemRelation item) => HandeItemDeleted(item, typeof(WorkItemRelation));
        }

        public ITableList<IUser> Users { get; set; }
        public ITableList<IUserRole> UserRoles { get; set; }
        public ITableList<IProject> Projects { get; set; }
        public ITableList<IWorkItemType> WorkItemTypes { get; set; }
        public ITableList<IWorkItemState> WorkItemStates { get; set; }
        public ITableList<IWorkItem> WorkItems { get; set; }
        public ITableList<IWorkItemRelation> WorkItemRelations { get; set; }


        public void Save()
        {
            DbContext.SaveChanges();
            Refresh();
        }

        private void HandleItemAddedOrUpdated(IIdenitifiable item, Type dbType)
        {
            DbContext.AddOrUpdate(Mapper.Map(item, item.GetType(), dbType) as ITable);
        }

        private void HandeItemDeleted(IIdenitifiable item, Type dbType)
        {
            var obj = Mapper.Map(item, item.GetType(), dbType) as ITable;
            obj.IsDeleted = true;
            DbContext.AddOrUpdate(obj);
        }

        private List<TTarget> CheckDeletedAndMap<TTarget, TDb>(IEnumerable<TDb> dbCollection) where TDb : class, ITable
        {
            var resultCollection = new List<TTarget>();
            foreach (var dbItem in dbCollection)
            {
                if (dbItem.IsDeleted)
                {
                    continue;
                }
                var dbItemReserialized = JsonConvert.DeserializeObject<TDb>(
                        JsonConvert.SerializeObject(dbItem,
                            Formatting.Indented,
                            new JsonSerializerSettings
                            {
                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                            }));
                var itarget = Mapper.Map<TTarget>(dbItemReserialized);
                resultCollection.Add(itarget);
            }
            return resultCollection;
        }
    }
}
