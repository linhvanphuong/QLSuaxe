using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace APP.MODELS
{
    [Table("Accessories")]
    public class Accessories
    {
        [Column("Id")]
        [Key]
        public long Id { get; set; }
        [Column("Name")]
        public string Name { get; set; }
        [Column("Price")]
        public decimal? Price { get; set; }
        [Column("Quantity")]
        public int? Quantity { get; set; }
        [Column("Unit")]
        public string Unit { get; set; }
    }
}
