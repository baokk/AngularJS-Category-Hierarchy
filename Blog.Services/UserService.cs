using Blog.Interfaces;
using System.Collections.Generic;
using System.Xml.Linq;
using Blog.Domains;

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

        #endregion

    }
}
