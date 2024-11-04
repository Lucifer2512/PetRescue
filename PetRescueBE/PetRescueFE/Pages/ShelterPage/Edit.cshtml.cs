using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Context;
using DataAccessLayer.Entity;
using System.ComponentModel.DataAnnotations;
using PetRescueFE.Pages.Model.Shelters;
using PetRescueFE.Pages.Model;

namespace PetRescueFE.Pages.ShelterPage
{
    public class EditModel : PageModel
    {
        private readonly ApiService _apiService;

        public EditModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        [BindProperty]
        public ShelterResponseModel Shelter { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apiUrl = $"https://localhost:7297/api/shelter/{id}";
            var response = await _apiService.GetAsync<BaseResponseModelFE<ShelterResponseModel>>(apiUrl);

            if (response.Data == null)
            {
                return NotFound();
            }

            Shelter = response.Data;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var apiUrl = $"https://localhost:7297/api/shelter/{Shelter.ShelterId}";
            var shelterRequest = new ShelterRequestModelForUpdate
            {
                ShelterName = Shelter.ShelterName,
                ShelterAddress = Shelter.ShelterAddress,
                Balance = Shelter.Balance,
                ShelterPhoneNumber = Shelter.ShelterPhoneNumber,
                UserEmail = Shelter.UserEmail
            };

            var validationContext = new ValidationContext(shelterRequest);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(shelterRequest, validationContext, validationResults, true);

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
                var response = await _apiService.PutAsync<ShelterRequestModelForUpdate, BaseResponseModelFE<ShelterResponseModel>>(apiUrl, shelterRequest);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Failed to update shelter.");
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
 }
