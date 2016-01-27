using Blog.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>list of users</returns>
        IEnumerable<User> GetAllUsers();

        /// <summary>
        /// Insert a user
        /// </summary>
        /// <param name="user">user</param>
        void InsertUser (User user);
    }
}
