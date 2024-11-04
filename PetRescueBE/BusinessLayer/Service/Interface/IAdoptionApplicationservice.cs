using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Model.Request;
using BusinessLayer.Model.Response;
using BusinessLayer.Models.Response;
using DataAccessLayer.Entity;

namespace BusinessLayer.Service.Interface
{
    public interface IAdoptionApplicationservice
    {
        Task<BaseResponseModel<IEnumerable<AdoptionApplicationResponseModel>>> GetAllAsync();
        Task<BaseResponseModel<AdoptionApplicationResponseModel>> AddAsync(AdoptionApplicationRequestModel request);
        Task<BaseResponseModel> DeleteAsync(Guid id);
        Task<BaseResponseModel<AdoptionApplicationResponseModel>> GetDetailAsync(Guid id);
        Task<BaseResponseModel<AdoptionApplicationResponseModel>> UpdateAsunc(Guid id,AdoptionApplicationRequestModel request);

    }
}
