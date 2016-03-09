using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Common
{
    public class Encrypt
    {
        public static string EncryptString(string str)
        {
            HashAlgorithm hash = new SHA256Managed();
            var plainTextBytes = Encoding.UTF8.GetBytes(str);
            var hashBytes = Convert.ToBase64String(hash.ComputeHash(plainTextBytes));

            return hashBytes;
        }
    }
}
