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
        public long? AccesaryId { get; set; }
    }
}
