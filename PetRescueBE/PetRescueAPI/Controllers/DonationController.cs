using BusinessLayer.Model.Request;
using BusinessLayer.Model.Response;
using BusinessLayer.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace PetRescueAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DonationController : Controller
    {
        private readonly IDonationService _donationService;
        private readonly IShelterService _shelterService;
        public DonationController( IDonationService donationService,IShelterService shelterService)
        {
            _donationService = donationService;
            _shelterService = shelterService;   
        }
        [HttpPost("CreateDonation")]
        public async Task<IActionResult> CreateDonation([FromBody] DonationRequestModel request)
        {
            var response = await _donationService.AddAsync(request);
            return StatusCode((int)response.Code, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDonationbyId(Guid id)
        {
            var response = await _donationService.GetDetailAsync(id);
            return StatusCode((int)response.Code, response);
        }
        [HttpGet("GetAllDonation")]
        public async Task<IActionResult> GetDonation()
        {
            var response = await _donationService.GetAllAsync();
            return StatusCode((int)response.Code, response);
        }

        [HttpPost("donationQRUrl")]
        public async Task<IActionResult> CreateQRCodeandTransaction([FromBody] DonationRequestModelQRCode request)
        {
            try
            {

                String url = await _donationService.GenerateQRBanking(request.EventId, request.ShelterId, request.UserId, request.Amount);
                if (url != null)
                {
                    return Ok(new BaseResponseModel<String>()
                    {
                        Code = Ok().StatusCode,
                        Message = "Success",
                        Data = url
                    });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
            return BadRequest();
        }

        [HttpGet("orderCode={ordercode}")]
        public async Task<IActionResult> PaymentandUpdateStatusandBalance(string ordercode)
        {
            try
            {
                var donation = await _donationService.GetDonationbyNotes(ordercode);
                donation.Data.Status = "Payment success";
                var updateDonation = await _donationService.UpdateStatusDonate(donation.Data.DonationId, donation.Data.Status);
                 var check = await _shelterService.UpdateBalanceAsync(donation.Data.ShelterId, donation.Data.Amount);                
                if (check.Code == 200 && updateDonation.Code == 200)
                {
                    try
                    {
                        
                    return Redirect("https://localhost:7132/UserPage");
                        
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
