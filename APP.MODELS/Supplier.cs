using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace APP.MODELS
{
    [Table("Supplier")]
    public class Supplier
    {
        [Column("Id")]
        [Key]
        public long Id { get; set; }
        [Column("Name")]
        public string Name { get; set; }

        [Column("Address")]
        public string Address { get; set; }
        [Column("Phone")]
        public string Phone { get; set; }
        [Column("Email")]
        public string Email { get; set; }
        [Column("Website")]
        public string Website { get; set; }
    }
}
