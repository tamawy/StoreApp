namespace StoreApp.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Store
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Store()
        {
            Spaces = new HashSet<Space>();
        }

        public long Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        public bool IsMain { get; set; }

        public bool IsInvoiceDirect { get; set; }

        [Required]
        public string Address { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Space> Spaces { get; set; }
    }
}
