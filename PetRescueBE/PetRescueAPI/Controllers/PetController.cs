using BusinessLayer.Model.Request;
using BusinessLayer.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace PetRescueAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetController : Controller
    {
        private readonly IPetService _petService;

        public PetController(IPetService petService)
        {
            _petService = petService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddPet([FromBody] PetAddRequestModel requestModel)
        {
            var response = await _petService.AddAsync(requestModel);
            return StatusCode((int)response.Code, response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _petService.GetAllAsync();
            return StatusCode((int)response.Code, response);
        }
        [HttpGet("search")]
        public async Task<IActionResult> GetPetBySearch(string? searchTerm)
        {
            var response = await _petService.GetBySearchAsync(searchTerm);
            return StatusCode((int)response.Code, response);
        }
        [HttpGet("user-search")]
        public async Task<IActionResult> GetPetByUserSearch(string? searchTerm)
        {
            var response = await _petService.GetByUserSearchAsync(searchTerm);
            return StatusCode((int)response.Code, response);
        }
        [HttpGet("id")]
        public async Task<IActionResult> PetDetail(Guid id)
        {
            var response = await _petService.GetDetailAsync(id);
            return StatusCode((int)response.Code, response);
        }
        [HttpGet("shelter/{id}")]
        public async Task<IActionResult> PetByShelter(Guid id,string? searchTerm)
        {
            var response = await _petService.GetByShelterAsync(id, searchTerm);
            return StatusCode((int)response.Code, response);
        }
        [HttpGet("user-shelter/{id}")]
        public async Task<IActionResult> PetByUserShelter(Guid id, string? searchTerm)
        {
            var response = await _petService.GetByUserShelterAsync(id, searchTerm);
            return StatusCode((int)response.Code, response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdatePet([FromBody] PetUpdateRequestModel petUpdateRequestModel)
        {
            var response = await _petService.UpdateASync(petUpdateRequestModel);
            return StatusCode((int)response.Code, response);
        }
        [HttpDelete("id")]
        public async Task<IActionResult> DeletePet(Guid id)
        {
            var response = await _petService.DeleteAsync(id);
            return StatusCode((int)response.Code, response);
        }

    }
}
