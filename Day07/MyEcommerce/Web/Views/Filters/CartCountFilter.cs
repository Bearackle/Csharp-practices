using MediatR;
using MyEcommerce.Application.Carts.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyEcommerce.Web.Views.Filters
{
    public class CartCountFilter : ActionFilterAttribute
    {
        private readonly IMediator _mediator;

        public CartCountFilter(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var items = _mediator.Send(new GetCartItemsQuery()).Result;
            filterContext.Controller.ViewBag.CartCount = items.Count();
            base.OnActionExecuting(filterContext);
        }
    }
}