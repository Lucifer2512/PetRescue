using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http;
using tutor_server.WebPage.ViewModels;

using System.Globalization;
using System;
using PetRescueFE.Pages.Model;
using PetRescueFE;
using DataAccessLayer.Entity;
using PetRescueFE.Pages;
using Microsoft.AspNetCore.SignalR;
using PetRescueFE.SignalRealtime;

namespace tutor_server.WebPage.Pages
{
    public class transactionModel : PageModel
    {
        public string AccountId { get; private set; }
        private HttpClient _httpClient;
        public List<DonationReponseModel> TransactionList { get;set; }
        private readonly ApiService _apiService;
        public PaginatedListFe<DonationReponseModel> transaction { get; set; }
        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;
        [BindProperty]

        public string? Status { get; set; } = null;
        [BindProperty]
        public string? Types { get; set; } = null;
        [BindProperty]
        public bool? Sorting { get; set; } = true;
        [BindProperty]
        public int? Amount { get; set; } = null;
       

        public transactionModel(ApiService apiService)
        {
            _apiService = apiService;
            
        }
        public async Task<IActionResult> OnGet() => await LoadData();
        
           
                                             
        
        /*public async Task<IActionResult> OnPostSearch() => await LoadData();*/



        public async Task<IActionResult> LoadData()
        {
            
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var UserId = HttpContext.Session.GetString("UserId");
            var reponse = await _apiService.GetAsync<BaseResponseModelFE<List<DonationReponseModel>>>($"donation/getdonationbyuserid/{UserId}"); //"`endpoint-url` cho
            

           
                if (reponse.Code == 200)
                {

                TransactionList = reponse.Data;
                int pageSize = 10;
                    transaction = PaginatedListFe<DonationReponseModel>.Create(TransactionList.AsQueryable(), PageIndex, pageSize);
                return Page();
                }
                else
                {
                    return Page();
                }
            


        }
       
    }
}
