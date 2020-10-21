using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace APP.MODELS
{
    public class Customers
    {
        [Column("Id")]
        [Key]
        public long Id { get; set; }
        [Column("Name")]
        public string Name { get; set; }
        [Column("Birth")]
        public DateTime? Birth { get; set; }
        [Column("Sex")]
        public bool Sex { get; set; }
        [Column("Phone")]
        public string Phone { get; set; }
        [Column("Address")]
        public string Address { get; set; }
        [Column("Status")]
        public byte? Status { get; set; }
    }
}
