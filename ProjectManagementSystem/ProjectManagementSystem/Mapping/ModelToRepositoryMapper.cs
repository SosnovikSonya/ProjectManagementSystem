using AutoMapper;
using ProjectManagementSystem.Models.Interfaces;
using ProjectManagementSystem.Repository.SqlRepository;
using ProjectManagementSystem.DependencyResolving;

namespace ProjectManagementSystem.Mapping
{
    public class ModelToRepositoryMapper: Profile
    {
        public ModelToRepositoryMapper()
        {
            CreateMap<IUserRole, UserRole>();

            CreateMap<UserRole, IUserRole>()
                    .ConstructUsing(dbEntity => DependencyContainer.Resolve<IUserRole>());

            CreateMap<IUser, User>()
                .ForMember(user => user.Id, mapper => mapper.MapFrom(src => src.Id))
                .ForMember(user => user.UserRole, mapper => mapper.MapFrom(src => src.UserRole));
            
            CreateMap<User, IUser>()
                    .ConstructUsing(dbEntity => DependencyContainer.Resolve<IUser>())
                    .ForMember(user => user.UserRole, mapper => mapper.MapFrom(src => src.UserRole));

            CreateMap<IProject, Project>();

            CreateMap<Project, IProject>()
                    .ConstructUsing(dbEntity => DependencyContainer.Resolve<IProject>());

            CreateMap<IWorkItemState, WorkItemState>();

            CreateMap<WorkItemState, IWorkItemState>()
                    .ConstructUsing(dbEntity => DependencyContainer.Resolve<IWorkItemState>());

            CreateMap<IWorkItemType, WorkItemType>();

            CreateMap<WorkItemType, IWorkItemType>()
                    .ConstructUsing(dbEntity => DependencyContainer.Resolve<IWorkItemType>());

            CreateMap<IWorkItem, WorkItem>();

            CreateMap<WorkItem, IWorkItem>()
                    .ConstructUsing(dbEntity => DependencyContainer.Resolve<IWorkItem>());

            CreateMap<IWorkItemRelation, WorkItemRelation>();

            CreateMap<WorkItemRelation, IWorkItemRelation>()
                    .ConstructUsing(dbEntity => DependencyContainer.Resolve<IWorkItemRelation>());
        }       

    }
}
