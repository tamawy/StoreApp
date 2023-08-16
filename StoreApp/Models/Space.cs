using StoreApp.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StoreApp.DAL
{
    [MetadataType(typeof(SpaceAnnotation))]
    public partial class Space
    {

    }

    internal class SpaceAnnotation
    {
        [Required]
        [StringLength(250)]
        [Display(Name="Name")]
        public string Name { get; set; }

        [Display(Name = "Store Name")]
        public long StoreFK { get; set; }

        [Display(Name="Products")]
        public virtual ICollection<Product> Products { get; set; }
    }
}