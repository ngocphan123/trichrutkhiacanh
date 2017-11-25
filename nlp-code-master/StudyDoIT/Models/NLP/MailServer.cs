namespace StudyDoIT.Models.NLP
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MailServer")]
    public partial class MailServer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [Required]
        public string Host { get; set; }

        [Required]
        public string SenderMail { get; set; }

        public bool EnableSSL { get; set; }

        [Required]
        public string UserName { get; set; }

        public string DisplayName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Port { get; set; }
    }
}
