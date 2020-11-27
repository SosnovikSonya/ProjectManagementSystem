using ProjectManagementSystem.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Repository.SqlRepository
{
    public class TableList<T, TDb> : ITableList<T> where T : IIdenitifiable where TDb : ITable
    {
        private IList<T> List { get; set; }

        public event Action<T> ElementAdded;
        public event Action<T> ElementUpdated;
        public event Action<T> ElementDeleted;


        public TableList(IList<T> list)
        {
            List = list;
        }

        public T FindById(int id)
        {
            return List.SingleOrDefault(item => item.Id == id);
        }

        public void Add(T element)
        {
            List.Add(element);
            ElementAdded.Invoke(element);
        }

        public void Delete(int id)
        {
            var element = List.Single(a => a.Id == id);
            List.Remove(element);
            ElementDeleted.Invoke(element);
        }

        public void Modify(T element)
        {
            List.Remove(List.Single(a => a.Id == element.Id));
            List.Add(element);
            ElementUpdated.Invoke(element);
        }

        IEnumerable<T> ITableList<T>.Search(Func<T, bool> searchPredicate)
        {
            return List.Where(searchPredicate);
        }
    }
}
