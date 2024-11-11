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

        public async Task OnGetAsync()
        {
           
            var response = await _apiService.GetAsync<BaseResponseModelFE<ICollection<PetResponseModelFE>>>("https://localhost:7297/api/pet");
            if (response != null && response.Code == 200)
            {
                Pets = response.Data;
            }
            else
            {
                // Xử lý nếu không thể lấy được danh sách pet, có thể hiển thị thông báo lỗi
            }
        }
    }
}
