using BusinessLayer.Model.Request;
using BusinessLayer.Model.Response;

namespace BusinessLayer.Service.Interface
{
    public interface IPetService
    {
        Task<BaseResponseModel<PetResponseModel>> GetDetailAsync(Guid id);
        Task<BaseResponseModel<PaginatedList<PetResponseModel>>> GetAllForUserAsync(
            string? searchTerm, string? species, string? gender, Guid? shelterId, int index, int size);
        Task<BaseResponseModel<PaginatedList<PetResponseModel>>> GetAllForShelterAsync(
            Guid userId, string? searchTerm, string? species, string? gender, string? status, int index, int size);
        Task<BaseResponseModel<PetResponseModel>> UpdateASync(PetUpdateRequestModel requestModel);
        Task<BaseResponseModel<PetResponseModel>> AddAsync(PetAddRequestModel requestModel);
        Task<BaseResponseModel<PetResponseModel>> DeleteAsync(Guid id);
        
        Task<BaseResponseModel<List<PetResponseModel>>> GetsPet();
    }
}
