using System.Net;
using System.Web.Mvc;
using StoreApp.BLL;
using StoreApp.DAL;

namespace StoreApp.Areas.Admin.Controllers
{
    public class StoresController : Controller
    {

        private static readonly StoreBll StoreBll = new();

        // GET: Admin/Stores
        public ActionResult Index()
        {
            return View(StoreBll.GetAll());
        }
        // GET: Admin/Stores/GetAll
        [HttpGet]
        public PartialViewResult GetAll()
        {
            return PartialView("PartialViews/_GetAll", StoreBll.GetAll());
        }

        // GET: Admin/Stores/Create
        public PartialViewResult Create()
        {
            return PartialView("PartialViews/_Create");
        }

        // POST: Admin/Stores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        // [ValidateAntiForgeryToken]
        public ActionResult Create(Store store)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index");
            var result = StoreBll.Create(store);
            return RedirectToAction("Index");
        }

        // GET: Admin/Stores/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var store = StoreBll.GetOne(id);
            return PartialView("PartialViews/_Edit", store);
        }

        // POST: Admin/Stores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,IsMain,IsInvoiceDirect,Address")] Store store)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index");
            StoreBll.Update(store);
            return RedirectToAction("Index");
        }

        // GET: Admin/Stores/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var store = StoreBll.GetOne(id);
            return PartialView( "PartialViews/_Delete",store);
        }

        // POST: Admin/Stores/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            StoreBll.Delete(id);
            return RedirectToAction("Index");
        }
    }
}