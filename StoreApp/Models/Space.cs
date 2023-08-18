using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;

namespace StoreApp.DAL
{
    [MetadataType(typeof(SpaceAnnotation))]
    public partial class Space
    {
        [Display(Name = "New Spaces Count")]
        [NotMapped]
        [IntegerValidator (MinValue = 2)]
        public int Count { get; set; }

        // Id of the item to be merged
        [NotMapped]
        public int MergedItemId { get; set; }
        
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