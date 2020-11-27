using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Repository.SqlRepository
{
    public class WorkItem : ITable
    {
        [Column("WorkItemId")]
        [Key()]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [ForeignKey("Creator")]
        public int? CreatorId { get; set; }
        public virtual User Creator { get; set; }
        [ForeignKey("AppointedTo")]
        public int? AppointedToId { get; set; }
        public virtual User AppointedTo { get; set; }
        [ForeignKey("Project")]
        public int? ProjectId { get; set; }
        public virtual Project Project { get; set; }
        [ForeignKey("Type")]
        public int? TypeId { get; set; }
        public virtual WorkItemType Type { get; set; }
        [ForeignKey("State")]
        public int? StateId { get; set; }
        public virtual WorkItemState State { get; set; }
        public bool IsDeleted { get; set; }

    }
}
