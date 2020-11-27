using ProjectManagementSystem.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Repository
{
    public interface ITableList<T> where T : IIdenitifiable
    {
        event Action<T> ElementAdded;
        event Action<T> ElementUpdated;
        event Action<T> ElementDeleted;

        T FindById(int id);
        void Add(T element);
        void Delete(int id);
        void Modify(T element);
        IEnumerable<T> Search(Func<T, bool> searchPredicate);
    }
}
