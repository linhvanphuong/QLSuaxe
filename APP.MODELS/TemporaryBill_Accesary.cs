using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace APP.MODELS
{
    [Table("TemporaryBill_Accesary")]
    public class TemporaryBill_Accesary
    {
        [Column("Id")]
        [Key]
        public long Id { get; set; }
        [Column("TemporaryBillId")]
        public long? TemporaryBillId { get; set; }
        [Column("AccesaryId")]
        public long AccesaryId { get; set; }
        [Column("Quantity")]
        public int Quantity { get; set; }
        [Column("AccesaryPrice")]
        public decimal AccesaryPrice { get; set; }
        [NotMapped]
        public string AccesaryName { get; set; }
        [NotMapped]
        public int MaxQuantity { get; set; }
        [NotMapped]
        public string Unit { get; set; }
        [NotMapped]
        public decimal ThanhTien { get; set; }
    }
}
