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
    public class IndexModel : PageModel
    {
        private readonly ApiService _apiService;

        public IndexModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        public ICollection<PetResponseModelFE> Pets { get; set; } = new List<PetResponseModelFE>();
        public bool IsAdmin { get; private set; }
        public string SearchTerm { get; set; }
        public string ErrorMessage { get; set; }
        public async Task OnGetAsync(string searchTerm)
        {
            var token = HttpContext.Session.GetString("JWTToken");
            var role = HttpContext.Session.GetString("Role");
            string apiUrl = string.Empty;
            if (role == "d290f1ee-6c54-4b01-90e6-d701748f0851" || role == "f3C8d4e5-6b7a-4c9d-8e2f-0a1b2c3d4e5f")
            {
                IsAdmin = true; 
                apiUrl = $"https://localhost:7297/api/pet/search?searchTerm={searchTerm}";
            }
            else
            {
                apiUrl = $"https://localhost:7297/api/pet/user-search?searchTerm={searchTerm}";
            }
            var response = await _apiService.GetAsync<BaseResponseModelFE<ICollection<PetResponseModelFE>>>(apiUrl);
            if (response != null && response.Code == 200)
            {
                Pets = response.Data;
            }
            else
            {
                ErrorMessage = response.Message;
            }

        }
    }
}
