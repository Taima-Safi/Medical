using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace M_EF.Entities
{
    public class SpecialtyModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Discription { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<ServiceModel> Services { get; set; }
        public ICollection<DoctorModel> Doctors { get; set; }
    }
}