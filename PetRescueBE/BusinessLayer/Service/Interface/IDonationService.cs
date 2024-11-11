using BusinessLayer.Model.Request;
using BusinessLayer.Model.Response;

namespace BusinessLayer.Service.Interface
{
    public interface IDonationService
    {
        Task<BaseResponseModel<IEnumerable<DonationReponseModel>>> GetAllAsync();
        Task<BaseResponseModel<DonationReponseModel>> AddAsync(DonationRequestModel request);
        Task<BaseResponseModel> DeleteAsync(Guid id);
        Task<BaseResponseModel<DonationReponseModel>> UpdateStatusDonate(Guid id, string status);
        Task<BaseResponseModel<DonationReponseModel>> GetDetailAsync(Guid id);
        Task<String> GenerateQRBanking(Guid EventID, Guid ShelterID, Guid UserId, int amount);
        Task<BaseResponseModel<DonationReponseModel>> GetDonationbyNotes(string notes);

    }
}
