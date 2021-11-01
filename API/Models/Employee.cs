using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

using System.Threading.Tasks;

namespace API.Models
{   [Table("Tb_M_Employee")]
    [Index(nameof(Employee.Phone), IsUnique= true)]
    [Index(nameof(Employee.email), IsUnique = true)]
    public class Employee
    {   
        [Key]
       // [RegularExpression("^[1-9]\\d*$", ErrorMessage = "Invalid fieldName.")]
        public string NIK { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public int salary { get; set; }

        public string email { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public Gender Gender { get; set; }
        [JsonIgnore]
        public virtual Account Emp { get; set; }
        
        
    }
    public enum Gender
    {
        Male,
        Female
    }

}
