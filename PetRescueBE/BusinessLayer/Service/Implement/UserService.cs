using AutoMapper;
using BusinessLayer.IServices;
using BusinessLayer.Models.Request;
using BusinessLayer.Models.Response;
using BusinessLayer.Utilities;
using DataAccessLayer.Entity;
using DataAccessLayer.UnitOfWork.Interface;
using Microsoft.Extensions.Configuration;

namespace BusinessLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IConfiguration configuration, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseResponseModel<LoginResponseModel>> LoginAsync(LoginRequestModel request)
        {
            var userRepository = _unitOfWork.Repository<User>();

            var user = await userRepository.GetAsync(u => u.Email == request.Email);

            if (user != null && Helper.VerifyPassword(request.Password, user.PasswordHash))
            {
                string token = Helper.GenerateJwtToken(user, _configuration);

                return new BaseResponseModel<LoginResponseModel>()
                {
                    Code = 200,
                    Message = "Login Success",
                    Data = new LoginResponseModel()
                    {
                        Token = new TokenModel()
                        {
                            Token = token
                        },

                        User = _mapper.Map<UserResponseModel>(user)
                    },
                };
            }
            return new BaseResponseModel<LoginResponseModel>()
            {
                Code = 400,
                Message = "Username or Password incorrect",
                Data = new LoginResponseModel()
                {
                    Token = null,
                    User = null
                },
            };
        }
        public async Task<BaseResponseModel<UserResponseModel>> AddAsync(UserRequestModel request)
        {
            var userRepository = _unitOfWork.Repository<User>();

            var existedUser = await userRepository.GetAsync(u => u.Email == request.Email);

            if (existedUser != null)
            {
                return new BaseResponseModel<UserResponseModel>
                {
                    Code = 400,
                    Message = "User already exists",
                    Data = null
                };
            }

            var newUser = _mapper.Map<User>(request);

            newUser.UserId = Guid.NewGuid();

            try
            {
                await _unitOfWork.BeginTransaction();

                await userRepository.InsertAsync(newUser);

                await _unitOfWork.CommitTransaction();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransaction();
                return new BaseResponseModel<UserResponseModel>
                {
                    Code = 500,
                    Message = ex.Message,
                    Data = null
                };
            }

            return new BaseResponseModel<UserResponseModel>
            {
                Code = 201,
                Message = "User Created Success",
                Data = _mapper.Map<UserResponseModel>(newUser)
            };
        }
        public async Task<BaseResponseModel<UserResponseModel>> UpdateAsync(UserRequestModelForUpdate request, Guid id)
        {
            var userRepository = _unitOfWork.Repository<User>();

            var existedUser = await userRepository.FindAsync(id);

            if (existedUser == null)
            {
                return new BaseResponseModel<UserResponseModel>
                {
                    Code = 404,
                    Message = "User not exists",
                    Data = null
                };
            }

            _mapper.Map(request, existedUser);

            try
            {
                await _unitOfWork.BeginTransaction();

                await userRepository.UpdateAsync(existedUser);

                await _unitOfWork.CommitTransaction();

            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransaction();
                return new BaseResponseModel<UserResponseModel>
                {
                    Code = 500,
                    Message = ex.Message,
                    Data = null
                };
            }

            return new BaseResponseModel<UserResponseModel>
            {
                Code = 200,
                Message = "User Updated Success",
                Data = _mapper.Map<UserResponseModel>(existedUser)
            };
        }
        public async Task<BaseResponseModel<UserResponseModel>> GetDetailAsync(Guid id)
        {
            var userRepository = _unitOfWork.Repository<User>();

            var existedUser = await userRepository.FindAsync(id);

            if (existedUser == null)
            {
                return new BaseResponseModel<UserResponseModel>
                {
                    Code = 404,
                    Message = "User not exists",
                    Data = null
                };
            }

            return new BaseResponseModel<UserResponseModel>
            {
                Code = 200,
                Message = "Get User Detail Success",
                Data = _mapper.Map<UserResponseModel>(existedUser)
            };
        }

        public async Task<BaseResponseModel<IEnumerable<UserResponseModel>>> GetAllUsersAsync()
        {
            var userRepository = _unitOfWork.Repository<User>();

            var users = await userRepository.GetAllAsync();
            var userResponseModels = _mapper.Map<IEnumerable<UserResponseModel>>(users);

            if (users.Count() == 0)
            {
                return new BaseResponseModel<IEnumerable<UserResponseModel>>
                {
                    Code = 200,
                    Message = "No Users in the list",
                    Data = userResponseModels
                };
            }

            return new BaseResponseModel<IEnumerable<UserResponseModel>>
            {
                Code = 200,
                Message = "Users retrieved successfully",
                Data = userResponseModels
            };
        }

        public async Task<BaseResponseModel> DeleteAsync(Guid id)
        {
            var userRepository = _unitOfWork.Repository<User>();

            var existedUser = await userRepository.FindAsync(id);

            if (existedUser == null)
            {
                return new BaseResponseModel
                {
                    Code = 404,
                    Message = "User not found",
                };
            }

            try
            {
                await _unitOfWork.BeginTransaction();

                await userRepository.DeleteAsync(existedUser);

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
                Message = "User is Deleted Successfully",
            };
        }
    }
}
