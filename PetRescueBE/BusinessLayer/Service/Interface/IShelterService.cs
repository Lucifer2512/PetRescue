using BusinessLayer.Model.Request;
using BusinessLayer.Model.Response;

namespace BusinessLayer.Service.Interface
{
    public interface IShelterService
    {
        Task<BaseResponseModel<IEnumerable<ShelterResponseModel>>> GetAllAsync();
        Task<BaseResponseModel<PaginatedList<ShelterResponseModel>>> GetAllPaginatedAsync(int index, int size);
        Task<BaseResponseModel<ShelterResponseModel>> AddAsync(ShelterRequestModel request);
        Task<BaseResponseModel<ShelterResponseModel>> UpdateAsync(ShelterRequestModelForUpdate user, Guid id);
        Task<BaseResponseModel> DeleteAsync(Guid id);
        Task<BaseResponseModel<ShelterResponseModel>> GetDetailAsync(Guid id);
        Task<BaseResponseModel<ShelterResponseModel>> UpdateBalanceAsync(Guid id, decimal amount);
        Task<BaseResponseModel<PaginatedList<ShelterResponseModel>>> GetAllByUserIdPagingAsync(Guid userId, int index, int size);
        Task<BaseResponseModel<IEnumerable<ShelterResponseModel>>> GetAllByUserIdAsync(Guid userId);
    }
}
