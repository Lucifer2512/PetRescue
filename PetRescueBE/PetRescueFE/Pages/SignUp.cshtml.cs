using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetRescueFE.Pages.Model;

namespace PetRescueFE.Pages
{
    public class SignUpModel : PageModel
    {
        [BindProperty]
        public UserRequestModelFE AccountRegister { get; set; }
        private ApiService _apiService;
        public SignUpModel(ApiService apiService, INotyfService notyfService) { _apiService = apiService; _notyf = notyfService; }
        private readonly INotyfService _notyf;
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var requestBody = new UserRequestModelv2FE
            {
                FirstName = AccountRegister.FirstName,
                LastName = AccountRegister.LastName,
                Email = AccountRegister.Email,
                PhoneNumber = AccountRegister.PhoneNumber,
                Address = AccountRegister.Address,
                Password = AccountRegister.ConfirmPassword,
                RoleId = Guid.Parse(Request.Form["RoleAccount"]),
                Status = "ACTIVE"
            };
            var reponse = await _apiService.PostAsync<UserRequestModelv2FE, BaseResponseModelFE<UserResponseModelFE>>("users", requestBody); //"`endpoint-url` cho phù hợp"
            if (reponse.Code == 201)
            {
                _notyf.Success("SignUp Success");
                return RedirectToPage("/login");
            }
            else
            {
                TempData["Message"] = "Wrong email or password";
            }
            return Page();

        }
    }
}
