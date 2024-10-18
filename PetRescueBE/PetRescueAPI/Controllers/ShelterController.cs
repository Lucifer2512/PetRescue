using BusinessLayer.IServices;
using BusinessLayer.Model.Request;
using BusinessLayer.Models.Request;
using BusinessLayer.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace PetRescueAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShelterController : ControllerBase
    {
        private readonly IShelterService _shelterService;

        public ShelterController(IShelterService shelterService)
        {
            _shelterService = shelterService;
        }

        [HttpPost]
        public async Task<IActionResult> AddShelter([FromBody] ShelterRequestModel request)
        {
            var response = await _shelterService.AddAsync(request);
            return StatusCode((int)response.Code, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShelter(Guid id, [FromBody] ShelterRequestModelForUpdate request)
        {
            var response = await _shelterService.UpdateAsync(request, id);
            return StatusCode((int)response.Code, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetShelterDetail(Guid id)
        {
            var response = await _shelterService.GetDetailAsync(id);
            return StatusCode((int)response.Code, response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllShelters()
        {
            var response = await _shelterService.GetAllAsync();
            return StatusCode((int)response.Code, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShelter(Guid id)
        {
            var response = await _shelterService.DeleteAsync(id);
            return StatusCode((int)response.Code, response);
        }
    }
}

