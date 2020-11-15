using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace APP.MODELS
{
    [Table("AccessoryPriceHistory")]
    public class AccessoryPriceHistory
    {
        [Key]
        public long Id { get; set; }
        public long AccessoriesId { get; set; }
        public long ImportReceiptId { get; set; }
        public decimal ImportPrice { get; set; }
        public decimal SalePrice { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
