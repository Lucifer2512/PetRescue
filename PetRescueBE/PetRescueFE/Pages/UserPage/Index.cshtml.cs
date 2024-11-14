using DataAccessLayer.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetRescueFE.Pages.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PetRescueFE.Pages.UserPage
{
    public class IndexModel : PageModel
    {
        private readonly ApiService _apiService;

        public IndexModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        public IList<UserResponseModelFE> Users { get; set; } = default!;
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? pageIndex)
        {
            var role = HttpContext.Session.GetString("Role");

            if (role != "d290f1ee-6c54-4b01-90e6-d701748f0851")
            {
                return RedirectToPage("/AuthorizationError");
            }

            CurrentPage = pageIndex ?? 1;

            var apiUrl = $"https://localhost:7297/api/users/paging?index={CurrentPage}&size=5";
            var response = await _apiService.GetAsync<BaseResponseModelFE<PaginatedList<UserResponseModelFE>>>(apiUrl);

            if (response.Data != null)
            {
                Users = response.Data.Items;
                TotalPages = response.Data.TotalPages;
                HasPreviousPage = response.Data.HasPreviousPage;
                HasNextPage = response.Data.HasNextPage;
            }

            return Page();
        }
    }
}
