using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StoreApp.DAL
{
    [MetadataType(typeof(ProductAnnotation))]
    public partial class Product
    {

    }

    internal class ProductAnnotation
    {
        [Display(Name = "Product Name")]
        public string Name { get; set; }
        [Display(Name = "Count")]
        public long Count { get; set; }
    }
}