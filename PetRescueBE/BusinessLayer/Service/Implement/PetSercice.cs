using AutoMapper;
using BusinessLayer.Model.Request;
using BusinessLayer.Model.Response;
using BusinessLayer.Service.Interface;
using DataAccessLayer.Entity;
using DataAccessLayer.UnitOfWork.Interface;
using Microsoft.Extensions.Logging;
using static System.Net.Mime.MediaTypeNames;

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
                    Message = "Request is null",
                    Data = null
                };
            }

            // Kiểm tra sự tồn tại của Shelter
             var checkShelter = await _unitOfWork.Repository<Shelter>().FindAsync(requestModel.ShelterId);
            if (checkShelter == null)
            {
                return new BaseResponseModel<PetResponseModel>
                {
                    Code = 404,
                    Message = "Shelter not found"                   
                };
            }

            // Tạo Pet mới từ requestModel
            var newPet = _mapper.Map<Pet>(requestModel);
            newPet.ArrivalDate = DateTime.Now;
            newPet.Status = "ACTIVE";
            newPet.PetId = Guid.NewGuid();

            try
            {
                await _unitOfWork.BeginTransaction();
                var PetRepos = _unitOfWork.Repository<Pet>().InsertAsync(newPet, false);
                await _unitOfWork.CommitTransaction();
            }
            catch (Exception ex)
            {
                // Rollback nếu có lỗi
                await _unitOfWork.RollbackTransaction();

                return new BaseResponseModel<PetResponseModel>
                {
                    Code = 500,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null
                };
            }

            // Map đối tượng Pet mới tạo sang PetResponseModel
            var response = _mapper.Map<PetResponseModel>(newPet);

            return new BaseResponseModel<PetResponseModel>
            {
                Code = 201,
                Message = "Pet created successfully",
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

            foreach (var item in response)
            {
                var originalPet = listPet.FirstOrDefault(p => p.PetId == item.PetId);
                if (originalPet.Image != null)
                {
                    item.ImageData = Convert.ToBase64String(originalPet.Image);
                }
            }


            return new BaseResponseModel<ICollection<PetResponseModel>>
            {
                Code = 200,
                Message = "get all success",
                Data = response.ToList()
            };
        }

        public async Task<BaseResponseModel<PetResponseModel>> GetDetailAsync(Guid id)
        {
            var pet = await _unitOfWork.Repository<Pet>().FindAsync(id);
            if (pet == null)
            {
                return new BaseResponseModel<PetResponseModel>
                {

                    Code = 200,
                    Message = "pet not found"

                };
            }
            var response = _mapper.Map<PetResponseModel>(pet);

            // Convert image to base64 string if it exists
            if (pet.Image != null)
            {
                response.ImageData = Convert.ToBase64String(pet.Image);
            }

            return new BaseResponseModel<PetResponseModel>
            {

                Code = 200,
                Message = "Get Pet Success",
                Data = response
            };
        }

        public async Task<BaseResponseModel<PetResponseModel>> UpdateASync(PetUpdateRequestModel requestModel)
        {
            var petRepos =  _unitOfWork.Repository<Pet>();
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
                await petRepos.UpdateAsync(pet,false);
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
