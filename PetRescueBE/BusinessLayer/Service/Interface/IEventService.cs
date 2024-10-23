using BusinessLayer.Model.Request;
using BusinessLayer.Model.Response;
using BusinessLayer.Models.Response;

namespace BusinessLayer.Service.Interface;

public interface IEventService
{
    public Task<BaseResponseModel<IEnumerable<EventResponseModel>>> GetsAsync();
    public Task<BaseResponseModel<EventResponseModel>> GetAsync(Guid id);
    public Task<BaseResponseModel<EventResponseModel>> CreateAsync(EventRequestModel4Create request);
    public Task<BaseResponseModel<EventResponseModel>> UpdateAsync(Guid id, EventRequestModel4Update request);
    
}