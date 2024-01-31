using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M_EF.Entities
{
    public class ServiceModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Discription { get; set; }
        public bool IsDeleted { get; set; }
        public int SpecialityId { get; set; }
        public SpecialtyModel Specialities { get; set; }
    }
}
