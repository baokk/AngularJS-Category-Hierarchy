using Blog.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.WebAPI.Models
{
    public class CategoryViewModel
    {
        public int category_id { get; set; }
        public string category_name { get; set; }
        public string category_slug { get; set; }
        public string category_description { get; set; }
        public int category_parent { get; set; }
        public bool category_active { get; set; }
        public List<Category> categories { get; set; }
    }
}