using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M_EF.Entities
{
    public class BookModel
    {
        public int Id { get; set; }
        [ForeignKey("DoctorsId")]
        public int DoctorId { get; set; }
        [ForeignKey("UsersId")]
        public string? UserId { get; set; }
        public DateTime Date { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsAvialable { get; set; } = true;
        public DateTime? BookedDate => IsAvialable == false ? DateTime.Now : null;

        //public DoctorModel Doctors { get; set; }
        //public ApplicationUser Users { get; set; }
    }
}
