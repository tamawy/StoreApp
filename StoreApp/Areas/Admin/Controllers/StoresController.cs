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

        // GET: Admin/Stores/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var store = StoreBll.GetOne(id);
            if (store == null)
            {
                return HttpNotFound();
            }
            return View(store);
        }

        // GET: Admin/Stores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Stores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,IsMain,IsInvoiceDirect,Address")] Store store)
        {
            if (!ModelState.IsValid) return View(store);
            StoreBll.Create(store);
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
            return View(store);
        }

        // POST: Admin/Stores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,IsMain,IsInvoiceDirect,Address")] Store store)
        {
            if (!ModelState.IsValid) return View(store);
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
            return View(store);
        }

        // POST: Admin/Stores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            StoreBll.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
