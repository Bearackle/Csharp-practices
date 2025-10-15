using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstASPNetApp.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public string Index()
        {
            return "this is my <b> hehe </b> action";
        }
        public string Welcome(int id, string name , int numTimes = 1)
        {
            return HttpUtility.HtmlEncode("Hello student " + name + " with id " + id + " NumTimes is: " + numTimes);
        }
        public ActionResult Login()
        {
            ViewBag.Title = "Login to you account";
            ViewBag.Message = "This is new Knowledge";
            return View();
        }
    }
}