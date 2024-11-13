using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Context;
using DataAccessLayer.Entity;
using PetRescueFE.Pages.Model;
using System.IdentityModel.Tokens.Jwt;

namespace PetRescueFE.Pages.PetPage
{
    public class DeleteModel : PageModel
    {
        private readonly ApiService _apiService;

        public DeleteModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        [BindProperty]
        public PetResponseModelFE Pet { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            var role = HttpContext.Session.GetString("Role");
            if (string.IsNullOrEmpty(role) || role == "e7b8f3d2-4a2f-4c3b-8f4d-9c5d8a3e1b2c") // User role
            {
                return RedirectToPage("/Login");
            }

            if (id == null)
            {
                return NotFound();
            }

            // Gọi API để lấy thông tin Pet
            var response = await _apiService.GetAsync<BaseResponseModelFE<PetResponseModelFE>>($"https://localhost:7297/api/pet/id?id={id}");

            if (response == null || response.Data == null)
            {
                return NotFound();
            }

            Pet = response.Data;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Gọi API để xóa Pet
            var response = await _apiService.DeleteAsync($"https://localhost:7297/api/pet/id?id={id}");

            if (response == null)
            {
                ModelState.AddModelError(string.Empty, "Failed to delete the pet.");
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
