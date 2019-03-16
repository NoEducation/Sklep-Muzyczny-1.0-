using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SklepMuzyczny.Infrastructure
{
    public class AppConfing
    {
        private static string _imageFolderPath = ConfigurationManager.AppSettings["ImageFolderPath"];
        private static string _nameMissingImage = ConfigurationManager.AppSettings["NameMissingImage"];
        public static string ImageFolderPath
        {
            get { return _imageFolderPath; }
        }
        public static string NameMissingImage
        {
            get { return _nameMissingImage; }
        }
    }
}