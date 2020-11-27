using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Repository.SqlRepository
{
    public class WorkItemRelation : ITable
    {
        [Column("WorkItemRelationId")]
        [Key()]
        public int Id { get; set; }
        [ForeignKey("Parent")]
        public int? ParentId { get; set; }
        public virtual WorkItem Parent { get; set; }
        [ForeignKey("Child")]
        public int? ChildId { get; set; }
        public virtual WorkItem Child { get; set; }
        public bool IsDeleted { get; set; }
    }
}
