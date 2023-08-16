using System.Collections.Generic;

namespace StoreApp.BLL
{
    /// <summary>
    /// Interface that represents the main CRUD operations for a specific table in database
    /// </summary>
    /// <typeparam name="T">Database Table</typeparam>
    internal interface IBusinessAccessLayer<T> where T : class
    {
        /// <summary>
        /// Create an item in a specific table in database
        /// </summary>
        /// <param name="item">Item to be created</param>
        /// <returns>Tuple represents process status and a message</returns>
        
        (bool done, string message) Create(T item);
        /// <summary>
        /// Get all items in a specific table in database
        /// </summary>
        /// <returns>IEnumerable of all items in specific table</returns>
        IEnumerable<T> GetAll();
        
        /// <summary>
        /// Get a specific item from table in database
        /// </summary>
        /// <param name="id">Id of the item</param>
        /// <returns>Item if found or null</returns>
        T GetOne(long  id);
        
        /// <summary>
        /// Update specific item in a table in database
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Tuple represents process status and a message</returns>
        (bool done, string message) Update(T item);
        
        /// <summary>
        /// Delete A specific item in a table in database
        /// </summary>
        /// <param name="id">Id of the item</param>
        /// <returns>Tuple represents process status and a message</returns>
        (bool done, string message) Delete(long id);
    }
}
