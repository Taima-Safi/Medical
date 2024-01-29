using M_Core.Dtos.UserDto;
using M_EF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M_Services.IServices
{
    public interface IAuthService
    {
        Task<string> Delete(LoginDto dto);
        Task<string> ForgetPassword(string userName);
        Task<ReadUsersDto> GetUsers();
        Task<AuthDto> Login(LoginDto dto);
        Task<AuthDto> RefreshToken(string refreshtoken);
        Task<AuthDto> Register(RegisterUserDto dto);
        Task<string> ResetPassword(ResetPasswordDto dto);
        Task<string> ResetPasswordToken(string resetToken);
        Task<string> Verify(string token);
    }
}
