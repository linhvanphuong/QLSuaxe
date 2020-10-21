using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace APP.MODELS
{
    [Table("TemporaryBill")]
    public class TemporaryBill
    {
        [Column("Id")]
        [Key]
        public long Id { get; set; }
        [Column("MotorLiftId")]
        public long MotorLiftId { get; set; }
        [Column("CustomerId")]
        public long CustomerId { get; set; }
        [Column("MotorTypeId")]
        public long MotorTypeId { get; set; }
        [Column("TimeIn")]
        public DateTime TimeIn { get; set; }
        [Column("TimeOut")]
        public DateTime? TimeOut { get; set; }
        [Column("Note")]
        public string Note { get; set; }
        [Column("Status")]
        public byte Status { get; set; }
        [Column("CreatedBy")]
        public long CreatedBy { get; set; }
        [Column("UpdatedBy")]
        public long UpdatedBy { get; set; }
        [Column("PrintedBy")]
        public long PrintedBy { get; set; }
        [Column("UpdatedTime")]
        public DateTime? UpdatedTime { get; set; }
        [NotMapped]
        public List<long> ListServices { get; set; }
        [NotMapped]
        public List<long> ListAccessories { get; set; }

    }
}
