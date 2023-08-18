using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using StoreApp.DAL;

namespace StoreApp.BLL
{
    public class SpaceBll : IBusinessAccessLayer<Space>
    {
        private static readonly StoreBll StoreBll = new();
        private static readonly ProductBll ProductBll = new();
        public (bool done, string message) Create(Space space)
        {
            try
            {
                using var db = new DBModel();
                db.Spaces.Add(space);
                db.SaveChanges();
                return (true, MessagesHelper.CreatedSuccessfully);
            }
            catch (Exception)
            {
                return (false, MessagesHelper.Error);
            }
        }

        public List<Space> GetAll()
        {
            using var db = new DBModel();
            return db.Spaces.ToList();
        }
        /// <summary>
        /// Get all spaces inside a specific store
        /// </summary>
        /// <param name="storeId">Id of the store</param>
        /// <returns>List of spaces inside id including Store, or null</returns>
        public List<Space> GetAll(long storeId)
        {
            using var db = new DBModel();
            return db.Spaces.Where(space => space.StoreFK == storeId).Include(space => space.Store).ToList();
        }
        public Space GetOne(long? id)
        {
            using var db = new DBModel();
            return db.Spaces.Include(s => s.Store).FirstOrDefault(s => s.Id == id);
        }

        public (bool done, string message) Update(Space store)
        {
            try
            {
                using var db = new DBModel();
                var itemInDb = db.Spaces.FirstOrDefault(s => s.Id == store.Id);
                if (itemInDb == null)
                    return (false, MessagesHelper.ItemNotFound);

                itemInDb.Name = store.Name;
                itemInDb.StoreFK = store.StoreFK;

                db.SaveChanges();

                return (true, MessagesHelper.UpdatedSuccessfully);
            }
            catch (Exception)
            {
                return (false, MessagesHelper.Error);
            }
        }
        /// <summary>
        /// Delete space even if it is last space in store
        /// </summary>
        /// <param name="id">Space Id</param>
        /// <returns>Tuple represents process status and a message</returns>
        public (bool done, string message) Delete(long id)
        {
            try
            {
                using var db = new DBModel();
                var itemInDb = db.Spaces.FirstOrDefault(s => s.Id == id);
                if (itemInDb == null) return (false, MessagesHelper.ItemNotFound);
                if (itemInDb.Products.Count > 0) // if store is not empty
                    return (false, "Space is not empty");
                db.Spaces.Remove(itemInDb);
                db.SaveChanges();
                return (true, MessagesHelper.DeletedSuccessfully);
            }
            catch (Exception)
            {
                return (false, MessagesHelper.Error);
            }
        }
        /// <summary>
        /// Delete space only if it is not last space in store
        /// </summary>
        /// <param name="id">Space Id</param>
        /// <returns>Tuple represents process status and a message</returns>
        public (bool doen, string message) DeleteIfNotLast(long id)
        {
            const int minimumNumberOfSpaces = 1;
            using var db = new DBModel();
            var itemInDb = db.Spaces.Where(s => s.Id == id).Include(s => s.Store).FirstOrDefault();
            return itemInDb?.Store.Spaces.Count > minimumNumberOfSpaces ? (false, "You need at least one space in the store") : Delete(id);
        }
        /// <summary>
        /// Delete all spaces in a store
        /// </summary>
        /// <param name="store">Store</param>
        //// <returns>Tuple represents process status and a message</returns>
        public (bool done, string message) DeleteSpacesInStore(Store store)
        {
            foreach (var space in store.Spaces)
            {
                var result = Delete(space.Id);
                if (!result.done)
                    return result;
            }
            return (true, MessagesHelper.DeletedSuccessfully);
        }

        /// <summary>
        /// Split a store into number of spaces
        /// </summary>
        /// <param name="spaceId">Current Space Id</param>
        /// <param name="numberOfSpaces">Number of new spaces</param>
        /// <returns>Tuple represents process status and a message</returns>
        public (bool done, string message) SplitSpace(long spaceId, int numberOfSpaces)
        {
            // Get the store
            var currentSpace = GetOne(spaceId);
            if (currentSpace == null) return (true, MessagesHelper.ItemNotFound);

            // Get the store
            var store = StoreBll.GetOne(currentSpace.StoreFK);
            return store == null ? (true, MessagesHelper.ItemNotFound) :
                // create new spaces
                CreateSpaces(store.Id, numberOfSpaces);
        }

        /// <summary>
        /// Create spaces in a store
        /// </summary>
        /// <param name="storeId">Id of store</param>
        /// <param name="numberOfSpaces">Number of new spaces</param>
        /// <returns></returns>
        private (bool done, string message) CreateSpaces(long storeId, int numberOfSpaces)
        {
            for (var i = 0; i < numberOfSpaces-1; i++)
            {
                var result = Create(new Space
                {
                    Name = $"New Space {i}-{storeId}",
                    StoreFK = storeId
                });

                if (!result.done)
                    return result;
            }
            return (true, MessagesHelper.CreatedSuccessfully);
        }

        /// <summary>
        /// Merge spaces and move all products to first store
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="firstSpaceId">Id of first store</param>
        /// <param name="secondSpaceId">Id of second store</param>
        /// <returns></returns>
        public (bool done, string message) MergeTwoSpaces(long storeId, long firstSpaceId, long secondSpaceId)
        {
            var store = StoreBll.GetOne(storeId);
            if(store == null) return (false,  MessagesHelper.ItemNotFound);
            // Check if the two spaces at the same store
            var spacesIds = new List<long>{ firstSpaceId, secondSpaceId };
            if (!IsSpacesAtTheSameStore(storeId, spacesIds))
                return (false, "Can not merge spaces in different stores.");
            
            // Move Products in the second store to the first store
            var productsInFirstSpace = store.Spaces.FirstOrDefault(s => s.Id == secondSpaceId)?.Products;
            var result = ProductBll.MoveProductsToAnotherSpace(productsInFirstSpace, firstSpaceId);
            
            return !result.done ? result : Delete(secondSpaceId);
        }

        /// <summary>
        /// Check if the list of spaces at he same store
        /// </summary>
        /// <param name="storeId">Store Id</param>
        /// <param name="spaceIds">IEnumerable of spaces ids</param>
        /// <returns></returns>
        public bool IsSpacesAtTheSameStore(long storeId, IEnumerable<long> spaceIds)
        {
            var store = StoreBll.GetOne(storeId);
            if (store == null) return false;

            var storeSpacesId = store.Spaces.Select(s => s.Id).ToList();

            return spaceIds.All(spaceId => storeSpacesId.Contains(spaceId));
        }

    }
}