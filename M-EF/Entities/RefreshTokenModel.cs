using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M_EF.Entities
{
    [Owned]
    public class RefreshTokenModel
    {
        public string Token { get; set; }
        public DateTime ExpiresOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsExpires => DateTime.UtcNow >= ExpiresOn;
        public DateTime? RevokedOn { get; set; }
        public bool IsActive => RevokedOn == null && !IsExpires;
    }
}
