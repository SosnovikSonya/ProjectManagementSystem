
using ProjectManagementSystem.Models.Implementations;
using ProjectManagementSystem.Models.Interfaces;
using StructureMap;

namespace ProjectManagementSystem.DependencyResolving
{
    public class DependencyContainer
    {
        private static Container container;

        public static void RegisterDependencies()
        {
            container = new Container((config) =>
            {
                config.For<IUser>().Use<User>();
                config.For<IUserRole>().Use<UserRole>();
                config.For<IProject>().Use<Project>();
                config.For<IWorkItemType>().Use<WorkItemType>();
                config.For<IWorkItemState>().Use<WorkItemState>();
                config.For<IWorkItem>().Use<WorkItem>();
                config.For<IWorkItemRelation>().Use<WorkItemRelation>();
            });
        }

        public static T Resolve<T>()
        {
            return container.GetInstance<T>();
        }
    }
}
