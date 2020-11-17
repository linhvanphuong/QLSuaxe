using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace APP.MODELS
{
    [Table("ServicePriceHistory")]
    public class ServicePriceHistory
    {
        [Key]
        public long Id { get; set; }
        public long ServiceId { get; set; }
        public decimal Price { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
