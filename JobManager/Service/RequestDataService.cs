using JobManager.Core.Data.DataTransferObjects;
using JobManager.Core.Data.Mappers;
using JobManager.Repository.Interface;
using JobManager.Service.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace JobManager.Service
{
    public class RequestDataService : IRequestDataService
    {
        private IHttpContextAccessor _httpContextAccessor;
        private IUserRepository _userRepository;
        private UserDto _currentUser;

        public RequestDataService(IHttpContextAccessor contextAccessor, IUserRepository userRepository)
        {
            _httpContextAccessor = contextAccessor;
            _userRepository = userRepository;
        }

        public async Task<UserDto> GetCurrentUser()
        {
            return _currentUser ?? await LoadCurrentUser();
        }

        private async Task<UserDto> LoadCurrentUser()
        {
            UserDto currentUser = new UserDto();

            if (_httpContextAccessor.HttpContext.Request.Headers.TryGetValue("Authorization", out var tokenString))
            {
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(tokenString.ToString().Replace("Bearer ", ""));

                var dbCurrentUser = await _userRepository.SelectById(Guid.Parse(token.Payload["userId"].ToString()));
                currentUser = UserMapper.ToDto(dbCurrentUser);

                _currentUser = currentUser;
            }

            return currentUser;
        }
    }
}
