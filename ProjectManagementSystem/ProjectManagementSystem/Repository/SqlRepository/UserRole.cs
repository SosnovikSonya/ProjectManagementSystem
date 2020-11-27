using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Repository.SqlRepository
{
    public class UserRole : ITable
    {
        [Column("UserRoleId")]
        [Key()]
        public int Id { get; set; }
        public string RoleName { get; set; }
        public bool IsDeleted { get; set; }
        public virtual List<User> User { get; set; }
    }
}
