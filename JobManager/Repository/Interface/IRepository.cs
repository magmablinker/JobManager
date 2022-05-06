using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobManager.Repository.Interface
{
    public interface IRepository<T> where T : class
    {
        Task<T> Insert(T value);
        Task<bool> Update(T value);
        Task<bool> Delete(T value);
        Task<T> SelectById(Guid id);
        Task<List<T>> SelectAll();
        Task SaveChangesAsync();
    }
}
