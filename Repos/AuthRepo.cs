using M_EF.Entities;
using Microsoft.AspNetCore.Identity;
using Repos.IRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repos
{
    public class AuthRepo : IAuthRepo
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationUser> _roleManager;

        public AuthRepo(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
    }
}
