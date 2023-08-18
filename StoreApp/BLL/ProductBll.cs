using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public List<Product> GetAll()
        {
            using var db = new DBModel();
            return db.Products.ToList();
        }

        /// <summary>
        /// Get all spaces inside a specific space
        /// </summary>
        /// <param name="spaceId">Space Id</param>
        /// <returns>List of products inside id including Store, or null</returns>
        public List<Product> GetAll(long spaceId)
        {
            return GetAll().Where(p => p.SpaceFK == spaceId).ToList();
        }

        public Product GetOne(long? id)
        {
            using var db = new DBModel();
            return db.Products.FirstOrDefault(p => p.Id == id);
        }

        public (bool done, string message) Update(Product store)
        {
            try
            {
                using var db = new DBModel();
                var itemInDb = db.Products.FirstOrDefault(s => s.Id == store.Id);
                if (itemInDb == null)
                    return (false, MessagesHelper.ItemNotFound);

                itemInDb.Name = store.Name;
                itemInDb.SpaceFK = store.SpaceFK;
                itemInDb.Count = store.Count;

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
        /// Move a store to another store inside the same store
        /// </summary>
        /// <param name="productId">Id of store to be moved</param>
        /// <param name="spaceId">Id of the new store</param>
        /// <returns>Tuple represents process status and a message</returns>
        /// First: It checks if the new store is inside the first store
        /// Second Moves the store.
        public (bool done, string message) MoveToAnotherSpace(long productId, long spaceId)
        {
            var db = new DBModel();
            var product = db.Products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
                return (false, MessagesHelper.ItemNotFound);

            // check if the new store inside the same store
            if (!product.Space.Store.Spaces.Select(s => s.Id).Contains(spaceId))
                return (false, "Can not move item to a store in another store.");

            product.SpaceFK = spaceId;
            return Update(product);
        }

        /// <summary>
        /// Move list of products to another store
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