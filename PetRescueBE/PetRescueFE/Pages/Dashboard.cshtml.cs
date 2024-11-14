using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetRescueFE.Services;

namespace PetRescueFE.Pages;

public class DashboardModel : PageModel
{
    private readonly DashboardService _dashboardService;
    private readonly WebService _webService;

    public int? TotalUsers { get; set; }
    public int? ActiveUsers { get; set; }
    public int? InactiveUsers { get; set; }

    public int? TotalEvents { get; set; }
    public int? ActiveEvents { get; set; }
    public int? InactiveEvents { get; set; }

    public int? TotalPets { get; set; }
    public int? ActivePets { get; set; }
    public int? InactivePets { get; set; }

    public int? TotalAdoptions { get; set; }
    public int? ActiveAdoptions { get; set; }
    public int? InactiveAdoptions { get; set; }

    public int? TotalShelters { get; set; }
    public int? ActiveShelters { get; set; }
    public int? InactiveShelters { get; set; }

    public int? TotalDonations { get; set; }
    public int? TotalDonationAmount { get; set; }

    public DashboardModel(DashboardService dashboardService, WebService webService)
    {
        _dashboardService = dashboardService;
        _webService = webService;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        if (!_webService.IsAccessible() && !_webService.IsAdmin())
        {
            return RedirectToPage("/Login");
        }
        
        var events = await _dashboardService.GetEvents();
        (TotalEvents, ActiveEvents, InactiveEvents) = await _dashboardService.TotalActiveNInactive(events);

        var pets = await _dashboardService.GetPets();
        (TotalPets, ActivePets, InactivePets) = await _dashboardService.TotalActiveNInactive(pets);

        var adoptions = await _dashboardService.GetAdoptions();
        (TotalAdoptions, ActiveAdoptions, InactiveAdoptions) = await _dashboardService.TotalActiveNInactive(adoptions);

        var shelters = await _dashboardService.GetShelters();
        (TotalShelters, ActiveShelters, InactiveShelters) = await _dashboardService.TotalActiveNInactive(shelters);

        (TotalDonations, TotalDonationAmount, _) = await _dashboardService.TotalAmountDonations();

        return Page();
    }
}