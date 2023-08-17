namespace StoreApp.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product
    {
        public long Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        public long Count { get; set; }

        public long SpaceFK { get; set; }

        public virtual Space Space { get; set; }
    }
}
