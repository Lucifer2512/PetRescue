using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using PetRescueFE.Pages.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PetRescueFE.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public AccountViewModel AccountViewModel { get; set; }
        private ApiService _apiService;
        public LoginModel(ApiService apiService)
        {
            _apiService = apiService;

        }
        

        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var reponse = await _apiService.PostAsync<AccountViewModel, BaseResponseModelFE<LoginResponseModelFE>>("users/login", AccountViewModel); //"`endpoint-url` cho phù hợp"
            if (reponse.Code == 200)
            {
                var userReponse = reponse.Data;
                if (userReponse != null)
                {
                    if (userReponse.Token == null)
                    {                        
                        return Page();
                    }
                    HttpContext.Session.SetString("JWTToken", userReponse.Token.Token.ToString());
                    var handler = new JwtSecurityTokenHandler();
                    var tokenS = handler.ReadToken(userReponse.Token.Token.ToString()) as JwtSecurityToken;
                    var role = tokenS.Claims.First(claim => claim.Type == "role")?.Value;
                    var id = tokenS.Claims.First(claim => claim.Type == "nameid")?.Value;
                    HttpContext.Session.SetString("Role", role);
                    HttpContext.Session.SetString("UserId", id);
                    //HttpContext.Session.SetString("AccountId", id);
                   /* var username = tokenS.Claims.First(claim => claim.Type == ClaimTypes.UserData).Value;
                    HttpContext.Session.SetString("Username", username);*/
                    switch (role)
                    {
                        case "d290f1ee-6c54-4b01-90e6-d701748f0851":      //Administrator
                            return RedirectToPage("/Index");
                        case "f3c8d4e5-6b7a-4c9d-8e2f-0a1b2c3d4e5f":      //ShelterOwner
                            return RedirectToPage("/Index");
                        case "e7b8f3d2-4a2f-4c3b-8f4d-9c5d8a3e1b2c":     //User
                            return RedirectToPage("/UserPage/Index");                      
                        default:
                            return Page();
                    }
                }
                


            }
            else
            {
                TempData["Message"] = "Wrong email or password";
            }
            return Page();

        }
    }
}
