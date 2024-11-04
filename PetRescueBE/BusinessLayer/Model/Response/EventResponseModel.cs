using BusinessLayer.Model.Enums;
using DataAccessLayer.Entity;

namespace BusinessLayer.Model.Response;

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
    public Status? Status { get; set; }

    public virtual Shelter? Shelter { get; set; }
    public virtual ICollection<Donation>? Donations { get; set; } = null;
}