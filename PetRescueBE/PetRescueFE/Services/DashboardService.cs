using PetRescueFE.Pages.Model;
using PetRescueFE.Pages.Model.Events;
using PetRescueFE.Pages.Model.Shelters;

namespace PetRescueFE.Services;

public class DashboardService
{
    private readonly ApiService _apiService;
    
    public DashboardService(ApiService apiService)
    {
        _apiService = apiService;
    }
    
    public async Task<(int?, int?, int?)> TotalAmountDonations()
    {
        var donations = await GetDonations();
        var total = donations?.Count ?? 0;
        var amount = (int)(donations?.Sum(d => d.Amount) ?? 0);
        return (total, amount, null);
    }

    public async Task<(int?, int?, int?)> TotalActiveNInactive<T>(List<T> request) where T : class
    {
        if (request.GetType().GetProperty("Status") == null)
        {
            return (TotalAndActive(request).Item1, null, null);
        }

        var (total, active) = TotalAndActive(request);

        var inactive = total - active;
        return (total, active, inactive);
    }

    public async Task<List<EventResponseModel?>?> GetEvents()
    {
        var response =
            await _apiService.GetAsync<BaseResponseModelFE<List<EventResponseModel>>>(UrlProfile.BASE_URL_S +
                UrlProfile.GETS_EVENT);
        return response.Data;
    }

    public async Task<List<ShelterResponseModel>?> GetShelters()
    {
        var response =
            await _apiService.GetAsync<BaseResponseModelFE<List<ShelterResponseModel>>>(UrlProfile.BASE_URL_S +
                UrlProfile.GETS_SHELTER);
        return response.Data;
    }

    public async Task<List<DonationReponseModel>?> GetDonations()
    {
        var response =
            await _apiService.GetAsync<BaseResponseModelFE<List<DonationReponseModel>>>(UrlProfile.BASE_URL_S +
                UrlProfile.GETS_DONATION);
        return response.Data;
    }

    public async Task<List<AdoptionApplicationResponseModel>?> GetAdoptions()
    {
        var response =
            await _apiService.GetAsync<BaseResponseModelFE<List<AdoptionApplicationResponseModel>>>(
                UrlProfile.BASE_URL_S +
                UrlProfile.GETS_ADOPTION);
        return response.Data;
    }

    public async Task<List<PetResponseModelFE>> GetPets()
    {
        var response =
            await _apiService.GetAsync<BaseResponseModelFE<List<PetResponseModelFE>>>(UrlProfile.BASE_URL_S +
                UrlProfile.GETS_PET);
        return response.Data;
    }

    /// <summary>
    /// do get total number of list and active of that list,
    /// some time, it can be total number and total amount
    /// </summary>
    /// <param name="request"></param>
    /// <typeparam name="T">List of T</typeparam>
    /// <returns>total, active - tupple int?</returns>
    public (int?, int?) TotalAndActive<T>(List<T> request) where T : class
    {
        var total = request.Count;
        if (request.GetType().GetProperty("Status") == null)
        {
            return (total, null);
        }

        var active = request.Count(x =>
            x.GetType().GetProperty("Status")?.GetValue(x).ToString() == Status.ACTIVE.ToString());
        return (total, active);
    }
}