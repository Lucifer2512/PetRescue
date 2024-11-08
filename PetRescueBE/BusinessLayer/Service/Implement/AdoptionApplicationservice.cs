using AutoMapper;
using BusinessLayer.Model.Request;
using BusinessLayer.Model.Response;
using BusinessLayer.Service.Interface;
using DataAccessLayer.Entity;
using DataAccessLayer.UnitOfWork.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BusinessLayer.Service.Implement
{
    public class AdoptionApplicationService : IAdoptionApplicationService
    {


        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AdoptionApplicationService(IConfiguration configuration, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponseModel<AdoptionApplicationResponseModel>> GetDetailAsync(Guid id)
        {
            var applicationRepo = _unitOfWork.Repository<AdoptionApplication>();
            var petRepo = _unitOfWork.Repository<Pet>();
            var userRepo = _unitOfWork.Repository<User>();

            var existedApplication = await applicationRepo.FindAsync(id);

            var response = _mapper.Map<AdoptionApplicationResponseModel>(existedApplication);
            response.PetName = petRepo.FindAsync(existedApplication.PetId).Result.Name;
            response.UserName = userRepo.FindAsync(existedApplication.UserId).Result.FirstName;

            if (existedApplication == null)
            {
                return new BaseResponseModel<AdoptionApplicationResponseModel>
                {
                    Code = 404,
                    Message = "Application not exists",
                    Data = null
                };
            }

            return new BaseResponseModel<AdoptionApplicationResponseModel>
            {
                Code = 200,
                Message = "Get Application Detail Success",
                Data = response
            };
        }

        public async Task<BaseResponseModel<IEnumerable<AdoptionApplicationResponseModel>>> GetAllAsync()
        {
            var repo = _unitOfWork.Repository<AdoptionApplication>();

            var applications = await repo.GetAll()
                .Include(u => u.User)
                .Include(p => p.Pet)
                .ToListAsync();
            var responseModels = _mapper.Map<IEnumerable<AdoptionApplicationResponseModel>>(applications);

            if (applications.Count() == 0)
            {
                return new BaseResponseModel<IEnumerable<AdoptionApplicationResponseModel>>
                {
                    Code = 200,
                    Message = "No Shelters in the list",
                    Data = responseModels
                };
            }

            return new BaseResponseModel<IEnumerable<AdoptionApplicationResponseModel>>
            {
                Code = 200,
                Message = "Shelters retrieved successfully",
                Data = responseModels
            };
        }

        public async Task<BaseResponseModel<AdoptionApplicationResponseModel>> AddAsync(AdoptionApplicationRequestModel request)
        {
            var applicationRepo = _unitOfWork.Repository<AdoptionApplication>();
            var petRepo = _unitOfWork.Repository<Pet>();
            var userRepo = _unitOfWork.Repository<User>();

            var newApplication = _mapper.Map<AdoptionApplication>(request);
            newApplication.ApplicationId = Guid.NewGuid();
            newApplication.Status = "PENDING";
            try
            {
                await _unitOfWork.BeginTransaction();

                await applicationRepo.InsertAsync(newApplication);

                await _unitOfWork.CommitTransaction();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransaction();
                return new BaseResponseModel<AdoptionApplicationResponseModel>
                {
                    Code = 500,
                    Message = ex.Message,
                    Data = null
                };
            }

            var response = _mapper.Map<AdoptionApplicationResponseModel>(newApplication);
            response.PetName = petRepo.FindAsync(request.PetId).Result.Name;
            response.UserName = userRepo.FindAsync(request.UserId).Result.FirstName;

            return new BaseResponseModel<AdoptionApplicationResponseModel>
            {
                Code = 201,
                Message = "Application Created Success",
                Data = response

            };
        }

        public async Task<BaseResponseModel<AdoptionApplicationResponseModel>> UpdateAsync(AdoptionApplicationRequestModelForUpdate request, Guid id)
        {
            var applicationRepo = _unitOfWork.Repository<AdoptionApplication>();
            var petRepo = _unitOfWork.Repository<Pet>();
            var userRepo = _unitOfWork.Repository<User>();

            var existedApplication = await applicationRepo.FindAsync(id);

            if (existedApplication == null)
            {
                return new BaseResponseModel<AdoptionApplicationResponseModel>
                {
                    Code = 404,
                    Message = "Application not exists",
                    Data = null
                };
            }

            _mapper.Map(request, existedApplication);

            try
            {
                await _unitOfWork.BeginTransaction();

                await applicationRepo.UpdateAsync(existedApplication);

                await _unitOfWork.CommitTransaction();

            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransaction();
                return new BaseResponseModel<AdoptionApplicationResponseModel>
                {
                    Code = 500,
                    Message = ex.Message,
                    Data = null
                };
            }

            var response = _mapper.Map<AdoptionApplicationResponseModel>(existedApplication);
            response.PetName = petRepo.FindAsync(existedApplication.PetId).Result.Name;
            response.UserName = userRepo.FindAsync(existedApplication.UserId).Result.FirstName;

            return new BaseResponseModel<AdoptionApplicationResponseModel>
            {
                Code = 200,
                Message = "Application Updated Success",
                Data = response
            };
        }

        public async Task<BaseResponseModel> DeleteAsync(Guid id)
        {
            var repo = _unitOfWork.Repository<AdoptionApplication>();

            var exitedApplication = await repo.FindAsync(id);

            if (exitedApplication == null)
            {
                return new BaseResponseModel
                {
                    Code = 404,
                    Message = "Application not found",
                };
            }

            try
            {
                await _unitOfWork.BeginTransaction();

                await repo.DeleteAsync(exitedApplication);

                await _unitOfWork.CommitTransaction();

            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransaction();
                return new BaseResponseModel
                {
                    Code = 500,
                    Message = ex.Message,
                };
            }

            return new BaseResponseModel
            {
                Code = 200,
                Message = "Application is Deleted Successfully",
            };
        }

    }
}

