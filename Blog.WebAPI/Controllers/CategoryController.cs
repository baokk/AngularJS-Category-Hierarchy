﻿using Blog.Domains;
using Blog.Interfaces;
using Blog.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Description;
using System.Xml.Serialization;
using Microsoft.Ajax.Utilities;

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

        private List<Category> GetCategoryChildren(int categoryId, string hyphen = " - ")
        {
            var categoryParents = _categoryService.GetAll()
                .Where(c => c.category_parent == categoryId).ToList();

            var listCategory = new List<Category>();

            foreach (var child in categoryParents)
            {
                if (child.category_parent == categoryId)
                {
                    child.category_name = hyphen + child.category_name;

                    listCategory.Add(child);
                    listCategory.AddRange(GetCategoryChildren(child.category_id, hyphen + " - "));
                }
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

        // PUT: api/Categories/5
        [ResponseType((typeof(void)))]
        public IHttpActionResult PutCategory(int id, Category categoryModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != categoryModel.category_id)
                return BadRequest();

            var category = _categoryService.GetCategoryById(id);
            category.category_id = categoryModel.category_id;
            category.category_name = categoryModel.category_name;
            category.category_slug = categoryModel.category_slug;
            category.category_description = categoryModel.category_description;
            category.category_parent = categoryModel.category_parent;
            category.category_active = categoryModel.category_active;

            if (categoryModel != null)
                _categoryService.UpdateCategory(category);

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: api/Category/5
        [ResponseType((typeof(Category)))]
        public IHttpActionResult DeleteCategory(int id)
        {
            var category = _categoryService.GetCategoryById(id);
            if (category == null) return NotFound();
            _categoryService.DeleteCategory(category);
            return Ok(category);
        }

        #endregion
    }
}
