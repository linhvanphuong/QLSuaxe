using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace APP.MODELS
{
    [Table("ImportReceipt_Accessory")]
    public class ImportReceipt_Accessory
    {
        [Column("Id")]
        [Key]
        public long Id { get; set; }
        [Column("ImportReceiptId")]
        public long ImportReceiptId { get; set; }
        [Column("AccesoryId")]
        public long AccesoryId { get; set; }
        [Column("ImportPrice")]
        public decimal ImportPrice { get; set; }
        [Column("Quantity")]
        public int Quantity { get; set; }
        [NotMapped]
        public string AccessoryName { get; set; }
        [NotMapped]
        public string Unit { get; set; }
    }
}
