using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dtos
{
    public class UserForRegisterDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [StringLength(50,MinimumLength =4,ErrorMessage ="Password must be before 4 and 50")]
        public string Password { get; set; }
    }
}
