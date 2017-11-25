namespace StudyDoIT.Models.NLP
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TypeMenu")]
    public partial class TypeMenu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TypeMenu()
        {
            Menus = new HashSet<Menu>();
        }

        [StringLength(15)]
        public string Id { get; set; }

        [StringLength(500)]
        public string Name { get; set; }

        public int? Publich { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Menu> Menus { get; set; }
    }
}
