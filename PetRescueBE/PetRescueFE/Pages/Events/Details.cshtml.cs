using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.Model.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Context;
using DataAccessLayer.Entity;
using Pages.Model;

namespace PetRescueFE.Pages.Events
{
    public class DetailsModel : PageModel
    {
        private readonly ApiService _apiService;
        private readonly IMapper _mapper;

        public DetailsModel(IMapper mapper, ApiService apiService)
        {
            _mapper = mapper;
            _apiService = apiService;
        }


        public Event Event { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            var response = await _apiService.GetAsync<BaseResponseModelFE<EventResponseModel>>("events/" + id);
            if (response == null)
            {
                return NotFound();
            }
            
            var data = response.Data;
            Event = _mapper.Map<Event>(data);
            return Page();
        }
    }
}
