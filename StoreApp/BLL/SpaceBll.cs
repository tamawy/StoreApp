using System;
using System.Collections.Generic;
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

        public IEnumerable<Space> GetAll()
        {
            using var db = new DBModel();
            return db.Spaces;
        }

        public Space GetOne(long id)
        {
            return GetAll().FirstOrDefault(s => s.Id == id);
        }

        public (bool done, string message) Update(Space space)
        {
            try
            {
                using var db = new DBModel();
                var itemInDb = db.Spaces.FirstOrDefault(s => s.Id == space.Id);
                if (itemInDb == null)
                    return (false, MessagesHelper.ItemNotFound);

                itemInDb.Name = space.Name;
                itemInDb.StoreFK = space.StoreFK;

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
            try
            {
                using var db = new DBModel();
                var itemInDb = db.Spaces.FirstOrDefault(s => s.Id == id);
                if (itemInDb == null) return (false, MessagesHelper.ItemNotFound);
                if (itemInDb.Products.Count > 0) // if space is not empty
                    return (false, "Space is not empty");
                db.Spaces.Remove(itemInDb);
                return (true, MessagesHelper.DeletedSuccessfully);
            }
            catch (Exception)
            {
                return (false, MessagesHelper.Error);
            }
        }

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
        /// Split a space into number of spaces
        /// </summary>
        /// <param name="spaceId">Current Space Id</param>
        /// <param name="numberOfSpaces">Number of new spaces</param>
        /// <returns>Tuple represents process status and a message</returns>
        public (bool done, string message) SplitSpace(long spaceId, int numberOfSpaces)
        {
            // Get the space
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
        /// Merge spaces and move all products to first space
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="firstSpaceId">Id of first space</param>
        /// <param name="secondSpaceId">Id of second space</param>
        /// <returns></returns>
        public (bool done, string message) MergeTwoSpaces(long storeId, long firstSpaceId, long secondSpaceId)
        {
            var store = StoreBll.GetOne(storeId);
            if(store == null) return (false,  MessagesHelper.ItemNotFound);
            // Check if the two spaces at the same store
            var spacesIds = new List<long>{ firstSpaceId, secondSpaceId };
            if (!IsSpacesAtTheSameStore(storeId, spacesIds))
                return (false, "Can not merge spaces in different stores.");
            
            // Move Products in the second space to the first space
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