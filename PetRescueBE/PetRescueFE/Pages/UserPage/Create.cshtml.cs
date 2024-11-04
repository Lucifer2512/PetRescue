using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataAccessLayer.Context;
using DataAccessLayer.Entity;
using BusinessLayer.Models.Request;
using BusinessLayer.Models.Response;
using System.Linq.Expressions;

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
        public UserRequestModel User { get; set; } = new UserRequestModel();

        public async Task<IActionResult> OnGetAsync()
        {
            var apiUrl = "https://localhost:7297/api/users/roles";
            var response = await _apiService.GetAsync<BaseResponseModel<IEnumerable<Role>>>(apiUrl);

            if (response.Data != null)
            {
                RoleList = response.Data.Select(role => new SelectListItem
                {
                    Value = role.RoleId.ToString(),
                    Text = role.RoleName
                }).ToList();
            }

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
                var response = await _apiService.PostAsync<UserRequestModel, BaseResponseModel<UserResponseModel>>(apiUrl, User);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error creating user.");
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
