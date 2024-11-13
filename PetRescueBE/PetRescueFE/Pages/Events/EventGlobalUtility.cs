using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

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
    public string? GetUserId()
    {
        var id = _httpContextAccessor.HttpContext?.Session.GetString("UserId");
        if (string.IsNullOrEmpty(id))
            return null;
        return id;
    }
    public string? GetUserRole()
    {
        var role = _httpContextAccessor.HttpContext?.Session.GetString("Role");
        if (string.IsNullOrEmpty(role))
            return null;
        return role;
    }

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
}
public class DateRangeAttribute : ValidationAttribute
{
    private readonly DateTime _minDate;
    private readonly DateTime _maxDate;

    public DateRangeAttribute(string minDate, string maxDate)
    {
        _minDate = DateTime.Parse(minDate);
        _maxDate = DateTime.Parse(maxDate);
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is DateTime dateTime)
        {
            if (dateTime < _minDate || dateTime > _maxDate)
            {
                return new ValidationResult($"Date must be between {_minDate.ToShortDateString()} and {_maxDate.ToShortDateString()}.");
            }
        }
        return ValidationResult.Success;
    }
}
public class EndDateAfterStartDateAttribute : ValidationAttribute
{
    private readonly string _startDatePropertyName;

    public EndDateAfterStartDateAttribute(string startDatePropertyName)
    {
        _startDatePropertyName = startDatePropertyName;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var startDateProperty = validationContext.ObjectType.GetProperty(_startDatePropertyName);
        if (startDateProperty == null)
        {
            return new ValidationResult($"Unknown property: {_startDatePropertyName}");
        }

        var startDateValue = startDateProperty.GetValue(validationContext.ObjectInstance, null);
        if (startDateValue == null)
        {
            return new ValidationResult("Start date is required.");
        }

        if (value is DateTime endDate && startDateValue is DateTime startDate)
        {
            if (endDate <= startDate)
            {
                return new ValidationResult("End date must be after the start date.");
            }
        }

        return ValidationResult.Success;
    }
}