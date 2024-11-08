using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Model.Request;
using BusinessLayer.Model.Response;

namespace BusinessLayer.Service.Interface
{
    public interface IPetService
    {
        Task<BaseResponseModel<PetResponseModel>> GetDetailAsync(Guid id);
        Task<BaseResponseModel<ICollection<PetResponseModel>>> GetAllAsync();
        Task<BaseResponseModel<PetResponseModel>> UpdateASync(PetUpdateRequestModel requestModel);
        Task<BaseResponseModel<PetResponseModel>> AddAsync(PetAddRequestModel requestModel);
        Task<BaseResponseModel<PetResponseModel>> DeleteAsync(Guid id);
     //   Task<BaseResponseModel<ICollection<PetResponseModel>>> GetAllByShelterIdAsync(Guid shelterId);

    }
}
