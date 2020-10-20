using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace APP.MODELS
{
    [Table("Accounts")]
    public class Accounts
    {
        [Column("Id")]
        [Key]
        public long Id { get; set; }
        [Column("EmployeeId")]
        public long EmployeeId { get; set; }
        [Column("UserName")]
        public string UserName { get; set; }
        [Column("Password")]
        public string Password { get; set; }
        [Column("Token")]
        public string Token { get; set; }
        [Column("ExpiredToken")]
        public DateTime? ExpiredToken { get; set; }
        [Column("Status")]
        public byte? Status { get; set; }
        [Column("CreatedDate")]
        public DateTime? CreatedDate { get; set; }
        [Column("UpdatedDate")]
        public DateTime? UpdatedDate { get; set; }
        [NotMapped]
        public List<long> ListRole { get; set; } 
        [NotMapped]
        public List<Permissions> ListPermissions { get; set; }
        [NotMapped]
        public List<Menus> ListMenu { get; set; }
    }
}
