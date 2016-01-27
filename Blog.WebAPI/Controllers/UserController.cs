using System;
using Blog.Domains;
using Blog.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web.Http;
using System.Web.Http.Description;

namespace Blog.WebAPI.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Get all Users
        /// </summary>
        /// <returns>users</returns>
        public IEnumerable<User> GetUsers()
        {
            var users = _userService.GetAllUsers().ToList();
            return users;
        }

        [ResponseType((typeof (User)))]
        public IHttpActionResult PostUser(User user)
        {
            if (ModelState.IsValid)
            {
                // encrypt password in SHA256
                HashAlgorithm hash = new SHA256Managed();
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(user.user_password);
                var hashBytes = hash.ComputeHash(plainTextBytes);

                user.user_password = Convert.ToBase64String(hashBytes);

                _userService.InsertUser(user);
            }
            else
            {
                return BadRequest((ModelState));
            }

            return CreatedAtRoute("DefaultApi", new {user.id}, user);
        }
    }
}
