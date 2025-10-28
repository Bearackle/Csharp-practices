using MediatR;
using MyEcommerce.Application.Carts.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyEcommerce.Web.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IMediator _mediator;
        public BaseController(IMediator mediator)
        {
            _mediator = mediator;
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.CartCount = Session["UserCartCount"] ?? 0;
            base.OnActionExecuting(filterContext);
        }
    }
}