using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace APP.MODELS
{
    [Table("Menus")]
    public class Menus
    {
        [Column("Id")]
        [Key]
        public long Id { get; set; }
        [Column("Name")]
        public string Name { get; set; }
        [Column("ParentId")]
        public long ParentId { get; set; }
        [Column("Url")]
        public string Url { get; set; }
        [Column("Note")]
        public string Note { get; set; }
        [Column("Order")]
        public int? Order { get; set; }
        [Column("Status")]
        public byte? Status { get; set; }
        [Column("CreatedDate")]
        public DateTime? CreatedDate { get; set; }
        [Column("UpdatedDate")]
        public DateTime? UpdatedDate { get; set; }
        [NotMapped]
        public string ParentName { get; set; }
        [NotMapped]
        public List<Menus> ListChild { get; set; }
    }
}
