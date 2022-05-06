using JobManager.Core.Data.DataTransferObjects.Request.Authentication;
using JobManager.Core.Data.DataTransferObjects.Request.Registration;
using JobManager.Core.Data.DataTransferObjects.Response;
using System.Threading.Tasks;

namespace JobManager.Service.Interface
{
    public interface IUserService
    {
        Task<AuthenticationResponseDto> Authenticate(AuthenticationRequestDto authenticationRequestDto);
        Task<AuthenticationResponseDto> Register(RegistrationRequestDto registrationRequestDto);
    }
}
