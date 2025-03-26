using JekirdekProject.DataAccessLayer.Concrete;
using JekirdekProject.DataAccessLayer.EntityFramework;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JekirdekProject.DataAccessLayer.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly Context _context;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(Context context, ILogger<UserRepository> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<User> GetUserByUsername(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<User> ValidateUser(string username, string password)
        {
            return await _context.Users .FirstOrDefaultAsync(u => u.UserName == username && u.Password == password);
        }
    }
}
