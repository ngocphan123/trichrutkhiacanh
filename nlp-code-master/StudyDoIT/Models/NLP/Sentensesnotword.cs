namespace StudyDoIT.Models.NLP
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Sentensesnotword")]
    public partial class Sentensesnotword
    {
        [StringLength(15)]
        public string Id { get; set; }

        public string ContentReview { get; set; }

        [StringLength(15)]
        public string CommentId { get; set; }

        public string Logs { get; set; }
    }
}
