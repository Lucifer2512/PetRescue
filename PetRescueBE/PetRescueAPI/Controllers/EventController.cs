using System.ComponentModel.DataAnnotations;
using BusinessLayer.Model.Request;
using BusinessLayer.Model.Response;
using BusinessLayer.Models.Response;
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

    /// <summary>
    /// get all events
    /// </summary>
    /// <returns>[event]</returns>
    /// <remarks>
    /// </remarks>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet]
    public async Task<ActionResult> Gets()
    {
        var response = await _eventService.GetsAsync();
        return StatusCode((int)response.Code!, response);
    }
    
    [HttpGet("p/")]
    public async Task<ActionResult<BaseResponseModel<PaginatedList<EventResponseModel>>>> GetsPagedAsync([FromQuery] EventParameter parameter)
    {
        var response = await _eventService.GetsPagedAsync(parameter.Index, parameter.Size);
        return StatusCode((int)response.Code!, response);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id}")]
    public async Task<ActionResult<BaseResponseModel<string>>> Get(Guid id)
    {
        var response = await _eventService.GetAsync(id);
        return StatusCode((int)response.Code!, response);
    }
    
    /// <summary>
    /// use many field to do, required shelter id, event name, event description, event date, event location
    /// </summary>
    /// <param name="request"></param>
    /// <returns>201,500</returns>
    /// <remarks>
    /// </remarks>
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status501NotImplemented)]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
    [HttpPost]
    public async Task<ActionResult<BaseResponseModel<EventResponseModel?>>> Create([FromBody] EventRequestModel4Create request)
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
public record EventParameter([Range(1, int.MaxValue)]int Index, int Size);