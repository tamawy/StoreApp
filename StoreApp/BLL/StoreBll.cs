using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.UI.WebControls;
using StoreApp.DAL;

namespace StoreApp.BLL
{
    public class StoreBll : IBusinessAccessLayer<Store>
    {
        private static readonly SpaceBll SpaceBll = new();

        public (bool done, string message) Create(Store store)
        {
            try
            {
                using var db = new DBModel();
                db.Stores.Add(store);
                db.SaveChanges();
                var result = CreateDefaultSpace(store.Id);
                return !result.done ? result : (true, MessagesHelper.CreatedSuccessfully);
            }
            catch (Exception)
            {
                return (false, MessagesHelper.Error);
            }

        }

        public List<Store> GetAll()
        {
            using var db = new DBModel();
            return db.Stores.ToList();
        }

        public Store GetOne(long? id)
        {
            using var db = new DBModel();
            return db.Stores.Include(s => s.Spaces).FirstOrDefault(s => s.Id == id);
        }

        public (bool done, string message) Update(Store store)
        {
            try
            {
                using var db = new DBModel();
                var itemInDb = db.Stores.FirstOrDefault(s => s.Id == store.Id);
                if(itemInDb == null)
                    return (false, MessagesHelper.ItemNotFound);
                itemInDb.Name = store.Name;
                itemInDb.Address = store.Address;
                itemInDb.IsInvoiceDirect = store.IsInvoiceDirect;
                itemInDb.IsMain = store.IsMain;
               
                db.SaveChanges();
                
                return (true, MessagesHelper.UpdatedSuccessfully);
            }
            catch (Exception)
            {
                return (false, MessagesHelper.Error);
            }
        }
        /// <summary>
        /// Deletes the store only if it has one free space
        /// </summary>
        /// <param name="id">Store Id</param>
        /// <returns>Tuple represents process status and a message</returns>
        public (bool done, string message) Delete(long id)
        {
            try
            {
                using var db = new DBModel();
                var itemInDb = db.Stores.FirstOrDefault(s => s.Id ==id);
                
                var result = DeleteSpaces(itemInDb);
                if (!result.done)
                    return result;
                
                if(itemInDb == null) return (false, MessagesHelper.ItemNotFound);
                db.Stores.Remove(itemInDb);
                db.SaveChanges();
                return (true, MessagesHelper.DeletedSuccessfully);
            }
            catch (Exception)
            {
                return (false, MessagesHelper.Error);
            }
        }
        /// <summary>
        /// Create default space for a store
        /// </summary>
        /// <param name="storeId">Store Id</param>
        /// <returns>Tuple represents process status and a message</returns>
        private static (bool done, string message) CreateDefaultSpace(long storeId)
        {
            return SpaceBll.Create(new Space
            {
                Name = "Default Space",
                StoreFK = storeId
            });
        }

        /// <summary>
        /// Checks if you can delete store
        /// </summary>
        /// <param name="store">Store to be deleted</param>
        /// <returns></returns>
        private static bool CanDeleteStore(Store store)
        {
            return store.Spaces.All(space => space.Products.Count == 0);
        }

        /// <summary>
        /// Delete Space inside a store
        /// </summary>
        /// <param name="store">Store contains the space</param>
        /// <returns>Tuple represents process status and a message</returns>
        private static (bool done, string message) DeleteSpaces(Store store)
        {
            return !CanDeleteStore(store) ? 
                (false, "Can't delete store, it contains non empty space(s)") : SpaceBll.DeleteSpacesInStore(store);
        }
    }
}