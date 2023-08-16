using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

// ReSharper disable once CheckNamespace
namespace StoreApp.DAL
{
    [MetadataType(typeof(StoreAnnotation))]
    public partial class Store
    {

    }

    internal sealed class StoreAnnotation
    {
        [Display(Name = "Store Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Is Main")]
        public bool IsMain { get; set; }

        [Required]
        [Display(Name = "Is Invoice Direct")]
        public bool IsInvoiceDirect { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Spaces")]
        public ICollection<Space> Spaces { get; set; }
    }
}