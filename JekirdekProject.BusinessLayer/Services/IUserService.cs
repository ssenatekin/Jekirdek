using JekirdekProject.DataAccessLayer.EntityFramework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JekirdekProject.BusinessLayer.Services
{
    public interface IUserService
    {
        Task<User> AuthenticateUser(string username, string password);
    }
}
