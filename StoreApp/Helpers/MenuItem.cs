namespace StoreApp.Helpers
{
    /// <summary>
    /// It represents a menu item
    /// </summary>
    public class MenuItem
    {
        /// <summary>
        /// The font Awesome Icon to be displayed
        /// </summary>
        public string FontAwesomeIcon{ get; set; }
        public string DisplayName { get; set; }
        /// <summary>
        /// Javascript function to be called when this item clicked
        /// </summary>
        public string OnClickFunction { get; set; }
    }
}