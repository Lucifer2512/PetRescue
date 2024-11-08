using DataAccessLayer.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PetRescueFE.Pages.Model;
using System.ComponentModel.DataAnnotations;

namespace PetRescueFE.Pages.UserPage
{
    public class EditModel : PageModel
    {
        private readonly ApiService _apiService;

        public EditModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        [BindProperty]
        public UserResponseModel User { get; set; } = default!;

        public List<SelectListItem> RoleList { get; set; } = new List<SelectListItem>();

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Fetch user details
            var apiUrlUser = $"https://localhost:7297/api/users/{id}";
            var userResponse = await _apiService.GetAsync<BaseResponseModelFE<UserResponseModel>>(apiUrlUser);
            if (userResponse.Data == null)
            {
                return NotFound();
            }
            User = userResponse.Data;

            // Fetch roles for the dropdown
            var apiUrlRoles = "https://localhost:7297/api/users/roles";
            var rolesResponse = await _apiService.GetAsync<BaseResponseModelFE<List<Role>>>(apiUrlRoles);
            if (rolesResponse.Data != null)
            {
                RoleList = rolesResponse.Data.ConvertAll(role =>
                    new SelectListItem { Value = role.RoleId.ToString(), Text = role.RoleName });
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var apiUrl = $"https://localhost:7297/api/users/{User.UserId}";
            var userRequest = new UserRequestModelForUpdate()
            {
                FirstName = User.FirstName,
                LastName = User.LastName,
                Email = User.Email,
                PhoneNumber = User.PhoneNumber,
                Address = User.Address,
                Status = User.Status
            };

            var validationContext = new ValidationContext(userRequest);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(userRequest, validationContext, validationResults, true);

            if (!isValid)
            {
                foreach (var validationResult in validationResults)
                {
                    ModelState.AddModelError(string.Empty, validationResult.ErrorMessage);
                }
                return Page();
            }

            try
            {
                var response = await _apiService.PutAsync<UserRequestModelForUpdate, BaseResponseModelFE<UserResponseModel>>(apiUrl, userRequest);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Failed to update user.");
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
