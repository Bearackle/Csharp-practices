using MediatR;
using MyEcommerce.Application.Carts.Commands;
using MyEcommerce.Application.Carts.Queries;
using MyEcommerce.Application.Products.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace MyEcommerce.Web.Controllers
{
    public class CartsController : BaseController
    {
        public CartsController(IMediator mediator) : base(mediator)
        { }
        [Authorize]
        public async Task<ActionResult> Index()
        {
            await _mediator.Send(new CreateUserCartCommand());
            var items = await _mediator.Send(new GetCartItemsQuery());
            return View(items);
        }
        [HttpPost]
        [Authorize]
        public async Task<JsonResult> AddProductToCart(int ProductId, int quantity = 1)
        {
            var product = await _mediator.Send(new GetProductWithIdQuery(ProductId));
            int result = await _mediator.Send(new AddProductToCartCommand(product, quantity));
            if(Session["UserCartCount"] != null)
            {
                Session["UserCartCount"] = (int) Session["UserCartCount"] + 1;
            }
            return Json(new {
                success = result
            });
        }
        [HttpPost]
        public async Task<JsonResult> UpdateQuantity(int CartItemId, int Quantity)
        {
            var result = await _mediator.Send(new UpdateQuantityItemCommand(CartItemId, Quantity));
            return Json(new
            {
                success = result
            });
        }
        [HttpPost]
        public async Task<JsonResult> DeleteProductInCart(int CartItemId)
        {
            await _mediator.Send(new DeleteProductInCartCommand(CartItemId));
            if (Session["UserCartCount"] != null)
            {
                Session["UserCartCount"] = (int)Session["UserCartCount"] - 1;
            }
            return Json(new {  success = 1 });
        }
        [HttpGet]
        public async Task<JsonResult> GetCountCartItems()
        {
            var items = await _mediator.Send(new GetCartItemsQuery()) ;
            return Json(new { count = items == null ? 0 : items.Count()}, JsonRequestBehavior.AllowGet);
        }
    }
}