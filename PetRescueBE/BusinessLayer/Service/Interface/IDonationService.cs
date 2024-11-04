using BusinessLayer.Model.Request;
using BusinessLayer.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service.Interface
{
    public interface IDonationService
    {
        Task<BaseResponseModel<IEnumerable<DonationReponseModel>>> GetAllAsync();
        Task<BaseResponseModel<DonationReponseModel>> AddAsync(DonationRequestModel request);       
        Task<BaseResponseModel> DeleteAsync(Guid id);
        Task<BaseResponseModel<DonationReponseModel>> GetDetailAsync(Guid id);
    }
}
