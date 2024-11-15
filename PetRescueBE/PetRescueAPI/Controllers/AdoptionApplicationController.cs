using BusinessLayer.Model.Request;
using BusinessLayer.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace PetRescueAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdoptionApplicationController : Controller
    {
        private readonly IAdoptionApplicationService _adoptionApplicationService;

        public AdoptionApplicationController(IAdoptionApplicationService adoptionApplicationService)
        {
            _adoptionApplicationService = adoptionApplicationService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetApplicationDetail(Guid id)
        {
            var response = await _adoptionApplicationService.GetDetailAsync(id);
            return StatusCode((int)response.Code, response);
        }

        [HttpGet("onlyid/{id}")]
        public async Task<IActionResult> GetApplicationDetaiOnlyId(Guid id)
        {
            var response = await _adoptionApplicationService.GetDetailOnlyIdAsync(id);
            return StatusCode((int)response.Code, response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllApplications(string status)
        {
            var response = await _adoptionApplicationService.GetAllAsync(status);
            return StatusCode((int)response.Code, response);
        }

        [HttpGet("shelter/{id}")]
        public async Task<IActionResult> GetAllApplicationsForShelter(Guid id, string status)
        {
            var response = await _adoptionApplicationService.GetAllForShelterAsync(id, status);
            return StatusCode((int)response.Code, response);
        }

        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetAllApplicationsForUser(Guid id, string status)
        {
            var response = await _adoptionApplicationService.GetAllForUserAsync(id, status);
            return StatusCode((int)response.Code, response);
        }

        [HttpPost]
        public async Task<IActionResult> AddApplication([FromBody] AdoptionApplicationRequestModel request)
        {
            var response = await _adoptionApplicationService.AddAsync(request);
            return StatusCode((int)response.Code, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateApplication(Guid id, [FromBody] AdoptionApplicationRequestModelForUpdate request)
        {
            var response = await _adoptionApplicationService.UpdateAsync(request, id);
            return StatusCode((int)response.Code, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplication(Guid id)
        {
            var response = await _adoptionApplicationService.DeleteAsync(id);
            return StatusCode((int)response.Code, response);
        }
    }
}
