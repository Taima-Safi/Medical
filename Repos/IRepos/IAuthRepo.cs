
using M_Core.UserStatics;
using M_EF.Entities;
using System.Linq.Expressions;

namespace Repos.IRepos
{
    public interface IAuthRepo
    {
        Task<bool> CheckPassword(ApplicationUser user, string password);
        Task<ApplicationUser> FindUserAsync(Expression<Func<ApplicationUser, bool>> predicate);
        Task<string> GenerateResetPassTokenString(ApplicationUser user);
        Task<string> GenerateTokenString(ApplicationUser user, JwtConfig jwtConfig);
        Task<List<ApplicationUser>> GetAllUsers();
        Task<List<string>> GetUserRoles(ApplicationUser user);
        Task<string> Register(ApplicationUser user, string password);
        Task<string> ResetPassword(ApplicationUser user, string newPassword);
        Task UpdateUser(ApplicationUser user);
    }
}