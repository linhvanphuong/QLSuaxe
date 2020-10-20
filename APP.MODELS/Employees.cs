using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace APP.MODELS
{
    [Table("Employees")]
    public class Employees
    {
        [Column("Id")]
        [Key]
        public long Id { get; set; }
        [Column("JobPositionId")]
        public long JobPositionId { get; set; }
        [Column("Name")]
        public string Name { get; set; }
        [Column("Birth")]
        public DateTime? Birth { get; set; }
        [Column("Sex")]
        public bool Sex { get; set; }
        [Column("JoinedDate")]
        public DateTime? JoinedDate { get; set; }
        [Column("IdentityId")]
        public long? IdentityId { get; set; }
        [Column("Phone")]
        public string Phone { get; set; }

        [Column("Address")]
        public string Address { get; set; }
        [Column("Note")]
        public string Note { get; set; }
    }
}
