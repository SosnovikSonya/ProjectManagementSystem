using System.ComponentModel;

namespace ProjectManagementSystem.Views.ViewModels.ProjectModels
{
    public class Project
    {
        public int Id { get; set; }
        [DisplayName("Название")]
        public string Title { get; set; }
        [DisplayName("Описание")]
        public string Description { get; set; }
    }
}
