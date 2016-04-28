using Blog.Domains;
using Blog.Interfaces;
using Blog.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Test.Services
{
    [TestClass]
    public class UserServiceTest
    {
        private IUserService _service;
        Mock<IUnitOfWork> _mockUnitOfWork;
        List<User> listUser;

        [TestInitialize]
        public void Initialize()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _service = new UserService(_mockUnitOfWork.Object);
            listUser = new List<User>()
            {
                new User() {id = 1, user_username = "abcdef", user_password = "123456", user_lastname = "abc", user_firstname = "def", user_email = "abc@xyz.com", user_displayname = "abc def", user_avatar = "abcxyz.jpg", user_active = true },
                new User() {id = 2, user_username = "qwerty", user_password = "1234567", user_lastname = "abc", user_firstname = "def", user_email = "abc@xyz.com", user_displayname = "abc def", user_avatar = "abcxyz.jpg", user_active = true },
                new User() {id = 3, user_username = "zxcvbnm", user_password = "1234568", user_lastname = "abc", user_firstname = "def", user_email = "abc@xyz.com", user_displayname = "abc def", user_avatar = "abcxyz.jpg", user_active = true }
            };
        }

        [TestMethod]
        public void User_Get_All()
        {
            //Arrange
            _mockUnitOfWork.Setup(x => x.UserRepository.GetAll()).Returns(listUser);

            //Act
            List<User> results = _service.GetAllUsers() as List<User>;

            //Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(3, results.Count);
        }

        [TestMethod]
        public void Can_Add_User()
        {
            //Arrange
            var user = new User()
            {
                id = 1,
                user_username = "abc",
                user_password = "123456",
                user_lastname = "abc",
                user_firstname = "def",
                user_email = "abc@xyz.com",
                user_displayname = "abc def",
                user_avatar = "abcxyz.jpg",
                user_active = true
            };

            _mockUnitOfWork.Setup(x => x.UserRepository.Insert(user));

            //Act
            _service.InsertUser(user);

            //Assert
            Assert.AreNotEqual(0, user.id);

            Assert.IsNotNull(user.user_username);
            Assert.AreNotEqual("", user.user_username);
            Assert.AreNotEqual(null, user.user_username);

            Assert.IsNotNull(user.user_email);
            Assert.AreNotEqual("", user.user_email);
            Assert.AreNotEqual(null, user.user_email);
            
        }
    }
}
