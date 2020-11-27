using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using ProjectManagementSystem.Views.ViewModels.UserModels;
using ProjectManagementSystem.Views.ViewModels.ProjectModels;

namespace ProjectManagementSystem.Views.ViewModels.WorkItemModels
{
    public class WorkItem
    {
        public int Id { get; set; }
        [DisplayName("Название")]
        public string Title { get; set; }
        [DisplayName("Описание")]
        public string Description { get; set; }
        [DisplayName("Создал")]
        public User Creator { get; set; }
        [DisplayName("Назначен")]
        public User AppointedTo { get; set; }
        [DisplayName("Проект")]
        public Project Project { get; set; }
        [DisplayName("Тип")]
        public WorkItemType Type { get; set; }
        [DisplayName("Состояние")]
        public WorkItemState State { get; set; }
    }
}
