using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.Model.Request;
using BusinessLayer.Model.Response;
using BusinessLayer.Service.Interface;
using DataAccessLayer.Entity;
using DataAccessLayer.UnitOfWork.Interface;

namespace BusinessLayer.Service.Implement
{
    public class PetSercice : IPetService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PetSercice(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponseModel<PetResponseModel>> AddAsync(PetAddRequestModel requestModel)
        {
            if (requestModel == null)
            {
                return new BaseResponseModel<PetResponseModel>
                {
                    Code = 400,
                    Message = "request is null",
                    Data = null
                };
            }
            var newpet = _mapper.Map<Pet>(requestModel);
            newpet.ArrivalDate = DateTime.Now;
            newpet.Status = "ACTIVE";
            newpet.PetId = Guid.NewGuid();
            try
            {
                await _unitOfWork.BeginTransaction();
                var PetRepos = _unitOfWork.Repository<Pet>().InsertAsync(newpet);
                await _unitOfWork.CommitTransaction();
            }
            catch (Exception ex)
            {
                return new BaseResponseModel<PetResponseModel>
                {
                    Code = 500,
                    Message = ex.Message,
                };
            }

            var response = _mapper.Map<PetResponseModel>(newpet);
            response.PetId = newpet.PetId;
            response.ShelterId = newpet.ShelterId;

            return new BaseResponseModel<PetResponseModel>
            {
                Code = 201,
                Message = "Pet Created Success",
                Data = response
            };
        }

        public async Task<BaseResponseModel<PetResponseModel>> DeleteAsync(Guid id)
        {
            var petRepos = _unitOfWork.Repository<Pet>();
            var pet = await petRepos.FindAsync(id);
            if (pet == null)
            {
                return new BaseResponseModel<PetResponseModel>
                {
                    Code = 200,
                    Message = "Pet Not found"
                };
            }
            var response = _mapper.Map<Pet>(pet);
            response.Status = "DELETE";
            await petRepos.UpdateAsync(response);
            return new BaseResponseModel<PetResponseModel>
            {
                Code = 200,
                Message = "delete pet success"
            };
        }

        public async Task<BaseResponseModel<ICollection<PetResponseModel>>> GetAllAsync()
        {
            var listPet = await _unitOfWork.Repository<Pet>().GetAllAsync();
            var response = _mapper.Map<ICollection<PetResponseModel>>(listPet);
            return new BaseResponseModel<ICollection<PetResponseModel>>
            {
                Code = 200,
                Message = "get all success",
                Data = response.ToList()
            };
        }

        public async Task<BaseResponseModel<PetResponseModel>> GetDetailAsync(Guid id)
        {
            var pet = _unitOfWork.Repository<Pet>().FindAsync(id);
            if (pet == null)
            {
                return new BaseResponseModel<PetResponseModel>
                {

                    Code = 200,
                    Message = "pet not found"

                };
            }
            var response = _mapper.Map<PetResponseModel>(pet);
            return new BaseResponseModel<PetResponseModel>
            {

                Code = 200,
                Message = "Get Pet Success",
                Data = response
            };
        }

        public async Task<BaseResponseModel<PetResponseModel>> UpdateASync(PetUpdateRequestModel requestModel)
        {
            var petRepos = _unitOfWork.Repository<Pet>();
            var pet = await petRepos.FindAsync(requestModel.PetId);

            if (pet == null)
            {
                return new BaseResponseModel<PetResponseModel>
                {
                    Code = 200,
                    Message = "Pet not found"
                };
            }
            _mapper.Map(requestModel, pet);
            try
            {
                await _unitOfWork.BeginTransaction();
                await petRepos.UpdateAsync(pet);
                await _unitOfWork.CommitTransaction();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransaction();
                return new BaseResponseModel<PetResponseModel>
                {
                    Code = 500,
                    Message = ex.Message
                };
            }
            var petResponse = _mapper.Map<PetResponseModel>(pet);
            return new BaseResponseModel<PetResponseModel>
            {
                Code = 200,
                Message = " update pet success",
                Data = petResponse
            };
        }
    }
}
