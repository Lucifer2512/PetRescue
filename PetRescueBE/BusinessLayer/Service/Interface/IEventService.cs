using BusinessLayer.Model.Request;
using BusinessLayer.Model.Response;

namespace BusinessLayer.Service.Interface;

public interface IEventService
{
    /// <summary>
    /// can be vulnerable,
    /// </summary>
    /// <param name="index">int</param>
    /// <param name="size">int</param>
    /// <returns>BaseResponseModel contain PaginatedList contain EventResponseModel</returns>
    public Task<BaseResponseModel<PaginatedList<EventResponseModel>>> GetsPagedAsync(int index, int size);

    /// <summary>
    /// get all events, no validate like is shelter or not
    /// </summary>
    /// <returns>BaseResponseModel with IEnumerable contain EventResponseModel</returns>
    public Task<BaseResponseModel<IEnumerable<EventResponseModel>>> GetsAsync();

    /// <summary>
    /// get event by event id
    /// </summary>
    /// <param name="id">Guid</param>
    /// <returns>BaseResponseModel with EventResponseModel</returns>
    public Task<BaseResponseModel<EventResponseModel>> GetAsync(Guid id);

    /// <summary>
    /// create a event
    /// </summary>
    /// <param name="request">EventRequestModel4Create</param>
    /// <returns>BaseResponseModel with EventResponseModel</returns>
    public Task<BaseResponseModel<string>> CreateAsync(EventRequestModel4Create request);
    public Task<BaseResponseModel<EventResponseModel>> UpdateAsync(Guid id, EventRequestModel4Update request);

}