﻿using BusinessLayer.Model.Request;
using BusinessLayer.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace PetRescueAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DonationController : Controller
    {
        private readonly IDonationService _donationService;
        public DonationController(IDonationService donationService)
        {
            _donationService = donationService;
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

    }
}
