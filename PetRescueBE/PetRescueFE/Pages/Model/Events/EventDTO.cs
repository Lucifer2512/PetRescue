namespace Pages.Model.Events;

public class EventDTO
{
    
}

public class EventResponseModel
{
    /*public Guid? EventId { get; set; }
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
    public virtual ICollection<Donation>? Donations { get; set; } = null;*/
}

public class Shelter4EventResponse
{
    public Guid ShelterId { get; set; }
    public string ShelterName { get; set; } = null!;
    public string ShelterAddress { get; set; } = null!;
    public string ShelterPhoneNumber { get; set; } = null!;
    public decimal Balance { get; set; }
    public Guid UsersId { get; set; }

}