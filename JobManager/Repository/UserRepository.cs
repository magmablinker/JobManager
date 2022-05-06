using JobManager.Core.Data.Model;
using JobManager.Core.Database;
using JobManager.Repository.Base;
using JobManager.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobManager.Repository
{
    public class UserRepository : GenericRepository<DbUser>, IUserRepository
    {

        public UserRepository(JobManagerContext context) : base(context) { }

        public async Task<DbUser> SelectByUsername(string username)
        {
            return await _context.Users.Where(user => user.Username == username).FirstOrDefaultAsync();
        }

        public async Task<DbUser> SelectByEmail(string email)
        {
            return await _context.Users.Where(user => user.EmailAddress == email).FirstOrDefaultAsync();
        }

    }
}
