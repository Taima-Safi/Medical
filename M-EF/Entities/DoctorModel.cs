
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace M_EF.Entities
{
    public class DoctorModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int SpecialityId { get; set; }
        [JsonIgnore]
        public SpecialtyModel Specialities { get; set; }
        public bool IsDeleted { get; set; }
    }
}