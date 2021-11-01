using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModel
{
    public class SignManagerVM
    {   
        [Required]
        public string NIK { get; set; }
        [Required]
        public int RoleID{ get; set; }
    }
}
