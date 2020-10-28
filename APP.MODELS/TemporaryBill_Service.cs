using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace APP.MODELS
{
    [Table("TemporaryBill_Service")]
    public class TemporaryBill_Service
    {
        [Column("Id")]
        [Key]
        public long Id { get; set; }
        [Column("TemporaryBillId")]
        public long? TemporaryBillId { get; set; }
        [Column("ServiceId")]
        public long ServiceId { get; set; }
        [Column("ServicePrice")]
        public decimal? ServicePrice { get; set; }
        [NotMapped]
        public string ServiceName { get; set; }
    }
}
