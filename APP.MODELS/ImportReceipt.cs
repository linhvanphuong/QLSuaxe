using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace APP.MODELS
{
    [Table("ImportReceipt")]
    public class ImportReceipt
    {
        [Column("Id")]
        [Key]
        public long Id { get; set; }
        [Column("SupplierId")]
        public long SupplierId { get; set; }
        [Column("CreatedDate")]
        public DateTime CreatedDate { get; set; }
        [Column("CreatedBy")]
        public long CreatedBy { get; set; }
        [NotMapped]
        public string SupllierName { get; set; }
        [NotMapped]
        public List<ImportReceipt_Accessory> listAccessory { get; set; }
    }
}
