using BusinessLayer.Model.Request;
using BusinessLayer.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace PetRescueAPI.Controllers;
[ApiController]
[Route("api/events")]
public class EventController : ControllerBase
{
    private readonly IEventService _eventService;

    public EventController(IEventService eventService)
    {
        _eventService = eventService;
    }

    [HttpGet]
    public async Task<ActionResult> Gets()
    {
        var response = await _eventService.GetsAsync();
        return StatusCode((int)response.Code!, response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> Get(Guid id)
    {
        var response = await _eventService.GetAsync(id);
        return StatusCode((int)response.Code!, response);
    }
    
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] EventRequestModel4Create request)
    {
        var response = await _eventService.CreateAsync(request);
        return StatusCode((int)response.Code!, response);
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, [FromBody] EventRequestModel4Update request)
    {
        var response = await _eventService.UpdateAsync(id, request);
        return StatusCode((int)response.Code!, response);
    }
}