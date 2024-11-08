using System.IdentityModel.Tokens.Jwt;

namespace PetRescueFE.Pages.Events;

public class EventGlobalUtility
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public EventGlobalUtility(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? GetUserMail()
    {
        var token = _httpContextAccessor.HttpContext?.Session.GetString("JWTToken");
        if (string.IsNullOrEmpty(token))
            return null;

        try
        {
            var handler = new JwtSecurityTokenHandler();
            var tokenRead = handler.ReadToken(token) as JwtSecurityToken;
            return tokenRead?.Claims.FirstOrDefault(claim => claim.Type == "email")?.Value;
        }
        catch
        {
            return null;
        }
    }
}