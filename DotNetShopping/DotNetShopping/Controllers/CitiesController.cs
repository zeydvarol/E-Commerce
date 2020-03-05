using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DotNetShopping.Models;

namespace DotNetShopping.Controllers
{
    public class CitiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Cities
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Index()
        {
            var cities = db.Cities.Include(c => c.Country).Join(db.States, c => c.StateId, cs => cs.StateId, (c, cs) => new CityListModel {
                CityId = c.CityId,
                Name = c.Name,
                Code = c.Code,
                StateId = cs.StateId,
                StateName = cs.Name,
                CountryId = c.CountryId,
                CountryName = c.Name
            });
            return View(await cities.ToListAsync());
        }

        // GET: Cities/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = db.Cities.Find(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            return View(city);
        }

        // GET: Cities/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var states = db.States.OrderBy(x => x.Name).ToList();
            states.Insert(0, new State {Name="No State" });
            ViewBag.StateId = new SelectList(states, "StateId", "Name");
            ViewBag.CountryId = new SelectList(db.Countries.OrderBy(x=>x.Name), "CountryId", "code");
            return View();
        }

        // POST: Cities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "CityId,Name,Code,StateId,CountryId")] City city)
        {
            if (ModelState.IsValid)
            {
                db.Cities.Add(city);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "code", city.CountryId);
            return View(city);
        }

        // GET: Cities/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = db.Cities.Find(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "code", city.CountryId);
            return View(city);
        }

        // POST: Cities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "CityId,Name,Code,StateId,CountryId")] City city)
        {
            if (ModelState.IsValid)
            {
                db.Entry(city).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "code", city.CountryId);
            return View(city);
        }

        // GET: Cities/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = db.Cities.Find(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            return View(city);
        }

        // POST: Cities/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            City city = db.Cities.Find(id);
            db.Cities.Remove(city);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
