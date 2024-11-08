﻿using BusinessLayer.Model.Request;
using BusinessLayer.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service.Interface
{
    public interface IShelterService
    {
        Task<BaseResponseModel<IEnumerable<ShelterResponseModel>>> GetAllAsync();
        Task<BaseResponseModel<ShelterResponseModel>> AddAsync(ShelterRequestModel request);
        Task<BaseResponseModel<ShelterResponseModel>> UpdateAsync(ShelterRequestModelForUpdate user, Guid id);
        Task<BaseResponseModel> DeleteAsync(Guid id);
        Task<BaseResponseModel<ShelterResponseModel>> GetDetailAsync(Guid id);
        Task<BaseResponseModel<ShelterResponseModel>> UpdateBalanceAsync(Guid id, decimal amount);
    }
}
