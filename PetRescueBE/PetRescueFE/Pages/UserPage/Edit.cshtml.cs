﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetRescueFE.Pages.Model;
using System.ComponentModel.DataAnnotations;

namespace PetRescueFE.Pages.UserPage
{
    public class EditModel : PageModel
    {
        private readonly ApiService _apiService;
        [BindProperty]
        public IFormFile? ImageFile { get; set; }
        public string UserRole { get; private set; }

        public EditModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        [BindProperty]
        public UserResponseModelFE User { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            UserRole = HttpContext.Session.GetString("Role");

            // Fetch user details
            var apiUrlUser = $"https://localhost:7297/api/users/{id}";
            var userResponse = await _apiService.GetAsync<BaseResponseModelFE<UserResponseModelFE>>(apiUrlUser);
            if (userResponse.Data == null)
            {
                return NotFound();
            }
            User = userResponse.Data;

            var loggedInUserId = HttpContext.Session.GetString("UserId");

            if (loggedInUserId != null && loggedInUserId != User.UserId.ToString() && UserRole != "d290f1ee-6c54-4b01-90e6-d701748f0851")
            {
                return RedirectToPage("/AuthorizationError");
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
                Status = User.Status,
                Image = ImageFile != null ? await _apiService.ConvertToByteArrayAsync(ImageFile) : null,
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
                var response = await _apiService.PutAsync<UserRequestModelForUpdate, BaseResponseModelFE<UserResponseModelFE>>(apiUrl, userRequest);
                if (response.Code != 200)
                {
                    ModelState.AddModelError(string.Empty, "User Email already exists.");
                    return Page();
                }
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
