using MediatR;
using MyEcommerce.Application.Carts.Queries;
using MyEcommerce.Application.Orders;
using MyEcommerce.Application.Orders.Queries;
using MyEcommerce.Domain.Entities;
using MyEcommerce.Domain.Enums;
using MyEcommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MyEcommerce.Web.Controllers
{
    public class OrdersController : BaseController
    {
        public OrdersController(IMediator _mediator) : base(_mediator) { }
        public async Task<ActionResult> Index()
        {
            var orders = await _mediator.Send(new GetUserOrdersQuery());

            return View(orders);
        }
        public async Task<ActionResult> Checkout()
        {
            var p = await _mediator.Send(new GetCartItemsQuery());
            return View(p);
        }
        public async Task<ActionResult> Create(OrderViewModel model)   
        {
            var ci = await _mediator.Send(new GetCartItemsQuery());
            int result = await _mediator.Send(new CreateCurrentOrderCommand(ci, model));
            if( result == 0)
            {
                return HttpNotFound();
            }
            return RedirectToAction("Pay", new { paymentMethod = model.PaymentMethod });
        }
        public ActionResult Pay(PaymentMethod paymentMethod)
        {
            if (paymentMethod == PaymentMethod.VNPAY)
            {
                
            }
            else if (paymentMethod == PaymentMethod.MOMO)
            {
                
            }
            return RedirectToAction("Index", "Home");
        }
    }
}