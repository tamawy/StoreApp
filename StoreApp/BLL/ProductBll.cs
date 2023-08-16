using System;
using System.Collections.Generic;
using System.Linq;
using StoreApp.DAL;

namespace StoreApp.BLL
{
    public class ProductBll : IBusinessAccessLayer<Product>
    {
        private static readonly SpaceBll SpaceBll = new();
        private static readonly StoreBll StoreBll = new();
        public (bool done, string message) Create(Product item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetAll()
        {
            using var db = new DBModel();
            return db.Products;
        }

        public Product GetOne(long id)
        {
            return GetAll().FirstOrDefault(s => s.Id == id);
        }

        public (bool done, string message) Update(Product product)
        {
            try
            {
                using var db = new DBModel();
                var itemInDb = db.Products.FirstOrDefault(s => s.Id == product.Id);
                if (itemInDb == null)
                    return (false, MessagesHelper.ItemNotFound);

                itemInDb.Name = product.Name;
                itemInDb.SpaceFK = product.SpaceFK;
                itemInDb.count = product.count;

                db.SaveChanges();

                return (true, MessagesHelper.UpdatedSuccessfully);
            }
            catch (Exception)
            {
                return (false, MessagesHelper.Error);
            }
        }

        public (bool done, string message) Delete(long id)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Move a product to another space inside the same store
        /// </summary>
        /// <param name="productId">Id of product to be moved</param>
        /// <param name="spaceId">Id of the new space</param>
        /// <returns>Tuple represents process status and a message</returns>
        /// First: It checks if the new space is inside the first space
        /// Second Moves the product.
        public (bool done, string message) MoveToAnotherSpace(long productId, long spaceId)
        {
            var db = new DBModel();
            var product = db.Products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
                return (false, MessagesHelper.ItemNotFound);

            // check if the new space inside the same store
            
            // 1. Get the space that contains the product
            var space = SpaceBll.GetOne(product.SpaceFK);
            if (space == null)
                return (false, MessagesHelper.Error);
            
            // Get the store that contains the space
            var store = StoreBll.GetOne(space.StoreFK);
            if (store == null)
                return (false, MessagesHelper.Error);

            // Search for the new space
            if (!store.Spaces.Select(s => s.Id).Contains(spaceId))
                return (false, "Can not move item to a store in another space.");

            product.SpaceFK = spaceId;
            return Update(product);
        }

        /// <summary>
        /// Move list of products to another space
        /// </summary>
        /// <param name="products">Products to be moved</param>
        /// <param name="spaceId">Id of the new Space</param>
        /// <returns>Tuple represents process status and a message</returns>
        public (bool done, string message) MoveProductsToAnotherSpace(IEnumerable<Product> products, long spaceId)
        {
            return products.Select(product => MoveToAnotherSpace(product.Id, spaceId)).Any(result => !result.done) ? 
                (false, MessagesHelper.Error) : (true, "All Products Moved Successfully");
        }
    }
}