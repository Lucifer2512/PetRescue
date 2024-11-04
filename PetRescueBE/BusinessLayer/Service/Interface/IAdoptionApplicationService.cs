using BusinessLayer.Model.Request;
using BusinessLayer.Model.Response;
using BusinessLayer.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
