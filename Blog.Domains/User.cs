
namespace Blog.Domains
{
    public class User
    {
        public int id { get; set; }
        public string user_username { get; set; }
        public string user_password { get; set; }
        public string user_email { get; set; }
        public string user_firstname { get; set; }
        public string user_lastname { get; set; }
        public string user_displayname { get; set; }
        public string user_avatar { get; set; }
        public bool user_active { get; set; }
    }
}
