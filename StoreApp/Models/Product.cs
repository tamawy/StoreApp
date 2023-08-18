using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreApp.DAL
{
    [MetadataType(typeof(ProductAnnotation))]
    public partial class Product
    {
        [NotMapped]
        public long NewSpaceId { get; set; }
        [NotMapped]
        public long OldSpaceId { get; set; }
    }

    internal class ProductAnnotation
    {
        [Display(Name = "Product Name")]
        public string Name { get; set; }
        [Display(Name = "Count")]
        public long Count { get; set; }
    }
}