using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyEcommerce.App_Start
{
    public class CustomRazorView : RazorViewEngine
    {
        public CustomRazorView()
        {
            ViewLocationFormats = new[]
       {
            "~/Web/Views/{1}/{0}.cshtml",
            "~/Web/Views/Shared/{0}.cshtml",
            "~/Views/{1}/{0}.cshtml",      // giữ fallback về mặc định
            "~/Views/Shared/{0}.cshtml"
        };

            PartialViewLocationFormats = ViewLocationFormats;
            MasterLocationFormats = ViewLocationFormats;
        }
    }
}