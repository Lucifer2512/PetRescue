using BusinessLayer.Model.Enums;
using System.ComponentModel;

namespace BusinessLayer.Model.Request;

public class EventRequestModel
{

}

public class EventRequestModel4Create
{
    public string? ImageUrl { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime? StartDateTime { get; set; }
    public DateTime? EndDateTime { get; set; }
    public string? Location { get; set; }
    public string? EventType { get; set; }
    public string? Goal { get; set; }
    [DefaultValue("ACTIVE")]
    public Status? Status { get; set; }
    public Guid? ShelterId { get; set; }

    /*
    public virtual ICollection<Donation>? Donations { get; set; } = null;*/
}

public class EventRequestModel4Update
{
    public string? ImageUrl { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime? StartDateTime { get; set; }
    public DateTime? EndDateTime { get; set; }
    public string? Location { get; set; }
    public string? EventType { get; set; }
    public string? Goal { get; set; }
    public Status? Status { get; set; }

}