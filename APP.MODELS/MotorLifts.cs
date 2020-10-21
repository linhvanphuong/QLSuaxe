using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace APP.MODELS
{
    [Table("MotorLifts")]
    public class MotorLifts
    {
        [Column("Id")]
        [Key]
        public long Id { get; set; }
        [Column("Name")]
        public string Name { get; set; }
        [Column("Status")]
        public byte? Status { get; set; }
        [Column("Note")]
        public string Note { get; set; }
        [Column("CreatedDate")]
        public DateTime? CreatedDate { get; set; }
    }
}
