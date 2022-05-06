using JobManager.Core.Data.DataTransferObjects;
using System.Threading.Tasks;

namespace JobManager.Service.Interface
{
    public interface IRequestDataService
    {
        Task<UserDto> GetCurrentUser();
    }
}
