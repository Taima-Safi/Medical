using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M_Core.Dtos.UserDto
{
    public class ResetPasswordDto
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
        [Compare("NewPassword")]
        public string ConfirmPassword { get; set; }
    }
}
