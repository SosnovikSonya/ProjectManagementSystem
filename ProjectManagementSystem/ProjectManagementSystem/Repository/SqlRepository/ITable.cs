using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Repository.SqlRepository
{
    public interface ITable
    {
        int Id { get; set; }
        bool IsDeleted { get; set; }
    }
}
