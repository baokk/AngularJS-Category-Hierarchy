using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Common;

namespace Blog.WebAPI.Controllers
{
    public class HelperController : Controller
    {
        /// <summary>
        /// Save file to folder
        /// </summary>
        /// <returns>Json</returns>
        [HttpPost]
        public JsonResult UploadFile()
        {
            string Message, fileName;
            Message = fileName = string.Empty;
            bool flag = false;

            if (Request.Files != null)
            {
                try
                {
                    var file = Request.Files[0];
                    fileName = FileUpload.RandomFileName(file.FileName);
                    file.SaveAs(Path.Combine(Server.MapPath("~/Uploads/avatars"), fileName));
                    Message = "File uploaded";
                    flag = true;
                }
                catch (Exception)
                {
                    Message = "File upload failed! Please try again";
                }
            }

            return Json(fileName);
        }

        /// <summary>
        /// Delete file in a folder
        /// </summary>
        /// <param name="fileName">name of image</param>
        [HttpPost]
        public void DeleteFile(string fileName)
        {
            string path = Server.MapPath("~/Uploads/avatars/" + fileName);
            var file = new FileInfo(path);
            if (file.Exists) file.Delete();
        }
    }
}