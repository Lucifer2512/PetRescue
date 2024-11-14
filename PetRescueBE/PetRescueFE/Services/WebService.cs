using Microsoft.IdentityModel.Tokens;
using PetRescueFE.Pages.Events;

namespace PetRescueFE.Services;
/// <summary>
///this class use to check global variable or function as role, id, etc
/// </summary>
public class WebService
{
    private readonly IHttpContextAccessor _context;


    public WebService( IHttpContextAccessor httpContextAccessor)
    {
        _context = httpContextAccessor;
    }

    public string? GetRole()
    {
        var role = _context.HttpContext.Session.GetString("Role");
        if (string.IsNullOrEmpty(role))
        {
            return null;
        }
        return role;
    }
    
    /// <summary>
    /// this function default return false, true if admin, shelter owner or user
    /// </summary>
    /// <param name="role">string</param>
    /// <returns>boolean</returns>
    public bool IsEditable(string role)
    {
        bool canEdit = false;
        if (role.IsNullOrEmpty())
        {
            return false;
        }
        canEdit = role == Role4Event.ADMIN || // Admin
                  role == Role4Event.SHELTER_OWNER;    // ShelterOwner
        canEdit = role == Role4Event.USER; // User

        return canEdit;
    }
    
    /// <summary>
    /// this function default return false, true if admin, shelter owner or user
    /// </summary>
    /// <returns>boolean</returns>
    public bool IsAccessible()
    {
        var role = GetRole();
        bool canAccess = false;
        if (role.IsNullOrEmpty())
        {
            return false;
        }
        canAccess = role == Role4Event.ADMIN || // Admin
                    role == Role4Event.SHELTER_OWNER || // ShelterOwner
                    role == Role4Event.USER; // User

        return canAccess;
    }
    
    /// <summary>
    /// this function default return false, true if admin
    /// </summary>
    /// <returns>boolean</returns>
    public bool IsAdmin()
    {
        var role = GetRole();
        if (role.IsNullOrEmpty())
        {
            return false;
        }
        return role == Role4Event.ADMIN;
    }
    
    public string? GetUserId()
    {
        var id = _context.HttpContext.Session.GetString("UserId");
        if (string.IsNullOrEmpty(id))
            return null;
        return id;
    }
}