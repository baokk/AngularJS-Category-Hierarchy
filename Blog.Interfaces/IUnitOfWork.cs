using Blog.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        IRepository<Category> CategoryRepository { get; }
        IRepository<User> UserRepository { get; }
    }
}
