using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.Model.Request;
using BusinessLayer.Model.Response;
using BusinessLayer.Models.Response;
using BusinessLayer.Service.Interface;
using DataAccessLayer.Entity;
using DataAccessLayer.UnitOfWork.Interface;

namespace BusinessLayer.Service.Implement
{
    public class AdoptionApplicationservice : IAdoptionApplicationservice
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AdoptionApplicationservice(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponseModel<AdoptionApplicationResponseModel>> AddAsync(AdoptionApplicationRequestModel request)
        {
            var username = GetUsernameFromToken();
            if (string.IsNullOrEmpty(username))
            {
                return new BaseResponseModel<AdoptionApplicationResponseModel>
                {
                    Code = 401,
                    Message = "User not authenticated.",
                    Data = null
                };
            }

            // Tìm user bằng username
            var user = await _unitOfWork.Repository<User>().FirstOrDefaultAsync(u => u.Email == username);

            if (user == null)
            {
                return new BaseResponseModel<AdoptionApplicationResponseModel>
                {
                    Code = 404,
                    Message = "User not found.",
                    Data = null
                };
            }

            // Tạo mới đối tượng AdoptionApplication
            var newAdoptionApplication = _mapper.Map<AdoptionApplication>(request);
            newAdoptionApplication.ApplicationId = Guid.NewGuid();
            newAdoptionApplication.UserId = user.UserId;
            newAdoptionApplication.Status = request.Status;
            newAdoptionApplication.RequestDate = DateTime.Now;
   //         var checkPet = _unitOfWork.Repository<Pet>.Ge
            // Lưu đối tượng mới
            await _unitOfWork.Repository<AdoptionApplication>().InsertAsync(newAdoptionApplication);
            await _unitOfWork.SaveChangesAsync();

            // Chuẩn bị đối tượng phản hồi
            var responseModel = _mapper.Map<AdoptionApplicationResponseModel>(newAdoptionApplication);

            // Trả về kết quả thành công
            return new BaseResponseModel<AdoptionApplicationResponseModel>
            {
                Code = 200,
                Message = "Adoption application created successfully.",
                Data = responseModel
            };
        }

        public Task<BaseResponseModel> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponseModel<IEnumerable<AdoptionApplicationResponseModel>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponseModel<AdoptionApplicationResponseModel>> GetDetailAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponseModel<AdoptionApplicationResponseModel>> UpdateAsunc(Guid id,AdoptionApplicationRequestModel request)
        {
            throw new NotImplementedException();
        }

        private string GetUsernameFromToken()
        {
            var username = string.Empty;
            if (ClaimsPrincipal.Current != null)
            {
                username = ClaimsPrincipal.Current.FindFirst(ClaimTypes.Name)?.Value;
            }
            return username;
        }
   

       
    }
}
