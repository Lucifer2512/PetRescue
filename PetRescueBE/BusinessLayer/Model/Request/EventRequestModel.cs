using BusinessLayer.Model.Enums;
using System.ComponentModel;

namespace BusinessLayer.Model.Request;

public class EventRequestModel
{

}

public class EventRequestModel4Create
{
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
    public byte[]? Image { get; set; }
}

public class EventRequestModel4Update
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime? StartDateTime { get; set; }
    public DateTime? EndDateTime { get; set; }
    public string? Location { get; set; }
    public string? EventType { get; set; }
    public string? Goal { get; set; }
    public Status? Status { get; set; }
    public byte[]? Image { get; set; }
}