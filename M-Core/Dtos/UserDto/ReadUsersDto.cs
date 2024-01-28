using M_EF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M_Core.Dtos.UserDto
{
    public class ReadUsersDto
    {
        public string Message { get; set; }

        public List<UserDto> Users { get; set; }
    }
}
