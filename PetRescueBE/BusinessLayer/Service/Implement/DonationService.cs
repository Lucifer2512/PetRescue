using AutoMapper;
using BusinessLayer.Model.Request;
using BusinessLayer.Model.Response;
using BusinessLayer.Service.Interface;
using BusinessLayer.Service.Interface.ConstantsService;
using DataAccessLayer.Entity;
using DataAccessLayer.UnitOfWork.Interface;
using Net.payOS;
using Net.payOS.Types;

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
        public async Task<BaseResponseModel<IEnumerable<DonationReponseModel>>> GetAllbyIdUserAsync(Guid id)
        {
            var DonationRepo = _unitOfWork.Repository<Donation>();
            var donations = await DonationRepo.GetAllAsync();
            var listDonations = donations.Where(d => d.UserId == id);

            var donationReponse = _mapper.Map<IEnumerable<DonationReponseModel>>(listDonations);

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
        public async Task<BaseResponseModel<DonationReponseModel>> UpdateStatusDonate(Guid id, string status)
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
            ExistedDonations.Status = status;
            await DonationRepo.UpdateAsync(ExistedDonations);

            return new BaseResponseModel<DonationReponseModel>
            {
                Code = 200,
                Message = "Success",
                Data = _mapper.Map<DonationReponseModel>(ExistedDonations)
            };
        }
        public async Task<BaseResponseModel<DonationReponseModel>> GetDonationbyNotes(String notes)
        {
            var donationRepo = _unitOfWork.Repository<Donation>();
            var listDonation = await donationRepo.GetAllAsync();
            var donation = listDonation.Where(n => n.Notes == notes).FirstOrDefault();


            if (donation == null)
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
                Data = _mapper.Map<DonationReponseModel>(donation)
            };

        }
        public async Task<string> GenerateQRBanking(Guid EventID, Guid ShelterID, Guid UserId, int amount)
        {
            var DonationRepo = _unitOfWork.Repository<Donation>();
            PayOS payOS;
            payOS = new PayOS(Constants.clientId, Constants.apiKey, Constants.checksumKey);
            var noidung = "Donation for Pet";
            var orderID = randomOrderId();
            //int amountDonation = amount;
            DonationRequestModel donation = new DonationRequestModel();
            donation.EventId = EventID;
            donation.ShelterId = ShelterID;
            donation.UserId = UserId;
            donation.Amount = amount;
            donation.PaymentMethod = "Banking";
            donation.Notes = orderID.ToString();
            donation.Status = "Waiting....";
            var check = await AddAsync(donation);

            var cancelUrl = "https://localhost:7132/"; // Replace with your actual cancel URL
                                                       // var returnUrl = "https://tutor-serverapi20240712023601.azurewebsites.net/api/Transaction/PaymentandUpdateTransaction/orderCode=" + orderID; // Replace with your actual success URL
            var returnUrl = "https://localhost:7297/api/donation/orderCode=" + orderID;
            //string dataToSign = $"amount={amount}&cancelUrl={cancelUrl}&description={noidung}&orderCode={check.Data.DonationId}&returnUrl={returnUrl}";
            //string signature = GenerateSignature(dataToSign, Constants.checksumKey);
            PaymentData paymentData = new PaymentData(orderID, amount, noidung, null, cancelUrl, returnUrl, null);
            CreatePaymentResult createPayment = await payOS.createPaymentLink(paymentData);
            String linkCheckOut = createPayment.checkoutUrl;
            return linkCheckOut;

        }

        public int randomOrderId()
        {
            Random random = new Random();
            int sixDigitNumber = random.Next(100000, 1000000);
            return sixDigitNumber;
        }
    }
}
