using System.ComponentModel.DataAnnotations;

namespace PetRescueFE.Pages.Model.Shelters;

public class ShelterDTO
{

}

public class ShelterRequestModel
{
    [Required(ErrorMessage = "Shelter Name is required")]
    [StringLength(100, ErrorMessage = "Shelter Name cannot be longer than 100 characters")]
    public string ShelterName { get; set; } = null!;

    [Required(ErrorMessage = "Shelter Address is required")]
    [StringLength(200, ErrorMessage = "Shelter Address cannot be longer than 200 characters")]
    public string ShelterAddress { get; set; } = null!;

    [Required(ErrorMessage = "Shelter Phone Number is required")]
    [Phone(ErrorMessage = "Invalid phone number format")]
    public string ShelterPhoneNumber { get; set; } = null!;

    [Range(0, double.MaxValue, ErrorMessage = "Balance cannot be negative")]
    public decimal Balance { get; set; }

    [Required(ErrorMessage = "User Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string UserEmail { get; set; }
}

public class ShelterRequestModelForUpdate
{
    [Required(ErrorMessage = "Shelter Name is required")]
    [StringLength(100, ErrorMessage = "Shelter Name cannot be longer than 100 characters")]
    public string ShelterName { get; set; } = null!;

    [Required(ErrorMessage = "Shelter Address is required")]
    [StringLength(200, ErrorMessage = "Shelter Address cannot be longer than 200 characters")]
    public string ShelterAddress { get; set; } = null!;

    [Required(ErrorMessage = "Shelter Phone Number is required")]
    [Phone(ErrorMessage = "Invalid phone number format")]
    public string ShelterPhoneNumber { get; set; } = null!;

    [Range(0, double.MaxValue, ErrorMessage = "Balance cannot be negative")]
    public decimal Balance { get; set; }

    [Required(ErrorMessage = "User Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string UserEmail { get; set; }
}

public class ShelterResponseModel
{
    public Guid ShelterId { get; set; }
    public string ShelterName { get; set; } = null!;
    public string ShelterAddress { get; set; } = null!;
    public string ShelterPhoneNumber { get; set; } = null!;
    public decimal Balance { get; set; }
    public string UserEmail { get; set; }
}