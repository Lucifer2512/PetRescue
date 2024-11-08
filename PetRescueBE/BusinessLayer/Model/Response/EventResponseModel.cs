namespace BusinessLayer.Model.Response;

public class EventResponseModel
{
    public Guid? EventId { get; set; }
    public string? ImageUrl { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime? StartDateTime { get; set; }
    public DateTime? EndDateTime { get; set; }
    public string? Location { get; set; }
    public string? EventType { get; set; }
    public string? Goal { get; set; }
    public string? Status { get; set; }

    public virtual ShelterResponseModel? Shelter { get; set; }
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