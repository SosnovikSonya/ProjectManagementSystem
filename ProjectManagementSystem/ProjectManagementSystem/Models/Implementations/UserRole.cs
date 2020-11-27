using ProjectManagementSystem.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Models.Implementations
{
    public class UserRole : IUserRole
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
    }
}
