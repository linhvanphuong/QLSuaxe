using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace APP.MODELS
{
    [Table("tbl_user")]
    public class Users
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string UserName { get; set; }

    }
}
