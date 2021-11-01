using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("Tb_T_Profiling")]
    public class Profiling
    {   
        [Key]
        public string NIK { get; set; }
        public int EducationId { get; set; }
        [JsonIgnore]
        public virtual Account Profil { get; set; }
        [JsonIgnore]
        public virtual Education Education { get; set; }
    }
}
