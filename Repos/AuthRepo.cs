using M_Core.Resources;
using M_Core.Statics;
using M_Core.UserStatics;
using M_EF.Data;
using M_EF.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Repos.IRepos;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Linq.Expressions;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Repos
{
    public class AuthRepo : IAuthRepo
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public AuthRepo(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IStringLocalizer<SharedResources> localizer,
            AppDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _localizer = localizer;
            _context = context;
        }

        public async Task<List<ApplicationUser>> GetAllUsers()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<ApplicationUser> FindUserAsync(Expression<Func<ApplicationUser, bool>> predicate)
        {
            var user = await _context.Users.FirstOrDefaultAsync(predicate);
            return user;
        }

        public async Task<string> Register(ApplicationUser user, string password)
        {
            var existing = await _userManager.FindByEmailAsync(user.Email);
            if (existing is not null)
                return _localizer[LocalizerStatics.EmailExist];

            var createdUser = await _userManager.CreateAsync(user, password);
            if (!createdUser.Succeeded)
            {
                var errorMessage = "User Registering Faild Because : ";
                foreach (var error in createdUser.Errors)
                {
                    errorMessage += error.Description;
                }
                return errorMessage;
            }
            await _userManager.AddToRoleAsync(user, Roles.User);

            return _localizer[LocalizerStatics.RegisteredSucceded];

        }

        public async Task<bool> CheckPassword(ApplicationUser user, string password)
        {
           return await _userManager.CheckPasswordAsync(user, password);
        }


        public async Task UpdateUser(ApplicationUser user)
        {
            await _userManager.UpdateAsync(user);
        }

        public async Task<string> GenerateTokenString(ApplicationUser user, JwtConfig jwtConfig)
        {
            var claims = await GetClaims(user.UserName);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Key));
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var securityToken = new JwtSecurityToken(
                claims:claims,
                expires:DateTime.Now.AddHours(jwtConfig.DurationInMinutes),
                issuer:jwtConfig.Issuer,
                audience:jwtConfig.Audience,
                signingCredentials:signingCredentials
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return tokenString;
        }

        public async Task<List<string>> GetUserRoles(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            return roles.ToList();
        }


        public async Task<string> ResetPassword(ApplicationUser user, string newPassword)
        {
            var result = await _userManager.ResetPasswordAsync(user, user.PasswordResetToken, newPassword);
            if (result.Succeeded)
                return _localizer[LocalizerStatics.RegisteredSucceded];
            return _localizer[LocalizerStatics.Invalid];
        }
        
        public async Task<string> GenerateResetPassTokenString(ApplicationUser user)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        private async Task<List<Claim>> GetClaims(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

    }
}
