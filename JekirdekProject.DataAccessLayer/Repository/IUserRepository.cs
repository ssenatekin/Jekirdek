using JekirdekProject.DataAccessLayer.EntityFramework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JekirdekProject.DataAccessLayer.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserByUsername(string username);
        Task<User> ValidateUser(string username, string password);
    }
}
