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
        /// Create an store in a specific table in database
        /// </summary>
        /// <param name="item">Item to be created</param>
        /// <returns>Tuple represents process status and a message</returns>
        
        (bool done, string message) Create(T item);
        /// <summary>
        /// Get all items in a specific table in database
        /// </summary>
        /// <returns>List of all items in specific table</returns>
        List<T> GetAll();
        
        /// <summary>
        /// Get a specific store from table in database
        /// </summary>
        /// <param name="id">Id of the store</param>
        /// <returns>Item if found or null</returns>
        T GetOne(long?  id);
        
        /// <summary>
        /// Update specific store in a table in database
        /// </summary>
        /// <param name="store"></param>
        /// <returns>Tuple represents process status and a message</returns>
        (bool done, string message) Update(T store);
        
        /// <summary>
        /// Delete A specific store in a table in database
        /// </summary>
        /// <param name="id">Id of the store</param>
        /// <returns>Tuple represents process status and a message</returns>
        (bool done, string message) Delete(long id);
    }
}
