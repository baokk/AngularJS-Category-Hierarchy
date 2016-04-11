using Blog.Domains;
using Blog.Interfaces;
using Blog.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Description;
using System.Xml.Serialization;

namespace Blog.WebAPI.Controllers
{
    public class CategoryController : ApiController
    {
        #region Fields

        private readonly ICategoryService _categoryService;

        #endregion

        #region Constructor

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        #endregion

        #region Methods

        public IEnumerable<CategoryViewModel> GetCategories()
        {
            var categories = _categoryService.GetAll();
            var categoryGrouped = (from c in categories
                                   where c.category_parent == 0
                                   select new CategoryViewModel
                                   {
                                       category_id = c.category_id,
                                       category_name = c.category_name,
                                       category_slug = c.category_slug,
                                       category_description = c.category_description,
                                       category_parent = c.category_parent,
                                       category_active = c.category_active,
                                       categories = GetCategoryChildren(c.category_id)
                                   }).OrderByDescending(c => c.category_id);
            return categoryGrouped;
        }

        private List<Category> GetCategoryChildren(int categoryId)
        {
            var categoryParents = _categoryService.GetAll()
                .Where(c => c.category_parent == categoryId).ToList();
            var category = _categoryService.GetCategoryById(categoryId);
            var listCategory = new List<Category>();

            var breadCrumb = " - ";

            foreach (var children in categoryParents)
            {

                children.category_name = breadCrumb + children.category_name;

                listCategory.Add(children);
                listCategory.AddRange(GetCategoryChildren(children.category_id));
            }

            return listCategory;
        }

        // GET: api/Categories/5
        [ResponseType(typeof(Category))]
        public IHttpActionResult GetCategory(int id)
        {
            var category = _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        // POST: api/Categories
        [ResponseType(typeof(Category))]
        public IHttpActionResult PostCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryService.InsertCategory(category);
            }
            else
            {
                return BadRequest(ModelState);
            }

            return CreatedAtRoute("DefaultApi", new { id = category.category_id }, category);
        }

        #endregion
    }
}
