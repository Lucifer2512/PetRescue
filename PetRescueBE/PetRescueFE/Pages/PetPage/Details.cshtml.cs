using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataAccessLayer.Entity;
using PetRescueFE.Pages.Model;

namespace PetRescueFE.Pages.PetPage
{
    public class DetailsModel : PageModel
    {
        private readonly ApiService _apiService;

        public DetailsModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        public PetResponseModelFE Pet { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Gọi API để lấy chi tiết Pet theo ID
            var response = await _apiService.GetAsync<BaseResponseModelFE<PetResponseModelFE>>($"https://localhost:7297/api/pet/id?id={id}");


            if (response == null || response.Data == null)
            {
                return NotFound();
            }

            Pet = response.Data;
            return Page();
        }
    }
}
