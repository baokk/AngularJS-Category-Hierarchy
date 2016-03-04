using System;
using Blog.Domains;
using Blog.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web.Http;
using System.Web.Http.Description;
using Blog.Common;

namespace Blog.WebAPI.Controllers
{
    public class UserController : ApiController
    {
        #region Fields

        private readonly IUserService _userService;

        #endregion

        #region Contructors

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Return the User collection
        /// </summary>
        /// <returns>users</returns>
        public IEnumerable<User> GetUsers()
        {
            var users = _userService.GetAllUsers().ToList();
            return users;
        }

        /// <summary>
        /// Insert a user
        /// </summary>
        /// <param name="user">The User objects</param>
        /// <returns></returns>
        [ResponseType((typeof(User)))]
        public IHttpActionResult PostUser(User user)
        {
            try
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

                return CreatedAtRoute("DefaultApi", new { user.id }, user);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Return a single Product
        /// </summary>
        /// <param name="id">The Product id</param>
        /// <returns></returns>
        [ResponseType((typeof(User)))]
        public IHttpActionResult GetUser(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Check a value already exist in database
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        /// <param name="property">property</param>
        /// <returns>value</returns>
        [Route("api/User/CheckUniqueValue")]
        [HttpGet]
        public bool CheckUniqueValue(int key, string value, string property)
        {
            switch (property.ToLower())
            {
                case "username":
                    var user = _userService.GetAllUsers()
                    .Where(u => u.id != key && u.user_username == value)
                    .ToList();
                    return user.Count == 0;

                case "email":
                    var email = _userService.GetAllUsers()
                    .Where(u => u.id != key && u.user_email == value)
                    .ToList();
                    return email.Count == 0;
            }

            return false;
        }

        #endregion
    }
}
