using BusinessLayer.Model.Request;
using BusinessLayer.Model.Response;

namespace BusinessLayer.Service.Interface
{
    public interface IAdoptionApplicationService
    {
        Task<BaseResponseModel<AdoptionApplicationResponseModel>> GetDetailAsync(Guid id);
        Task<BaseResponseModel<IEnumerable<AdoptionApplicationResponseModel>>> GetAllAsync();
        Task<BaseResponseModel<AdoptionApplicationResponseModel>> AddAsync(AdoptionApplicationRequestModel request);
        Task<BaseResponseModel<AdoptionApplicationResponseModel>> UpdateAsync(AdoptionApplicationRequestModelForUpdate request, Guid id);
        Task<BaseResponseModel> DeleteAsync(Guid id);
    }
}
