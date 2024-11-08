using BusinessLayer.Model.Request;
using BusinessLayer.Model.Response;

namespace BusinessLayer.Service.Interface
{
    public interface IUserService
    {
        Task<BaseResponseModel<IEnumerable<UserResponseModel>>> GetAllUsersAsync();
        Task<BaseResponseModel<LoginResponseModel>> LoginAsync(LoginRequestModel request);
        Task<BaseResponseModel<UserResponseModel>> AddAsync(UserRequestModel request);
        Task<BaseResponseModel<UserResponseModel>> UpdateAsync(UserRequestModelForUpdate user, Guid id);
        Task<BaseResponseModel> DeleteAsync(Guid id);
        Task<BaseResponseModel<UserResponseModel>> GetDetailAsync(Guid id);
        Task<BaseResponseModel<IEnumerable<DataAccessLayer.Entity.Role>>> GetAllRoleAsync();
        Task<BaseResponseModel> AddRoleAsync(string role);
    }
}
