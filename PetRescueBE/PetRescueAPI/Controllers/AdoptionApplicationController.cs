using BusinessLayer.Model.Request;
using BusinessLayer.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace PetRescueAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdoptionApplicationController : Controller
    {
        private readonly IAdoptionApplicationservice _adoptionApplicationservice;
        public AdoptionApplicationController(IAdoptionApplicationservice adoptionApplicationservice)
        {
            adoptionApplicationservice = _adoptionApplicationservice;
        }

        [HttpPost("add")]
        public async Task<IActionResult> CreateAdoption([FromBody] AdoptionApplicationRequestModel request)
        {
            var response = await _adoptionApplicationservice.AddAsync(request);
            return StatusCode((int)response.Code, response);
        }

        [HttpGet()]
        public async Task<IActionResult> GetAdoption()
        {
            var response = await _adoptionApplicationservice.GetAllAsync();
            return StatusCode((int)response.Code, response);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetAdoptionDetail(Guid id)
        {
            var response = await _adoptionApplicationservice.GetDetailAsync(id);
            return StatusCode((int)response.Code, response);
        }

        [HttpPut("id")]
        public async Task<IActionResult> UpdateAdoption(Guid id, [FromBody] AdoptionApplicationRequestModel requestModel)
        {
            var response = await _adoptionApplicationservice.UpdateAsunc(id,requestModel);
            return StatusCode((int)response.Code, response);
        }

    }
}
