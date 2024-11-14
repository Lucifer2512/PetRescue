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

        [HttpGet("ForUser")]
        public async Task<IActionResult> GetAllForUser(
            int index,
            int size,
            string? searchTerm = null,
            string? species = null,
            string? gender = null,
            Guid? shelterId = null)
        {
            var response = await _petService.GetAllForUserAsync(searchTerm, species, gender, shelterId, index, size);
            return StatusCode((int)response.Code, response);
        }

        [HttpGet("ForShelter/{userId}")]
        public async Task<IActionResult> GetAllForShelter(
            Guid userId,
            int index,
            int size,
            string? searchTerm = null,
            string? species = null,
            string? gender = null,
            string? status = null)
        {
            var response = await _petService.GetAllForShelterAsync(userId, searchTerm, species, gender, status, index, size);
            return StatusCode((int)response.Code, response);
        }

        [HttpGet("id")]
        public async Task<IActionResult> PetDetail(Guid id)
        {
            var response = await _petService.GetDetailAsync(id);
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
