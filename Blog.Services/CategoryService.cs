using Blog.Interfaces;
using System.Collections.Generic;
using Blog.Domains;

namespace Blog.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Category> GetAll()
        {
            var categories = _unitOfWork.CategoryRepository.GetAll();
            
            return categories;
        }

        public void InsertCategory(Category category)
        {
            _unitOfWork.CategoryRepository.Insert(category);
            _unitOfWork.Commit();
        }

        public void UpdateCategory(Category category)
        {
            _unitOfWork.CategoryRepository.Update(category);
            _unitOfWork.Commit();
        }

        public Category GetCategoryById(int categoryId)
        {
            return _unitOfWork.CategoryRepository.GetById(categoryId);
        }

        public IEnumerable<Category> FindBy(int categoryId)
        {
            return _unitOfWork.CategoryRepository.Find(c => c.category_id == categoryId);
        }

        public void DeleteCategory(Category category)
        {
            var childrenCategory = _unitOfWork.CategoryRepository.Find(c => c.category_parent == category.category_id);
            
            foreach (var childCate in childrenCategory)
            {
                childCate.category_parent = 0;
                _unitOfWork.CategoryRepository.Update(childCate);
            }

            _unitOfWork.CategoryRepository.Delete(category);
            _unitOfWork.Commit();
        }
    }
}
