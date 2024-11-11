using DataAccessLayer.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PetRescueFE.Pages.Model;

namespace PetRescueFE.Pages.UserPage
{
    public class CreateModel : PageModel
    {
        private readonly ApiService _apiService;

        public CreateModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        public List<SelectListItem> RoleList { get; set; }

        [BindProperty]
        public UserRequestModelv2FE User { get; set; } = new UserRequestModelv2FE();

        private async Task LoadRolesAsync()
        {
            var apiUrl = "https://localhost:7297/api/users/roles";
            var response = await _apiService.GetAsync<BaseResponseModelFE<IEnumerable<Role>>>(apiUrl);

            if (response.Data != null)
            {
                RoleList = response.Data.Select(role => new SelectListItem
                {
                    Value = role.RoleId.ToString(),
                    Text = role.RoleName
                }).ToList();
            }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadRolesAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var apiUrl = "https://localhost:7297/api/users";

            try
            {
                var response = await _apiService.PostAsync<UserRequestModelv2FE, BaseResponseModelFE<UserResponseModel>>(apiUrl, User);
                if (response.Code != 201)
                {
                    ModelState.AddModelError(string.Empty, "User Email already exists.");
                    await LoadRolesAsync();
                    return Page();
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Error creating user.");
                await LoadRolesAsync();
                return Page();
            }


            return RedirectToPage("./Index");
        }
    }
}
