using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyEcommerce.Web.Controllers
{
    public class OrdersController : BaseController
    {
        public OrdersController(IMediator _mediator) : base(_mediator) { }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Checkout()
        {
            return View();
        }
    }
}