using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Context;
using DataAccessLayer.Entity;
using PetRescueFE.Pages.Model;

namespace PetRescueFE.Pages.PetPage
{
    public class EditModel : PageModel
    {
        private readonly ApiService _apiService;

        public EditModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        [BindProperty]
        public PetUpdateRequestModelFE Pet { get; set; } = default!;

        public List<SelectListItem> ShelterOptions { get; set; } = new List<SelectListItem>();

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Gọi API để lấy thông tin Pet
            var response = await _apiService.GetAsync<BaseResponseModelFE<PetUpdateRequestModelFE>>($"https://localhost:7297/api/pet/id?id={id}");

            if (response == null || response.Data == null)
            {
                return NotFound();
            }

            Pet = response.Data;

            // Danh sách shelter tạm thời để sử dụng làm dropdown
            var predefinedShelters = new List<Shelter>
            {
                new Shelter { ShelterId = Guid.Parse("61c88ed1-7b25-4956-b746-41908b607cb3"), ShelterName = "Shelter A" },
                new Shelter { ShelterId = Guid.Parse("4dc88ed1-7b25-4956-b746-41908b607cb3"), ShelterName = "Shelter B" },
                new Shelter { ShelterId = Guid.Parse("8ac88ed1-7b25-4956-b746-41908b607cb3"), ShelterName = "Shelter C" }
            };

            ShelterOptions = predefinedShelters.Select(s => new SelectListItem
            {
                Value = s.ShelterId.ToString(),
                Text = s.ShelterName
            }).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Gọi API để cập nhật Pet
            var response = await _apiService.PutAsync<PetUpdateRequestModelFE, BaseResponseModelFE<PetResponseModelFE>>(
                $"https://localhost:7297/api/pet/update", Pet);

            if (response == null || response.Code != 200)
            {
                ModelState.AddModelError(string.Empty, "Failed to update the pet.");
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
