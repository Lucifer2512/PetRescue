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
    public class ShelterService : IShelterService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ShelterService(IConfiguration configuration, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponseModel<ShelterResponseModel>> AddAsync(ShelterRequestModel request)
        {
            var shelterRepository = _unitOfWork.Repository<Shelter>();

            var userRepository = _unitOfWork.Repository<User>();

            var newShelter = _mapper.Map<Shelter>(request);

            var user = await userRepository.GetAll().Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == request.UserEmail);

            if (user == null)
            {
                return new BaseResponseModel<ShelterResponseModel>
                {
                    Code = 400,
                    Message = "User email not exists",
                    Data = null
                };
            }

            var shelterId = Guid.NewGuid();

            newShelter.ShelterId = shelterId;
            newShelter.UsersId = user.UserId;

            try
            {
                await _unitOfWork.BeginTransaction();

                await shelterRepository.InsertAsync(newShelter);

                await _unitOfWork.CommitTransaction();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransaction();
                return new BaseResponseModel<ShelterResponseModel>
                {
                    Code = 500,
                    Message = ex.Message,
                    Data = null
                };
            }

            var createdShelter = await shelterRepository.GetAll().Include(s => s.Users).FirstOrDefaultAsync(s => s.ShelterId == shelterId);

            return new BaseResponseModel<ShelterResponseModel>
            {
                Code = 201,
                Message = "Shelter Created Success",
                Data = _mapper.Map<ShelterResponseModel>(createdShelter)
            };
        }

        public async Task<BaseResponseModel<ShelterResponseModel>> UpdateAsync(ShelterRequestModelForUpdate request, Guid id)
        {
            var shelterRepository = _unitOfWork.Repository<Shelter>();

            var existedShelter = await shelterRepository.GetAll().Include(s => s.Users).FirstOrDefaultAsync(s => s.ShelterId == id);

            if (existedShelter == null)
            {
                return new BaseResponseModel<ShelterResponseModel>
                {
                    Code = 404,
                    Message = "Shelter not exists",
                    Data = null
                };
            }

            var userRepository = _unitOfWork.Repository<User>();

            var user = await userRepository.GetAll().Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == request.UserEmail);

            if (user == null)
            {
                return new BaseResponseModel<ShelterResponseModel>
                {
                    Code = 400,
                    Message = "User email not exists",
                    Data = null
                };
            }

            _mapper.Map(request, existedShelter);

            try
            {
                await _unitOfWork.BeginTransaction();

                await shelterRepository.UpdateAsync(existedShelter);

                await _unitOfWork.CommitTransaction();

            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransaction();
                return new BaseResponseModel<ShelterResponseModel>
                {
                    Code = 500,
                    Message = ex.Message,
                    Data = null
                };
            }

            return new BaseResponseModel<ShelterResponseModel>
            {
                Code = 200,
                Message = "Shelter Updated Success",
                Data = _mapper.Map<ShelterResponseModel>(existedShelter)
            };
        }

        public async Task<BaseResponseModel<ShelterResponseModel>> GetDetailAsync(Guid id)
        {
            var shelterRepository = _unitOfWork.Repository<Shelter>();

            var existedShelter = await shelterRepository.GetAll().Include(s => s.Users).FirstOrDefaultAsync(s => s.ShelterId == id);

            if (existedShelter == null)
            {
                return new BaseResponseModel<ShelterResponseModel>
                {
                    Code = 404,
                    Message = "Shelter not exists",
                    Data = null
                };
            }

            var responseModel = _mapper.Map<ShelterResponseModel>(existedShelter);

            // Convert image to base64 string if it exists
            if (existedShelter.Image != null)
            {
                responseModel.ImageData = Convert.ToBase64String(existedShelter.Image);
            }

            return new BaseResponseModel<ShelterResponseModel>
            {
                Code = 200,
                Message = "Get Shelter Detail Success",
                Data = responseModel
            };
        }

        public async Task<BaseResponseModel<IEnumerable<ShelterResponseModel>>> GetAllAsync()
        {
            var shelterRepository = _unitOfWork.Repository<Shelter>();

            var shelters = await shelterRepository.GetAll().Include(s => s.Users).ToListAsync();
            var shelterResponseModels = _mapper.Map<IEnumerable<ShelterResponseModel>>(shelters);

            if (shelters.Count() == 0)
            {
                return new BaseResponseModel<IEnumerable<ShelterResponseModel>>
                {
                    Code = 200,
                    Message = "No Shelters in the list",
                    Data = shelterResponseModels
                };
            }

            return new BaseResponseModel<IEnumerable<ShelterResponseModel>>
            {
                Code = 200,
                Message = "Shelters retrieved successfully",
                Data = shelterResponseModels
            };
        }

        public async Task<BaseResponseModel<PaginatedList<ShelterResponseModel>>> GetAllPaginatedAsync(int index, int size)
        {
            var shelterRepository = _unitOfWork.Repository<Shelter>();

            var shelters = await shelterRepository.GetAll().Include(s => s.Users)
                .Skip((index - 1) * size)
                .Take(size)
                .ToListAsync();

            var count = await shelterRepository.GetAll().CountAsync();
            var totalPages = (int)Math.Ceiling(count / (double)size);
            var shelterResponseModels = _mapper.Map<IEnumerable<ShelterResponseModel>>(shelters);

            if (shelters.Count() == 0)
            {
                return new BaseResponseModel<PaginatedList<ShelterResponseModel>>
                {
                    Code = 200,
                    Message = "No Shelters in the list",
                    Data = new PaginatedList<ShelterResponseModel>
                    {
                        Items = shelterResponseModels.ToList(),
                        PageIndex = index,
                        TotalPages = totalPages,
                        TotalCount = count,
                        HasPreviousPage = index > 1,
                        HasNextPage = index < totalPages
                    }
                };
            }

            return new BaseResponseModel<PaginatedList<ShelterResponseModel>>
            {
                Code = 200,
                Message = "Shelters retrieved successfully",
                Data = new PaginatedList<ShelterResponseModel>
                {
                    Items = shelterResponseModels.ToList(),
                    PageIndex = index,
                    TotalPages = totalPages,
                    TotalCount = count,
                    HasPreviousPage = index > 1,
                    HasNextPage = index < totalPages
                }
            };
        }

        public async Task<BaseResponseModel<PaginatedList<ShelterResponseModel>>> GetAllByUserIdPagingAsync(Guid userId, int index, int size)
        {
            var shelterRepository = _unitOfWork.Repository<Shelter>();

            var shelters = await shelterRepository.GetAll().Where(s => s.UsersId == userId).Include(s => s.Users)
                .Skip((index - 1) * size)
                .Take(size)
                .ToListAsync();

            var count = await shelterRepository.GetAll().CountAsync();
            var totalPages = (int)Math.Ceiling(count / (double)size);
            var shelterResponseModels = _mapper.Map<IEnumerable<ShelterResponseModel>>(shelters);

            if (shelters.Count() == 0)
            {
                return new BaseResponseModel<PaginatedList<ShelterResponseModel>>
                {
                    Code = 200,
                    Message = "No Shelters in the list",
                    Data = new PaginatedList<ShelterResponseModel>
                    {
                        Items = shelterResponseModels.ToList(),
                        PageIndex = index,
                        TotalPages = totalPages,
                        TotalCount = count,
                        HasPreviousPage = index > 1,
                        HasNextPage = index < totalPages
                    }
                };
            }

            return new BaseResponseModel<PaginatedList<ShelterResponseModel>>
            {
                Code = 200,
                Message = "Shelters retrieved successfully",
                Data = new PaginatedList<ShelterResponseModel>
                {
                    Items = shelterResponseModels.ToList(),
                    PageIndex = index,
                    TotalPages = totalPages,
                    TotalCount = count,
                    HasPreviousPage = index > 1,
                    HasNextPage = index < totalPages
                }
            };
        }

        public async Task<BaseResponseModel<IEnumerable<ShelterResponseModel>>> GetAllByUserIdAsync(Guid userId)
        {
            var shelterRepository = _unitOfWork.Repository<Shelter>();

            var shelters = await shelterRepository.GetAll().Include(s => s.Users).Where(s => s.UsersId == userId).ToListAsync();
            var shelterResponseModels = _mapper.Map<IEnumerable<ShelterResponseModel>>(shelters);

            if (shelters.Count() == 0)
            {
                return new BaseResponseModel<IEnumerable<ShelterResponseModel>>
                {
                    Code = 200,
                    Message = "No Shelters in the list",
                    Data = shelterResponseModels
                };
            }

            return new BaseResponseModel<IEnumerable<ShelterResponseModel>>
            {
                Code = 200,
                Message = "Shelters retrieved successfully",
                Data = shelterResponseModels
            };
        }

        public async Task<BaseResponseModel> DeleteAsync(Guid id)
        {
            var shelterRepository = _unitOfWork.Repository<Shelter>();

            var existedShelter = await shelterRepository.FindAsync(id);

            if (existedShelter == null)
            {
                return new BaseResponseModel
                {
                    Code = 404,
                    Message = "Shelter not found",
                };
            }

            try
            {
                await _unitOfWork.BeginTransaction();

                await shelterRepository.DeleteAsync(existedShelter);

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
                Message = "Shelter is Deleted Successfully",
            };
        }

        public async Task<BaseResponseModel<ShelterResponseModel>> UpdateBalanceAsync( Guid id,decimal amount)
        {
            var shelterRepository = _unitOfWork.Repository<Shelter>();

            var existedShelter = await shelterRepository.GetAll().Include(s => s.Users).FirstOrDefaultAsync(s => s.ShelterId == id);

            if (existedShelter == null)
            {
                return new BaseResponseModel<ShelterResponseModel>
                {
                    Code = 404,
                    Message = "Shelter not exists",
                    Data = null
                };
            }

            existedShelter.Balance += amount;

            try
            {
                await _unitOfWork.BeginTransaction();

                await shelterRepository.UpdateAsync(existedShelter);

                await _unitOfWork.CommitTransaction();

            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransaction();
                return new BaseResponseModel<ShelterResponseModel>
                {
                    Code = 500,
                    Message = ex.Message,
                    Data = null
                };
            }

            return new BaseResponseModel<ShelterResponseModel>
            {
                Code = 200,
                Message = "Shelter Updated Success",
                Data = _mapper.Map<ShelterResponseModel>(existedShelter)
            };
        }
    }
}

