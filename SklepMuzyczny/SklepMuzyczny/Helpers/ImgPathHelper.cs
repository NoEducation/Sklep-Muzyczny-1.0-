﻿using SklepMuzyczny.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using System.Web;
using System.Web.Mvc;

namespace SklepMuzyczny.Helpers
{
    public  static class ImgPathHelper
    {
        public static string ImagePathGenerator(this UrlHelper helper,string ImageName)
        {

            string path;
 
            if(ImageName == null)
                path = Path.Combine(AppConfing.ImageFolderPath, AppConfing.NameMissingImage);
            else
                path = Path.Combine(AppConfing.ImageFolderPath, ImageName);
            var resultPath = helper.Content(path);
            return resultPath;
        }
    }
}