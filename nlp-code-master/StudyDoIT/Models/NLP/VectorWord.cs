namespace StudyDoIT.Models.NLP
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VectorWord")]
    public partial class VectorWord
    {
        [StringLength(15)]
        public string id { get; set; }

        [StringLength(50)]
        public string word { get; set; }

        [Column(TypeName = "text")]
        public string vector { get; set; }

        [StringLength(15)]
        public string idaspect { get; set; }
    }
}
