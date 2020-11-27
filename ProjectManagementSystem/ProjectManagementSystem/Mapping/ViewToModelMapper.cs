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
                   .ForMember(viewEntity => viewEntity.UserRole, mapper => mapper.MapFrom(src => DependencyContainer.Resolve<IUserRole>()));

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


            CreateMap<WorkItem, IWorkItem>().
                ConstructUsing(viewEntity => DependencyContainer.Resolve<IWorkItem>());

            CreateMap<IWorkItem, WorkItem>();

        }
    }
}
