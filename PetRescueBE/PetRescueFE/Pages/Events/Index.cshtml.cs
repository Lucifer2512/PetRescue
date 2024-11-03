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
    public class IndexModel : PageModel
    {
        
        private readonly ApiService _apiService;
        private readonly IMapper _mapper;

        public IndexModel(ApiService apiService, IMapper mapper)
        {
            _apiService = apiService;
            _mapper = mapper;
        }

        public IList<Event> Event { get;set; } = default!;

        public async Task<ActionResult> OnGetAsync()
        {
            var data = await _apiService.GetAsync<BaseResponseModel<List<EventResponseModel>>>("events/");
            if (data is null)
            {
                return NotFound();
            }
            
            var eventsRaw = data.Data.ToList();
            
            var eventCooked = _mapper.Map<List<Event>>(eventsRaw);
            
            return Page();
        }
    }
}
