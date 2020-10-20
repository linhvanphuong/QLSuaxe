using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace APP.MODELS
{
    [Table("MotorManufacture")]
    public class MotorManufacture
    {
        [Column("Id")]
        [Key]
        public long Id { get; set; }
        [Column("Name")]
        public string Name { get; set; }
        [Column("Status")]
        public byte? Status { get; set; }
    }
}
