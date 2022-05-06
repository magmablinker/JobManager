using FluentValidation.Results;
using JobManager.Core.Data.DataTransferObjects;
using JobManager.Core.Data.DataTransferObjects.Request.Authentication;
using JobManager.Core.Data.DataTransferObjects.Request.Registration;
using JobManager.Core.Data.DataTransferObjects.Response;
using JobManager.Core.Data.Model;
using JobManager.Core.Enum;
using JobManager.Repository.Interface;
using JobManager.Service.Interface;
using JobManager.Validation.Registration;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JobManager.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;
        private readonly ILanguageService _languageService;
        private readonly IPasswordHasherService _passwordHasherService;

        public UserService(IUserRepository userRepository, 
            IConfiguration config, 
            ILanguageService languageService, 
            IPasswordHasherService passwordHasherService)
        {
            _userRepository = userRepository;
            _config = config;
            _languageService = languageService;
            _passwordHasherService = passwordHasherService;
        }

        public async Task<AuthenticationResponseDto> Authenticate(AuthenticationRequestDto authenticationRequestDto)
        {
            var response = new AuthenticationResponseDto();

            var dbUser = await _userRepository.SelectByEmail(authenticationRequestDto.EmailAddress);

            if(dbUser is null)
            {
                response.AddError(_languageService.Get("invalidEmailOrPassword"));
                response.StatusCode = HttpStatusCode.Unauthorized;
                return response;
            }

            if(!_passwordHasherService.Check(dbUser.Password, authenticationRequestDto.Password).Verified)
            {
                response.AddError(_languageService.Get("invalidEmailOrPassword"));
                response.StatusCode = HttpStatusCode.Unauthorized;
                return response;
            }

            response.JwtToken = GenerateJSONWebToken(dbUser);

            return response;
        }

        public async Task<AuthenticationResponseDto> Register(RegistrationRequestDto registrationRequestDto)
        {
            var response = new AuthenticationResponseDto();

            var dbUser = await _userRepository.SelectByUsername(registrationRequestDto.Username);

            if (dbUser != null)
            {
                response.Infos.Errors.Add(_languageService.Get("usernameAlreadyInUse", registrationRequestDto.Username));
                response.StatusCode = HttpStatusCode.Conflict;
                return response;
            }

            dbUser = await _userRepository.SelectByEmail(registrationRequestDto.EmailAddress);

            if (dbUser != null)
            {
                response.Infos.Errors.Add(_languageService.Get("emailAlreadyInUse", registrationRequestDto.EmailAddress));
                response.StatusCode = HttpStatusCode.Conflict;
                return response;
            }

            RegistrationRequestDtoValidator validator = new RegistrationRequestDtoValidator();
            ValidationResult result = validator.Validate(registrationRequestDto);

            if (!result.IsValid)
            {
                response.AddErrors(result.Errors.ToList().Select(error => error.ErrorMessage));
                response.StatusCode = HttpStatusCode.UnprocessableEntity;
                return response;
            }

            DbUser newUser = new DbUser
            {
                Username = registrationRequestDto.Username,
                Password = _passwordHasherService.Hash(registrationRequestDto.Password),
                EmailAddress = registrationRequestDto.EmailAddress,
                DateOfBirth = registrationRequestDto.DateOfBirth,
                UserType = UserType.User
            };

            await _userRepository.Insert(newUser);
            await _userRepository.SaveChangesAsync();

            response.JwtToken = GenerateJSONWebToken(newUser);

            return response;
        }

        private string GenerateJSONWebToken(DbUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("username", user.Username),
                new Claim("dateOfBirth", user.DateOfBirth.ToString()),
                new Claim("userType", user.UserType.ToString()),
                new Claim("userId", user.Id.ToString())
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
