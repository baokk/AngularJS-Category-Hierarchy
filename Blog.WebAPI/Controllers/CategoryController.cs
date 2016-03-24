using Blog.Domains;
using Blog.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

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

        public IEnumerable<Category> GetCategories()
        {
            var categories = _categoryService.GetAll();
            return categories;
        }

        #endregion
    }
}
