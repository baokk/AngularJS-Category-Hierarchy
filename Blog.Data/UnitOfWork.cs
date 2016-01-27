using Blog.Interfaces;
using System;
using Blog.Domains;

namespace Blog.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private IRepository<Category> _categoryRepository;
        private IRepository<User> _userRepository;
        private readonly BlogContext _blogContext;

        public UnitOfWork(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        public IRepository<Category> CategoryRepository
        {
            get
            {
                if (_categoryRepository == null)
                    _categoryRepository = new GenericRepository<Category>(_blogContext);
                return _categoryRepository;
            }
        }

        public IRepository<User> UserRepository
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new GenericRepository<User>(_blogContext);
                return _userRepository;
            }
        }

        public void Commit()
        {
            _blogContext.SaveChanges();
        }

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _blogContext.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
