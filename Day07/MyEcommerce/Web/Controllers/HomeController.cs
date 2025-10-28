using MediatR;
using MyEcommerce.Application.Carts.Queries;
using MyEcommerce.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MyEcommerce.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IMediator mediator) : base(mediator)
        {
        }
        public async Task<ActionResult> Index()
        {
            if (Session["UserCartCount"] == null)
            {
                var items = await _mediator.Send(new GetCartItemsQuery());
                Session["UserCartCount"] = items.Count();
                ViewBag.CartCount = Session["UserCartCount"];
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}