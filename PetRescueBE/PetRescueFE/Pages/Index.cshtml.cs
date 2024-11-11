using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetRescueFE.Pages.Model;

namespace PetRescueFE.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApiService _apiService;

        public IndexModel(ILogger<IndexModel> logger, ApiService apiService)
        {
            _logger = logger;
            _apiService = apiService;
        }

        public async Task OnGet()
        {
            var userId = HttpContext.Session.GetString("UserId");

            var apiUrl = $"https://localhost:7297/api/users/{userId}";
            var response = await _apiService.GetAsync<BaseResponseModelFE<UserResponseModelFE>>(apiUrl);

            if (response.Data != null)
            {
                HttpContext.Session.SetString("UserImageBase64", response.Data.ImageData);
            }

        }
    }
}
