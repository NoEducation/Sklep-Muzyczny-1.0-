using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SklepMuzyczny.Infrastructure
{
    public class AppConfing
    {
        private static string imageFolderPath = ConfigurationManager.AppSettings["ImageFolderPath"];

        public static string ImageFolderPath
        {
            get { return imageFolderPath; }
        }
    }
}