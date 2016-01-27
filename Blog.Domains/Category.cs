
namespace Blog.Domains
{
    public class Category
    {
        public int category_id { get; set; }
        public string category_name { get; set; }
        public string category_slug { get; set; }
        public string category_description { get; set; }
        public int category_parent { get; set; }
        public bool category_active {get; set;}
    }
}
