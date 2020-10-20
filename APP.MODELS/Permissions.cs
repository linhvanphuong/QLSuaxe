using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace APP.MODELS
{
    [Table("Permissions")]
    public class Permissions
    {
        [Column("Id")]
        [Key]
        public long Id { get; set; }
        [Column("RoleId")]
        public long RoleId { get; set; }
        [Column("MenuId")]
        public long MenuId { get; set; }
        [Column("ActionCode")]
        public string ActionCode { get; set; }
        [NotMapped]
        public string MenuUrl { get; set; }
    }
}
