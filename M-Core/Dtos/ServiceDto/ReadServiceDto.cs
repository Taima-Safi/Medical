using M_EF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M_Core.Dtos.ServiceDto
{
    public class ReadServiceDto
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Title { get; set; }
        public string Discription { get; set; }
        public int SpecialityId { get; set; }
    }
}
