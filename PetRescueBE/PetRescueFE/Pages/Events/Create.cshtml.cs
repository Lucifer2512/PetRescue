using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.Model.Request;
using BusinessLayer.Model.Response;
using BusinessLayer.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataAccessLayer.Context;
using DataAccessLayer.Entity;

namespace PetRescueFE.Pages.Events
{
    public class CreateModel : PageModel
    {
        private readonly ApiService _apiService;
        private readonly IMapper _mapper;

        public CreateModel(ApiService apiService, IMapper mapper)
        {
            _apiService = apiService;
            _mapper = mapper;
        }

        public IActionResult OnGet()
        {
        
            return Page();
        }

        [BindProperty]
        public Event Event { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _apiService == null || Event == null)
            {
                return Page();
            }
            
            // do something to create the event
            /*EventResponseModel? request = _mapper.Map<EventResponseModel>(Event) ?? new ();*/
            
            EventRequestModel4Create? create = _mapper.Map<EventRequestModel4Create>(Event) ?? new ();
            await _apiService.PostAsync<EventRequestModel4Create, EventResponseModel>("events/", create);
            
            return RedirectToPage("./Index");
        }
    }
}
