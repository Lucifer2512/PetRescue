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
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Context;
using DataAccessLayer.Entity;

namespace PetRescueFE.Pages.Events
{
    public class EditModel : PageModel
    {
        private readonly ApiService _apiService;
        private readonly IMapper _mapper;

        public EditModel(ApiService apiService, IMapper mapper)
        {
            _apiService = apiService;
            _mapper = mapper;
        }


        [BindProperty] public Event Event { get; set; } = default!;
        private Guid? eventIdTmp = null;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            var response = await _apiService.GetAsync<BaseResponseModel<EventResponseModel>>("events/" + id);
            if (response == null)
            {
                return NotFound();
            }

            var data = response.Data;
            Event = _mapper.Map<Event>(data);
            eventIdTmp = Event.EventId;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (!ModelState.IsValid || _apiService == null || Event == null)
            {
                return Page();
            }
            
            // do something to update the event
            EventRequestModel4Update? update = _mapper.Map<EventRequestModel4Update>(Event) ?? new ();
            
            await _apiService.PutAsync<EventRequestModel4Update, ActionResult>("events/" + Event.EventId, update);
            
            return RedirectToPage("./Index");
        }
        
        
        /*private bool EventExists(Guid id)
        {
            return (_context.Events?.Any(e => e.EventId == id)).GetValueOrDefault();
        }*/
    }
}