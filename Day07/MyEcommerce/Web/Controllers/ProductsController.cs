using CloudinaryDotNet.Actions;
using MediatR;
using MyEcommerce.Application.Categories.Queries;
using MyEcommerce.Application.Products.Commands;
using MyEcommerce.Application.Products.Queries;
using MyEcommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace MyEcommerce.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IMediator _mediator;
        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }        
        public async Task<ActionResult> Index()
        {
            var query = new GetProductQuery();
            var result = await _mediator.Send(query);
            List<Category> categories = (await _mediator.Send(new GetCategoriesQuery())).ToList();
            ViewBag.Categories = categories;
            return View(result);
        }
        public async Task<ActionResult> Create()
        {
            IEnumerable<Category> categories = await _mediator.Send(new GetCategoriesQuery());
            ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName");
            Product product = new Product()
            {
                Attributes = new List<ProductAttribute>()
            };
            return View(product);
        }
        [HttpPost]
        public async Task<ActionResult> Create(Product product, HttpPostedFileBase[] NewImages)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<Category> categories = await _mediator.Send(new GetCategoriesQuery());
                ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName", product.CategoryId);
                return View(product);
            }
            if (NewImages != null && NewImages.Any(f => f != null && f.ContentLength > 0))
            {
                var CreateImagesPath = await _mediator.Send(new UploadFileCloudinaryCommand(NewImages));
                product.Images = new List<ProductImage>();
                foreach (string path in CreateImagesPath)
                {
                    product.Images.Add(new ProductImage()
                    {
                        ImagePath = path,
                    });
                }
            }
            await _mediator.Send(new CreateProductCommand(product));
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> Edit(int id)
        {
            Product product = await _mediator.Send(new GetProductWithIdQuery(id));
            if (product == null)
            {
                return HttpNotFound();
            }
            List<Category> categories = (await _mediator.Send(new GetCategoriesQuery())).ToList();
            ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName");
            return View(product);
        }
        [HttpPost]
        public async Task<ActionResult> Edit(Product product, HttpPostedFileBase[] NewImages)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<Category> categories = await _mediator.Send(new GetCategoriesQuery());
                ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName", product.CategoryId);
                return View(product);
            }
            if (NewImages != null && NewImages.Any(f => f != null && f.ContentLength > 0))
            {
                var CreateImagesPath = await _mediator.Send(new UploadFileCloudinaryCommand(NewImages));
                product.Images = new List<ProductImage>();
                foreach (string path in CreateImagesPath)
                {
                    product.Images.Add(new ProductImage()
                    {
                        ImagePath = path,
                    });
                }
            }
            await _mediator.Send(new UpdateProductCommad(product)); 
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> Details(int id = 0)
        {
            Product p = await _mediator.Send(new GetProductWithIdQuery(id));
            if(p == null)
            {
                return HttpNotFound();
            }
            return View(p);
        }
        [HttpPost]
        public async Task<Unit> RemoveImage(int ImageId) 
        {
            await _mediator.Send(new RemoveImageCommand(ImageId));
            return Unit.Value;
        }
        [HttpGet]
        public async Task<ActionResult> ProductFilter(ProductFilterModel filter)
        {
            var products = await _mediator.Send(new GetProductQuery());
            if (filter.CategoryId != null)
            {
                products = products.Where(p => p.CategoryId == filter.CategoryId);
            }
            if (!filter.Sort.IsEmpty())
            {
                if(filter.Sort == "asc")
                {
                    products = products.OrderBy(p => p.Price);
                }
                else
                {
                    products = products.OrderByDescending(p => p.Price);
                }
            }
            List<Category> categories = (await _mediator.Send(new GetCategoriesQuery())).ToList();
            ViewBag.Categories = categories;
            return View("Index", products);
        }
        [HttpGet]
        public async Task<ActionResult> FlashSaleProducts()
        {
            var flsp = await _mediator.Send(new GetFlashSaleProductQuery());
            return PartialView("_FlashSaleProducts", flsp);
        }
        [HttpGet]
        public async Task<ActionResult> GetPopularCategoryItems(int CategoryId)
        {
                var products = await _mediator.Send(new GetPopularCategoryItemsQuery(CategoryId));
                return PartialView("_PopularQueryItems", products);
        }
    }
}