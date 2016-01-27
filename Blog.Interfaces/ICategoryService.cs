using Blog.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Interfaces
{
    public interface ICategoryService
    {
        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns>list of categories</returns>
        IEnumerable<Category> GetAll();

        /// <summary>
        /// Finds a category with the specified criteria
        /// </summary>
        /// <param name="categoryId">Criteria to match on</param>
        /// <returns>A collection containing the results of the query</returns>
        IEnumerable<Category> FindBy(int categoryId);

        /// <summary>
        /// Get category by category id
        /// </summary>
        /// <param name="categoryId">categoryId</param>
        /// <returns>category</returns>
        Category GetCategoryById(int categoryId);

        /// <summary>
        /// Insert a category
        /// </summary>
        /// <param name="category">category</param>
        void InsertCategory(Category category);

        /// <summary>
        /// Update a category
        /// </summary>
        /// <param name="category">category</param>
        void UpdateCategory(Category category);

        /// <summary>
        /// Delete a category
        /// </summary>
        /// <param name="category">category</param>
        void DeleteCategory(Category category);
        
    }
}
