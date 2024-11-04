using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PetRescueFE.Pages.Model.Events;

public class EventDTO
{

}

public class EventResponseModel
{
    public Guid? EventId { get; set; }
    public Guid? ShelterId { get; set; }
    public string? ImageUrl { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime? StartDateTime { get; set; }
    public DateTime? EndDateTime { get; set; }
    public string? Location { get; set; }
    public string? EventType { get; set; }
    public string? Goal { get; set; }
    public string? Status { get; set; }

    public virtual Shelter4EventResponse? Shelter { get; set; }
    public virtual ICollection<Donation4EventResponse>? Donations { get; set; } = null;
}

public class Donation4EventResponse
{
    public Guid DonationId { get; set; }
    public Guid UserId { get; set; }
    public string? Amount { get; set; }
    public DateTime DonationDate { get; set; }
    public string? PaymentMethod { get; set; }
    public string? Notes { get; set; }
    public string Status { get; set; } = null!;
}

public class Shelter4EventResponse
{
    public Guid ShelterId { get; set; }
    public string ShelterName { get; set; } = null!;
    public string ShelterAddress { get; set; } = null!;
    public string ShelterPhoneNumber { get; set; } = null!;
    public decimal Balance { get; set; }
    public string UserEmail { get; set; }
}

public class EventRequestModel4Create
{
    [Required]
    public string? Name { get; set; }

    public string? ImageUrl { get; set; }

    public string? Description { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
    public DateTime StartDateTime { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
    public DateTime EndDateTime { get; set; }

    [Required]
    public string? Location { get; set; }

    [Required]
    public string? EventType { get; set; }

    public string? Goal { get; set; }

    [DefaultValue("ACTIVE")]
    public string? Status { get; set; }

    [Required]
    public Guid? ShelterId { get; set; }
}

public class EventRequestModel4Update
{
    [Required]
    public string? Name { get; set; }

    public string? ImageUrl { get; set; }

    public string? Description { get; set; }

    [Required]
    public DateTime? StartDateTime { get; set; }

    [Required]
    public DateTime? EndDateTime { get; set; }

    [Required]
    public string? Location { get; set; }

    [Required]
    public string? EventType { get; set; }

    public string? Goal { get; set; }

    [Required]
    public string? Status { get; set; }
}
