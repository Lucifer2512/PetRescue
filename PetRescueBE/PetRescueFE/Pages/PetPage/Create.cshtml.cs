using DataAccessLayer.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PetRescueFE.Pages.Model;
using PetRescueFE.Pages.Model.Shelters;
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

        public IList<SelectListItem> PetOptions { get; set; } = new List<SelectListItem>();
     
        public async Task<IActionResult> OnGetAsync()
        {
            var apiUrl = "https://localhost:7297/api/shelter";
            var response = await _apiService.GetAsync<BaseResponseModelFE<IList<ShelterResponseModel>>>(apiUrl);

            // Kiểm tra phản hồi từ API
            if (response == null || response.Data == null)
            {
                ModelState.AddModelError(string.Empty, "Unable to load shelter data.");
                return Page();
            }

            // Thiết lập PetOptions cho dropdown từ dữ liệu shelter lấy từ API
            PetOptions = response.Data.Select(s => new SelectListItem
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
