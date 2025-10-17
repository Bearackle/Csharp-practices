using MyNerdDinner.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.Entity.Spatial;
using System.Globalization;
namespace MyNerdDinner.Controllers
{
    public class DinnerController : Controller
    {
        private ApplicationDbContext _context = ApplicationDbContext.Create();
        private const int PageSize = 25;
        // GET: Dinner
        public ActionResult Index(int? page)
        {
            int pageIndex = page ?? 1;
            var dinner = _context.Dinners.Where(d => d.EventDate >= DateTime.Now).OrderBy(d => d.EventDate);
            return View(dinner.ToPagedList(pageIndex, PageSize));
        }
        public ActionResult Details(int id = 0)
        {
            Dinner dinner = _context.Dinners.Find(id);
            if(dinner == null)
            {
                return HttpNotFound();
            }
            return View(dinner);
        }
        [Authorize]
        public ActionResult Create()
        {
            var dinner = new Dinner()
            {
                EventDate = DateTime.Now.AddDays(7),
                HostedBy = User.Identity.Name,
            };
            return View(dinner);
        }
        [HttpPost, Authorize, ValidateAntiForgeryToken]
        public ActionResult Create(Dinner dinner, FormCollection formValues)
        {
            if (ModelState.IsValid)
            {
                DbGeography loc;
                try
                {
                    double lat = double.Parse(formValues["Latitude"],CultureInfo.InvariantCulture);
                    double lon = double.Parse(formValues["Longitude"], CultureInfo.InvariantCulture);
                    loc = DbGeography.PointFromText($"POINT({lon} {lat})", 4326);
                }
                catch (Exception ex)
                {
                    return HttpNotFound(ex.Message);
                }
                dinner.Location = loc;
                dinner.HostedBy = User.Identity.Name;
                RSVP rsvp = new RSVP();
                rsvp.AttendeeName = User.Identity.Name;
                dinner.RSVPs = new List<RSVP>();
                dinner.RSVPs.Add(rsvp);
                _context.Dinners.Add(dinner);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dinner);
        }
        public ActionResult Edit(int id)
        {
            Dinner dinner = _context.Dinners.Find(id);
            if (dinner == null)
            {
                return HttpNotFound();
            }
            if (!dinner.IsHostedBy(User.Identity.Name))
            {
                return View("InvalidOwner");
            }
            return View(dinner);
        }
        [HttpPost, Authorize, ValidateAntiForgeryToken]
        public ActionResult Edit(Dinner dinner)
        {
            if (!dinner.IsHostedBy(User.Identity.Name))
            {
                return View("InvalidOwner");
            }
            if (ModelState.IsValid)
            {
                _context.Entry(dinner).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dinner);
        }
        public ActionResult Delete(int id = 0)
        {
            Dinner dinner = _context.Dinners.Find(id);
            if (dinner == null) { 
                return HttpNotFound();
            }
            return View(dinner);
        }
        [HttpPost, ActionName("Delete"),Authorize, ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int id)
        {
            Dinner dinner = _context.Dinners.Find(id);
            if (!dinner.IsHostedBy(User.Identity.Name))
            {
                return View("InvalidOwner");
            }
            _context.Dinners.Remove(dinner);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool diposing)
        {
            _context.Dispose();
            base.Dispose(diposing);
        }
        public ActionResult WebSlicePopular()
        {
            ViewData["Title"] = "Popular Nerd Dinners";
            var model = from d in _context.Dinners
                        where d.EventDate >= DateTime.Now
                        orderby d.RSVPs.Count descending
                        select d;
            return View("WebSlice", model.Take(5));
        }
        public ActionResult WebSliceUpcoming()
        {
            ViewData["Title"] = "Upcomming Nerd Dinners";
            DateTime date = DateTime.Now.AddMonths(2);
            var model = from d in _context.Dinners
                        where d.EventDate < date
                        select d;
            return View("WebSlice", model.Take(5));
        }
    }
}