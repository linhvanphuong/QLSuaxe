using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace APP.MODELS
{
    [Table("Account_Roles")]
    public class Account_Roles
    {
        [Column("Id")]
        [Key]
        public long Id { get; set; }
        [Column("AccountId")]
        public long AccountId { get; set; }
        [Column("RoleId")]
        public long RoleId { get; set; }
    }
}
