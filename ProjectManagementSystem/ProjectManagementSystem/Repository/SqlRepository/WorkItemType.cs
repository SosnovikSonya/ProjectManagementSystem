using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Repository.SqlRepository
{
    public class WorkItemType : ITable
    {
        [Column("WorkItemTypeId")]
        [Key()]
        public int Id { get; set; }
        public string TypeName { get; set; }
        public bool IsDeleted { get; set; }
    }
}
