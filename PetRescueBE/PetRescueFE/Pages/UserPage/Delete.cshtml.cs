﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetRescueFE.Pages.Model;

namespace PetRescueFE.Pages.UserPage
{
    public class DeleteModel : PageModel
    {
        private readonly ApiService _apiService;

        public DeleteModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        [BindProperty]
        public UserResponseModelFE User { get; set; } = default!;

        private async Task<bool> LoadUserAsync(Guid id)
        {
            var apiUrl = $"https://localhost:7297/api/users/{id}";
            var response = await _apiService.GetAsync<BaseResponseModelFE<UserResponseModelFE>>(apiUrl);

            if (response.Data != null)
            {
                User = response.Data;
                return true;
            }

            return false;
        }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            var role = HttpContext.Session.GetString("Role");

            if (role != "d290f1ee-6c54-4b01-90e6-d701748f0851")
            {
                return RedirectToPage("/AuthorizationError");
            }

            if (id == null || !await LoadUserAsync(id.Value))
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apiUrl = $"https://localhost:7297/api/users/{id}";

            try
            {
                var loggedInUserId = HttpContext.Session.GetString("UserId");

                if (loggedInUserId != null && loggedInUserId == id.ToString())
                {
                    ModelState.AddModelError(string.Empty, "You cannot delete your own account.");
                    await LoadUserAsync(id.Value);
                    return Page();
                }

                var response = await _apiService.DeleteAsync(apiUrl);

                if (response == false)
                {
                    ModelState.AddModelError(string.Empty, "There is other information related to this user, so this CANNOT BE DELETED.");
                    await LoadUserAsync(id.Value);
                    return Page();
                }


            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "There is other information related to this user, so this CANNOT BE DELETED.");
                await LoadUserAsync(id.Value);
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
