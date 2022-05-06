using JobManager.Core.Data.DataTransferObjects;
using JobManager.Core.Data.Model;

namespace JobManager.Core.Data.Mappers
{
    public static class UserMapper
    { 
        public static UserDto ToDto(DbUser user)
        {
            return new UserDto
            {
                Id = user.Id,
                DateOfBirth = user.DateOfBirth,
                EmailAddress = user.EmailAddress,
                Username = user.Username,
                UserType = user.UserType
            };
        }

        public static DbUser ToDb(UserDto user)
        {
            return new DbUser
            {
                Id = user.Id,
                DateOfBirth = user.DateOfBirth,
                EmailAddress = user.EmailAddress,
                Username = user.Username,
                UserType = user.UserType,
            };
        }
    }
}
