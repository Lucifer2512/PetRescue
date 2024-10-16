using BusinessLayer.Models.Request;
using BusinessLayer.Models.Response;

namespace BusinessLayer.IServices
{
    public interface IUserService
    {
        Task<BaseResponseModel<IEnumerable<UserResponseModel>>> GetAllUsersAsync();
        Task<BaseResponseModel<LoginResponseModel>> LoginAsync(LoginRequestModel request);
        Task<BaseResponseModel<UserResponseModel>> AddAsync(UserRequestModel request);
        Task<BaseResponseModel<UserResponseModel>> UpdateAsync(UserRequestModelForUpdate user, Guid id);
        Task<BaseResponseModel> DeleteAsync(Guid id);
        Task<BaseResponseModel<UserResponseModel>> GetDetailAsync(Guid id);
    }
}
