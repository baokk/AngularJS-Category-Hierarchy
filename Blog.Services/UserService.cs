using Blog.Interfaces;
using System.Collections.Generic;
using System.Xml.Linq;
using Blog.Domains;
using System;

namespace Blog.Services
{
    public class UserService : IUserService
    {
        #region Fields

        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Contructor

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Methods

        private void Save()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _unitOfWork.UserRepository.GetAll();
        }

        public void InsertUser(User user)
        {
            _unitOfWork.UserRepository.Insert(user);
            Save();
        }

        public User GetUserById(int id)
        {
            var user = _unitOfWork.UserRepository.GetById(id);
            return user;
        }

        #endregion

    }
}
