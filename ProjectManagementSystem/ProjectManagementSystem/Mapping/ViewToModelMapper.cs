using AutoMapper;
using ProjectManagementSystem.DependencyResolving;
using ProjectManagementSystem.Models.Interfaces;
using ProjectManagementSystem.Views.ViewModels.ProjectModels;
using ProjectManagementSystem.Views.ViewModels.UserModels;
using ProjectManagementSystem.Views.ViewModels.WorkItemModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels = ProjectManagementSystem.Views.ViewModels.UserModels;

namespace ProjectManagementSystem.Mapping
{
    public class ViewToModelMapper : Profile
    {
        public ViewToModelMapper()
        {
            CreateMap<ViewModels.User, IUser>()
                   .ConstructUsing(viewEntity => DependencyContainer.Resolve<IUser>())
                   .ForMember(user => user.UserRole, mapper => mapper.MapFrom(src => DependencyContainer.Resolve<IUserRole>()))
                   .AfterMap((viewEntity, user) => user.UserRole.RoleName = viewEntity.Role);

            //       .ForMember(
            //    dest => dest.UserRole,
            //opt => opt.MapFrom(src => DependencyContainer.Resolve<IUserRole>()));
            // TODO: add role mapping

            CreateMap<IUser, ViewModels.User>()
                .ForMember(
                dest => dest.Role,
            opt => opt.MapFrom(src => src.UserRole.RoleName));


            CreateMap<UserRole, IUserRole>()
                .ConstructUsing(viewEntity => DependencyContainer.Resolve<IUserRole>());

            CreateMap<IUserRole, UserRole>();


            CreateMap<Project, IProject>()
                .ConstructUsing(viewEntity => DependencyContainer.Resolve<IProject>());

            CreateMap<IProject, Project>();


            CreateMap<WorkItemState, IWorkItemState>()
                .ConstructUsing(viewEntity => DependencyContainer.Resolve<IWorkItemState>());

            CreateMap<IWorkItemState, WorkItemState>();


            CreateMap<WorkItemType, IWorkItemType>()
                .ConstructUsing(viewEntity => DependencyContainer.Resolve<IWorkItemType>());

            CreateMap<IWorkItemType, WorkItemType>();


            CreateMap<WorkItem, IWorkItem>()
                .ForMember(iWI => iWI.Creator, mapper => mapper.MapFrom(src => src.Creator))
                .ForMember(iWI => iWI.AppointedTo, mapper => mapper.MapFrom(src => src.AppointedTo))
                .ForMember(iWI => iWI.Project, mapper => mapper.MapFrom(src => src.Project))
                .ForMember(iWI => iWI.State, mapper => mapper.MapFrom(src => src.State))
                .ForMember(iWI => iWI.Type, mapper => mapper.MapFrom(src => src.Type))
                .ConstructUsing(viewEntity => DependencyContainer.Resolve<IWorkItem>());

            CreateMap<IWorkItem, WorkItem>()
                .ForMember(wi => wi.Creator, mapper => mapper.MapFrom(src => src.Creator))
                .ForMember(wi => wi.AppointedTo, mapper => mapper.MapFrom(src => src.AppointedTo))
                .ForMember(wi => wi.Project, mapper => mapper.MapFrom(src => src.Project))
                .ForMember(wi => wi.State, mapper => mapper.MapFrom(src => src.State))
                .ForMember(wi => wi.Type, mapper => mapper.MapFrom(src => src.Type));
        }
    }
}
