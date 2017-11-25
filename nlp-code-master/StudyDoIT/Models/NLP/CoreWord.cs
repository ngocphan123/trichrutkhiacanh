namespace StudyDoIT.Models.NLP
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CoreWord")]
    public partial class CoreWord
    {
        [StringLength(15)]
        public string id { get; set; }

        [StringLength(50)]
        public string aspect { get; set; }

        [StringLength(50)]
        public string core_word { get; set; }
    }
}
