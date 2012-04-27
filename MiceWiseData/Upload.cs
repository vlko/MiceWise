using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace MiceWiseData
{
    public static class Upload
    {
        public const int MaxFileSize = 1 * 1024 * 1024;
        public static string[] AllowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff" };

        /// <summary>
        /// Gets the user temp path.
        /// </summary>
        /// <value>The user temp path.</value>
        public static string UserTempPath
        {
            get
            {
                return Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "App_Data\\UserTemp");
            }
        }

        public static string UserPath
        {
            get
            {
                return Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "Users");
            }
        }
    }
}
