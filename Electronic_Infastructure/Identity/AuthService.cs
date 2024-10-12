using Electronic_Application.Contracts.Infastructure.Auth;
using Electronic_Application.Exceptions;
using Electronic_Application.Features.Auth.Commands.Login;
using Electronic_Application.Features.Auth.Commands.RefreshToken;
using Electronic_Application.Helpers;
using Electronic_Application.Models.Auth;
using Electronic_Domain.Common.Enums;
using Electronic_Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ValidationFailure = FluentValidation.Results.ValidationFailure;

namespace Electronic_Infastructure.Identity
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;
        private readonly IApplicationDBContext _applicationDBContext;

        public AuthService(UserManager<ApplicationUser> userManager, IConfiguration config, IApplicationDBContext applicationDBContext)
        {
            _userManager = userManager;
            _config = config;
            _applicationDBContext = applicationDBContext;
        }

        private async Task<string> GenerateTokenString(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.BusinessUserId.ToString()),
               // new Claim(ClaimTypes.Name, user.BusinessUser.Name),
                //new Claim(ClaimTypes.NameIdentifier, user.BusinessUserId.ToString()),
               // new Claim(ClaimTypes.Name, user.BusinessUser.GetFullName()),              
               // new Claim("userstatus",await SetUserStatus(user)),
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }         

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Secret").Value));

            var signingCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(3),
                issuer: _config.GetSection("Jwt:ValidIssuer").Value,
                audience: _config.GetSection("Jwt:ValidAudience").Value,
                signingCredentials: signingCred);

            string tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return tokenString;
        }

        public async Task<LoginDto> Login(LoginUser user)
        {
            var identityUser = await _userManager.FindByEmailAsync(user.UserName);
            if (identityUser != null)
            {
                var result = await _userManager.CheckPasswordAsync(identityUser, user.Password);
                if (result)
                {
                    var accessToken = await GenerateTokenString(identityUser);
                    var refreshToken = CreateRefreshToken();
                    await InsertRefreshToken(identityUser.Id, refreshToken);
                    return new LoginDto
                    {
                        AccessToken = accessToken,
                        RefreshToken = refreshToken
                    };                  

                }
                else
                    throw new UnAuthorizedException("The password is incorrect");
            }
            throw new UnAuthorizedException("The Email is incorrect,Please contact administrator.");
        }

        public async Task<bool> RegisterUser(RegisterUserModel registerUserModel)
        {
            var useremail = await _userManager.FindByEmailAsync(registerUserModel.Email);

            if (useremail != null)
            {
                var error = new List<ValidationFailure>() { new ValidationFailure("Email", "Email address is already taken.") };
                throw new ValidationException(error);
            }

            var businessUser = new BusinessUser()
            {
                Id = Guid.NewGuid(),
                Name = registerUserModel.Email,
                Status= BusinessUserStatus.Pending               
            };

            var identityUser = new ApplicationUser
            {
                UserName = registerUserModel.Email,
                Email = registerUserModel.Email,
                BusinessUser = businessUser
            };

            var result = await _userManager.CreateAsync(identityUser, registerUserModel.Password);

            if (registerUserModel.IsAdmin)
            {
                await _userManager.AddToRoleAsync(identityUser, RoleHelper.Admin);
            }
            else {
                await _userManager.AddToRoleAsync(identityUser, RoleHelper.Customer);
            }
           
            if (result.Succeeded)
            {
                return true;
            }
            else
            {
                var errorList = result.Errors.Select(a => new ValidationFailure(a.Code, a.Description)).ToList();
                throw new ValidationException(errorList);
            }
        }

        private string CreateRefreshToken()
        {
            var tokenBytes = RandomNumberGenerator.GetBytes(64);
            var refreshtoken = Convert.ToBase64String(tokenBytes);

            var tokenIsInUser = _applicationDBContext.RefreshToken.Any(_ => _.Token == refreshtoken);

            if (tokenIsInUser)
            {
                return CreateRefreshToken();
            }
            return refreshtoken;
        }

        private async Task InsertRefreshToken(string userId, string refreshtoken)
        {
            CancellationToken cancellationToken = CancellationToken.None;
            var newRefreshToken = new RefreshToken
            {
                UserId = userId,
                Token = refreshtoken,
                ExpirationDate = DateTime.Now.AddDays(7)
            };
            _applicationDBContext.RefreshToken.Add(newRefreshToken);
            await _applicationDBContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<LoginDto> RenewTokens(RefreshTokenDto refreshToken)
        {
            string newJwtToken = string.Empty;
            string newRefreshToken = string.Empty;

            var userRefreshToken = await _applicationDBContext.RefreshToken.Where(_ => _.Token == refreshToken.RefreshToken && _.ExpirationDate >= DateTime.Now).FirstOrDefaultAsync();

            if (userRefreshToken == null)
            {
                return null;
            }
           
            var user = await _userManager.FindByIdAsync(userRefreshToken.UserId);

            if (user != null) {
                 newJwtToken = await GenerateTokenString(user);
                 newRefreshToken = CreateRefreshToken();

                userRefreshToken.Token = newRefreshToken;
                userRefreshToken.ExpirationDate = DateTime.Now.AddDays(7);
            }
          
            await _applicationDBContext.SaveChangesAsync(CancellationToken.None);

            return new LoginDto
            {
                AccessToken = newJwtToken,
                RefreshToken = newRefreshToken
            };
        }
    }
}
