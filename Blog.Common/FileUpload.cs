using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Common
{
    public class FileUpload
    {
        public static string RandomFileName(string fileName)
        {
            var newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + fileName;
            return newFileName;
        }
    }
}
