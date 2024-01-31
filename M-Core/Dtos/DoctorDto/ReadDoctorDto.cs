using M_EF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M_Core.Dtos.DoctorDto
{
    public class ReadDoctorDto
    {
        public string Message { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int SpecialityId { get; set; }
    }
}
