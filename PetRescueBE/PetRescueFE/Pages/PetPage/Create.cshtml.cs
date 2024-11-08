using DataAccessLayer.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PetRescueFE.Pages.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetRescueFE.Pages.PetPage
{
    public class CreateModel : PageModel
    {
        private readonly ApiService _apiService;

        public CreateModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        [BindProperty]
        public PetAddRequestModelFE Pet { get; set; } = new PetAddRequestModelFE();

        public List<SelectListItem> PetOptions { get; set; } = new List<SelectListItem>();

        public async Task<IActionResult> OnGetAsync()
        {
            // Danh sách shelter tạm thời
            var predefinedShelters = new List<Shelter>
    {
        new Shelter { ShelterId = Guid.Parse("61c88ed1-7b25-4956-b746-41908b607cb3"), ShelterName = "Shelter A" },
        new Shelter { ShelterId = Guid.NewGuid(), ShelterName = "Shelter B" },
        new Shelter { ShelterId = Guid.NewGuid(), ShelterName = "Shelter C" }
    };

            // Thiết lập PetOptions cho dropdown từ danh sách shelter tạm thời
            PetOptions = predefinedShelters.Select(s => new SelectListItem
            {
                Value = s.ShelterId.ToString(),
                Text = s.ShelterName
            }).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || Pet == null)
            {
                return Page();
            }

            // Gọi API để tạo pet mới
            var response = await _apiService.PostAsync<PetAddRequestModelFE, BaseResponseModelFE<PetResponseModelFE>>("https://localhost:7297/api/pet/add", Pet);
          
            if (response == null || response.Data == null)
            {
                
                ModelState.AddModelError(string.Empty, response.Message.ToString());
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
