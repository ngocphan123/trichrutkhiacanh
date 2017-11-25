namespace StudyDoIT.Models.NLP
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Vocabulary_1
    {
        [StringLength(15)]
        public string Id { get; set; }

        [StringLength(50)]
        public string Word { get; set; }

        [StringLength(4)]
        public string TypeWord { get; set; }

        public int? Counts { get; set; }

        [StringLength(15)]
        public string GroupCommentId { get; set; }

        public int? Type { get; set; }
    }
}
