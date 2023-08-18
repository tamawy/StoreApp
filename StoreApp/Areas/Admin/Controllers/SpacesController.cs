using System.Net;
using System.Web.Mvc;
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

        [HttpGet]
        public ActionResult Split(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return PartialView("PartialViews/_Split", SpaceBll.GetOne(id));
        }

        [HttpPost]
        public ActionResult Split([Bind(Include = "Id, StoreFK, Count")] Space space)
        {
            SpaceBll.SplitSpace(space.Id, space.Count);
            return RedirectToAction("Index", new { id = space.StoreFK });
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
            var store = SpaceBll.GetOne(id);
            return PartialView("PartialViews/_Delete", store);
        }

        // POST: Admin/Spaces/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed([Bind(Include = "Id, StoreFK, Count")] Space space)
        {
            SpaceBll.Delete(space.Id);
            return RedirectToAction("Index", new {id = space.StoreFK });
        }
    }
}
