using AutoMapper;
using BusinessLayer.Model.Request;
using BusinessLayer.Model.Response;
using BusinessLayer.Service.Interface;
using DataAccessLayer.Entity;
using DataAccessLayer.UnitOfWork.Interface;

namespace BusinessLayer.Service.Implement
{
    public class DonationService : IDonationService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DonationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseResponseModel<DonationReponseModel>> AddAsync(DonationRequestModel request)
        {

            var donationRepo = _unitOfWork.Repository<Donation>();

            var newDonation = _mapper.Map<Donation>(request);
            newDonation.DonationId = Guid.NewGuid();
            newDonation.DonationDate = DateTime.Now;

            try
            {

                await _unitOfWork.BeginTransaction();
                await donationRepo.InsertAsync(newDonation);
                var isSaved = await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransaction();
                return new BaseResponseModel<DonationReponseModel>
                {
                    Code = 201,
                    Message = "Created Donation",
                    Data = null
                };
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
                Console.WriteLine(e);
                return new BaseResponseModel<DonationReponseModel>
                {
                    Code = 500,
                    Message = "Failed to save event, " + e.InnerException,
                    Data = null
                };
            }

        }

        public Task<BaseResponseModel> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponseModel<IEnumerable<DonationReponseModel>>> GetAllAsync()
        {
            var DonationRepo = _unitOfWork.Repository<Donation>();
            var donations = await DonationRepo.GetAllAsync();
            var donationReponse = _mapper.Map<IEnumerable<DonationReponseModel>>(donations);

            if (donations.Count() == 0)
            {
                return new BaseResponseModel<IEnumerable<DonationReponseModel>>
                {
                    Code = 404,
                    Message = "No donation found, return null as no data",
                    Data = donationReponse
                };
            }

            return new BaseResponseModel<IEnumerable<DonationReponseModel>>
            {
                Code = 200,
                Message = "Success",
                Data = donationReponse
            };
        }

        public async Task<BaseResponseModel<DonationReponseModel>> GetDetailAsync(Guid id)
        {
            var DonationRepo = _unitOfWork.Repository<Donation>();
            var ExistedDonations = await DonationRepo.FindAsync(id);


            if (ExistedDonations == null)
            {
                return new BaseResponseModel<DonationReponseModel>
                {
                    Code = 404,
                    Message = "Donation not exists",
                    Data = null
                };
            }


            return new BaseResponseModel<DonationReponseModel>
            {
                Code = 200,
                Message = "Get Donation Detail Success",
                Data = _mapper.Map<DonationReponseModel>(ExistedDonations)
            };

        }
    }
}
