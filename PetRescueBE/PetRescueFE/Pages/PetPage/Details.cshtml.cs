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
        public bool IsAdmin { get; private set; }
        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            var role = HttpContext.Session.GetString("Role");
            if (role == "d290f1ee-6c54-4b01-90e6-d701748f0851" || role == "f3c8d4e5-6b7a-4c9d-8e2f-0a1b2c3d4e5f")
            {
                IsAdmin = true;
            }
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
