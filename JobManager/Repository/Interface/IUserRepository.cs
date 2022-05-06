using JobManager.Core.Data.Model;
using System.Threading.Tasks;

namespace JobManager.Repository.Interface
{
    public interface IUserRepository : IRepository<DbUser>
    {
        public Task<DbUser> SelectByUsername(string username);
        public Task<DbUser> SelectByEmail(string email);
    }
}
