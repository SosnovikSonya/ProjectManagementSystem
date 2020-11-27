using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Repository.SqlRepository
{
    public class User : ITable
    {
        [Column("UserId")]
        [Key()]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public virtual UserRole UserRole { get; set; }
        public int UserRoleId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
