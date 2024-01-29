using M_Core.Dtos.UserDto;
using Microsoft.AspNetCore.Hosting;
using M_Core.Resources;
using M_Core.Statics;
using M_EF.Entities;
using M_Services.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Repos.IRepos;
using SixLabors.ImageSharp.Processing.Processors.Transforms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using M_Core.UserStatics;
using Microsoft.VisualBasic.FileIO;
using Microsoft.Extensions.Options;
using Microsoft.Graph.Models;

namespace M_Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepo _authRepo;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IHostingEnvironment _host;
        private readonly JwtConfig _jwtConfig;
        private readonly string _imagePath;

        public AuthService(IAuthRepo authRepo, IStringLocalizer<SharedResources> localizer, IHostingEnvironment host, IOptions<JwtConfig> jwtConfig)
        {
            _authRepo = authRepo;
            _localizer = localizer;
            _host = host;
            _imagePath = $"{_host.WebRootPath}{FilesSettings.ImagesPath}";
            _jwtConfig = jwtConfig.Value;
        }

        public async Task<ReadUsersDto> GetUsers()
        {
            var dto = new ReadUsersDto();
            var users = await _authRepo.GetAllUsers();
            if (users is null)
            {
                dto.Message = "Invalid User";
                return dto;
            }

            dto.Message = _localizer[LocalizerStatics.User];
            dto.Users = users.Select(u => new UserDto
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                UserName = u.UserName,
                Email = u.Email,
                Gender = (int)u.Gender,

            }).ToList();
            return dto;
        }

        public async Task<AuthDto> Register(RegisterUserDto dto)
        {
            var authDto = new AuthDto();
            if (dto is null)
            {
                authDto.Message = $"{_localizer[LocalizerStatics.Invalid]} {_localizer[LocalizerStatics.User]}";
                return authDto;
            }
            var user = new ApplicationUser();

            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.UserName = dto.UserName;
            user.Email = dto.Email;
            user.CreatedOn = DateTime.UtcNow;
            user.Gender = (GenderType)dto.Gender;
            user.VerificationToken = CreateRandomToken();


            string fileName = string.Empty;
            if (dto.ClientFile is null)
            {
                if ((GenderType)dto.Gender == GenderType.Male)
                {
                    var mImage = FilesSettings.DefaultUserImagePath;
                    user.Image = Path.GetFileName(mImage);
                }
                else
                {
                    var fImage = FilesSettings.DefaultFemaleImagePath;
                    user.Image = Path.GetFileName(fImage);
                }
            }
            else
            {
                var uploadFile = FilesSettings.UserImageAllowUplaod(dto.ClientFile);
                if (uploadFile.IsValid)
                {
                    string myUpload = Path.Combine(_imagePath, "UserImages");
                    fileName = dto.ClientFile.FileName;
                    string fullPath = Path.Combine(myUpload, fileName);

                    dto.ClientFile.CopyTo(new FileStream(fullPath, FileMode.Create));
                    user.Image = fileName;
                }
                else
                {
                    authDto.Message = uploadFile.Message;
                    return authDto;
                }
            }

            var result = await _authRepo.Register(user, dto.Password);
            if (result != _localizer[LocalizerStatics.RegisteredSucceded])
            {
                authDto.Message = result;
                return authDto;
            }
            var refreshToken = GenerateRefreshToken();

            user.RefreshTokens?.Add(refreshToken);
            await _authRepo.UpdateUser(user);

            authDto.Message = result;
            authDto.IsAuthenticated = true;
            authDto.Id = user.Id;
            authDto.FirstName = user.FirstName;
            authDto.LastName = user.LastName;
            authDto.Email = user.Email;
            authDto.Gender = user.Gender;
            authDto.Token = await _authRepo.GenerateTokenString(user, _jwtConfig);
            authDto.RefreshToken = refreshToken.Token;
            authDto.RefreshTokenExpiration = refreshToken.ExpiresOn;
            return authDto;
        }

        public async Task<AuthDto> Login(LoginDto dto)
        {
            var authDto = new AuthDto();
            var user = await _authRepo.FindUserAsync(u => u.Email == dto.Email);
            if (user is null)
            {
                authDto.Message = $"{_localizer[LocalizerStatics.Invalid]}{_localizer[LocalizerStatics.User]}";
                return authDto;
            }

            if (!await _authRepo.CheckPassword(user, dto.Password))
            {
                authDto.Message = _localizer[LocalizerStatics.IncorrectPassword];
                return authDto;
            }

            if (user.VerifiedAt == null)
            {
                authDto.Message = _localizer[LocalizerStatics.NotVerified];
                return authDto;
            }

            if (user.RefreshTokens.Any(r => r.IsActive))
            {
                var activeRefreshToken = user.RefreshTokens.FirstOrDefault(r => r.IsActive);
                authDto.RefreshToken = activeRefreshToken.Token;
                authDto.RefreshTokenExpiration = activeRefreshToken.ExpiresOn;
            }
            else
            {
                var refreshToken = GenerateRefreshToken();
                authDto.RefreshToken = refreshToken.Token;
                authDto.RefreshTokenExpiration = refreshToken.ExpiresOn;
                user.RefreshTokens.Add(refreshToken);
                await _authRepo.UpdateUser(user);
            }

            authDto.Id = user.Id;
            authDto.Email = user.Email;
            authDto.IsAuthenticated = true;
            authDto.Token = await _authRepo.GenerateTokenString(user, _jwtConfig);
            authDto.Gender = user.Gender;
            authDto.FirstName = user.FirstName;
            authDto.LastName = user.LastName;

            return authDto;
        }


        public async Task<string> ForgetPassword(string userName)
        {
            var user = await _authRepo.FindUserAsync(u => u.UserName == userName);
            if (user is null)
                return $"{_localizer[LocalizerStatics.Invalid]} {_localizer[LocalizerStatics.User]}";
            if (user.VerifiedAt is null)
                return _localizer[LocalizerStatics.NotVerified];
            var resetPassToken = await _authRepo.GenerateResetPassTokenString(user);
            user.PasswordResetToken = resetPassToken;
            user.ResetTokenExpires = DateTime.Now.AddMinutes(2);
            await _authRepo.UpdateUser(user);
            return resetPassToken;
        }

        public async Task<string> ResetPasswordToken(string resetToken)
        {
            var user = await _authRepo.FindUserAsync(u => u.PasswordResetToken == resetToken);
            if (user is null)
                return $"{_localizer[LocalizerStatics.Invalid]} {_localizer[LocalizerStatics.User]}";

            user.IsResetPasswordToken = true;
            await _authRepo.UpdateUser(user);

            return "You Can to Reset Password";
        }


        public async Task<string> ResetPassword(ResetPasswordDto dto)
        {
            var user = await _authRepo.FindUserAsync(u => u.Email == dto.Email);
            if (user is null)
                return $"{_localizer[LocalizerStatics.Invalid]} {_localizer[LocalizerStatics.User]}";
            if (user.IsResetPasswordToken == null || user.IsResetPasswordToken == false)
                return "You Have to pass the code first : ";
            if (user.ResetTokenExpires < DateTime.Now)
                return "ResetTokenExpires";
            await _authRepo.ResetPassword(user, dto.NewPassword);

            user.IsResetPasswordToken = false;
            await _authRepo.UpdateUser(user);

            return "Successfully";
        }


        public async Task<string> Verify(string token)
        {
            var user = await _authRepo.FindUserAsync(u => u.VerificationToken == token);
            if(user is null)
                return $"{_localizer[LocalizerStatics.Invalid]} {_localizer[LocalizerStatics.User]}";
            user.VerifiedAt = DateTime.UtcNow;

            await _authRepo.UpdateUser(user);
            return _localizer[LocalizerStatics.Verified];
        }
        public async Task<string> Delete(LoginDto dto)
        {
            var user = await _authRepo.FindUserAsync(u => u.Email == dto.Email);
            if(user is null)
                return $"{_localizer[LocalizerStatics.Invalid]} {_localizer[LocalizerStatics.User]}";
            if (!await _authRepo.CheckPassword(user, dto.Password))
                return _localizer[LocalizerStatics.IncorrectPassword];

            user.IsDeleted = true;
            await _authRepo.UpdateUser(user);

            return _localizer[LocalizerStatics.Updated];
        }

        public async Task<AuthDto> RefreshToken(string token)
        {
            var authDto = new AuthDto();
            var user = await _authRepo.FindUserAsync(u => u.RefreshTokens.Any(r => r.Token == token));
            if (user is null)
            {
                authDto.IsAuthenticated = false;
                authDto.Message = $"{_localizer[LocalizerStatics.Invalid]} {_localizer[LocalizerStatics.User]}";
                return authDto;
            }
            var refreshToken = user.RefreshTokens.Single(t => t.Token == token);
            if(!refreshToken.IsActive)
            {
                authDto.IsAuthenticated = false;
                authDto.Message = $"Non Active";
                return authDto;
            }
            refreshToken.RevokedOn = DateTime.Now;
            var newRefreshToken = GenerateRefreshToken();

            user.RefreshTokens.Add(newRefreshToken);
            await _authRepo.UpdateUser(user);

            authDto.Id = user.Id;
            authDto.FirstName = user.FirstName;
            authDto.Email = user.Email;
            authDto.Token = await _authRepo.GenerateTokenString(user, _jwtConfig);
            authDto.RefreshToken = newRefreshToken.Token;
            authDto.RefreshTokenExpiration = newRefreshToken.ExpiresOn;
            authDto.IsAuthenticated = true;

            return authDto;
        }

        private RefreshTokenModel GenerateRefreshToken()
        {
            var randonNum = new byte[32];
            using var generator = new RNGCryptoServiceProvider();
            generator.GetBytes(randonNum);
            return new RefreshTokenModel
            {
                Token = Convert.ToBase64String(randonNum),
                ExpiresOn = DateTime.UtcNow.AddDays(10),
                CreatedOn = DateTime.UtcNow
            };
        }

        private string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }
    }
}
