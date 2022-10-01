using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransferMate.Data.Repository
{
    public interface IRepository<T>
    {
        public abstract bool Insert(T entity);
        public abstract bool Delete(int key);
        public abstract bool Update(T entity);
        public abstract IEnumerable<T> Select(int? key = 0);
    }
}
