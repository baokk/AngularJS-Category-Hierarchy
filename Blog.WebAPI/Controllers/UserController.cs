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
        /// Get all Users
        /// </summary>
        /// <returns>users</returns>
        public IEnumerable<User> GetUsers()
        {
            var users = _userService.GetAllUsers().ToList();
            return users;
        }

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

        [Route("api/User/CheckUserNameExists")]
        [HttpGet]
        public bool CheckUserNameExists(string userName)
        {
            var user = _userService.GetAllUsers()
                .Where(u => u.user_username == userName)
                .ToList();
            return user.Count > 0;
            //if (user == true)
            //    return true;
            //else
            //    return false;
        }

        [Route("api/User/CheckEmailExists")]
        [HttpGet]
        public bool CheckEmailExists(string email)
        {
            var user = _userService.GetAllUsers()
                .Where(u => u.user_email == email)
                .ToList();
            return user.Count > 0;
        }

        #endregion
    }
}
