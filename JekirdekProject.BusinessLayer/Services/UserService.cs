using JekirdekProject.DataAccessLayer.EntityFramework;
using JekirdekProject.DataAccessLayer.Repository;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JekirdekProject.BusinessLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> AuthenticateUser(string username, string password)
        {
            var user = await _userRepository.GetUserByUsername(username);
            if (user == null || user.Password != password) // Normalde şifre hash'lenmelidir!
            {
                return null;
            }
            return user;
        }
    }
}
