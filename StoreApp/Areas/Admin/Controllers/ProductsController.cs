using System.Web.Mvc;
using StoreApp.BLL;

namespace StoreApp.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        private static readonly ProductBll ProductBll = new ProductBll();
        /// <summary>
        /// Get PartialView with products in the same space
        /// </summary>
        /// <param name="id">Space Id</param>
        /// <returns></returns>
        public ActionResult GetProducts(long id)
        {
            return PartialView("PartialViews/_GetAll", ProductBll.GetAll(id));
        }
    }
}