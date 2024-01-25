using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M_EF.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedOn { get; set; }
        //public string Image { get; set; }
        public GenderType Gender { get; set; }
        public string? VerificationToken { get; set; }
        public DateTime? VerifiedAt { get; set; }
        public string? PasswordResetToken { get; set; }

        //public DateTime? ResetTokenExpires { get; set; }
       // public List<RefreshTokenModel>? RefreshTokens { set; get; }
    }

    public enum GenderType
    {
        Male,
        Female
    }
}
