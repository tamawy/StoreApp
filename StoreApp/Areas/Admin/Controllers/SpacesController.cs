using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using StoreApp.BLL;
using StoreApp.DAL;

namespace StoreApp.Areas.Admin.Controllers
{
    public class SpacesController : Controller
    {
        private static readonly SpaceBll SpaceBll = new ();
        private DBModel db = new DBModel();
        /// <summary>
        /// Get the spaces in specific store
        /// </summary>
        /// <param name="id">Store Id</param>
        /// <returns></returns>
        // GET: Admin/Spaces
        public ActionResult Index(long? id)
        {
            return View(SpaceBll.GetOne(id));
        }

        public ActionResult GetAll(long id)
        {
            return PartialView("PartialViews/_GetAll", SpaceBll.GetAll(id));
        }
        // GET: Admin/Spaces/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Space space = db.Spaces.Find(id);
            if (space == null)
            {
                return HttpNotFound();
            }
            return View(space);
        }

        // GET: Admin/Spaces/Create
        public ActionResult Create()
        {
            ViewBag.StoreFK = new SelectList(db.Stores, "Id", "Name");
            return View();
        }

        // POST: Admin/Spaces/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,StoreFK")] Space space)
        {
            if (ModelState.IsValid)
            {
                db.Spaces.Add(space);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StoreFK = new SelectList(db.Stores, "Id", "Name", space.StoreFK);
            return View(space);
        }

        // GET: Admin/Spaces/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var store = SpaceBll.GetOne(id);
            return PartialView("PartialViews/_Edit", store);
        }

        // POST: Admin/Spaces/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,StoreFK")] Space space)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index", space.StoreFK);
            SpaceBll.Update(space);
            return RedirectToAction("Index", new {id = space.StoreFK });
        }

        // GET: Admin/Spaces/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Space space = db.Spaces.Find(id);
            if (space == null)
            {
                return HttpNotFound();
            }
            return View(space);
        }

        // POST: Admin/Spaces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Space space = db.Spaces.Find(id);
            db.Spaces.Remove(space);
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
