using System.Web.Mvc;
using StoreApp.BLL;
using StoreApp.DAL;

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

        /// <summary>
        /// A form to choose item to move to another space
        /// </summary>
        /// <param name="id">itemId</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult MoveToAnotherSpace(long id)
        {
            var product = ProductBll.GetOne(id);
            return PartialView("PartialViews/_Move", product);
        }
        /// <summary>
        /// Move product to another space inside the same store
        /// </summary>
        /// <param name="product">Product to be moved</param>
        /// <returns>
        /// Json Represents:
        /// done: process status.
        /// oldSpaceId: The id of the old space.
        /// newSpaceId: The id of the new space.
        /// </returns>
        [HttpPost]
        public JsonResult MoveToAnotherSpace(Product product)
        {
            var oldSpaceId = product.SpaceFK;
            var newSpaceId = product.NewSpaceId;
            var result = ProductBll.MoveToAnotherSpace(product.Id, newSpaceId);
            return Json(new
            {
                done = result.done,
                message = result.message,
                oldSpaceId = oldSpaceId,
                newSpaceId = newSpaceId
            });
        }
    }
}