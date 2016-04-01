using System;
using Blog.Domains;
using Blog.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using Blog.Common;
using System.Net;

namespace Blog.WebAPI.Controllers
{
    public class UserController : ApiController
    {
        #region Fields

        private readonly IUserService _userService;

        #endregion

        #region Constructors

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
            var users = _userService.GetAllUsers()
                .OrderByDescending(u => u.user_username);
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
                    user.user_password = Encrypt.EncryptString(user.user_password);
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
        /// Return a single User
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

        /// <summary>
        /// Update a single User
        /// </summary>
        /// <param name="id">The user id</param>
        /// <param name="user">The user objects</param>
        /// <returns></returns>
        [ResponseType((typeof(void)))]
        public IHttpActionResult PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != user.id)
                return BadRequest();

            var userDetail = _userService.GetUserById(id);
            userDetail.id = user.id;
            userDetail.user_username = user.user_username;
            userDetail.user_password = Encrypt.EncryptString(user.user_password);
            userDetail.user_email = user.user_email;
            userDetail.user_firstname = user.user_firstname;
            userDetail.user_lastname = user.user_lastname;
            userDetail.user_avatar = user.user_avatar;
            userDetail.user_displayname = user.user_displayname;
            userDetail.user_active = user.user_active;

            _userService.UpdateUser(userDetail);

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Delete a user
        /// </summary>
        /// <param name="id">The User id</param>
        /// <returns></returns>
        [ResponseType((typeof(User)))]
        public IHttpActionResult DeleteUser(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null) return NotFound();
            _userService.DeleteUser(user);
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
