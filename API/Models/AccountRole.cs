using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("Tb_T_AccountRole")]
    public class AccountRole
    {
        [Key]
        public int AccountRoleId { get; set; }
        public string NIK { get; set; }
        public int RoleId { get; set; }

        [JsonIgnore]
        public virtual Role Role { get; set; }
        [JsonIgnore]
        public virtual Account Account { get; set; }
    }
}
