using AutoMapper;
using BusinessLayer.Model.Request;
using BusinessLayer.Model.Response;
using BusinessLayer.Service.Interface;
using DataAccessLayer.Entity;
using DataAccessLayer.UnitOfWork.Interface;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Service.Implement;

public class EventService : IEventService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public EventService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    
    public async Task<BaseResponseModel<PaginatedList<EventResponseModel>>> GetsPagedAsync(int index, int size)
    {
        var eventRepo = _unitOfWork.Repository<Event>();
        var events = await eventRepo.DbContext.Set<Event>().OrderBy(e => e.StartDateTime).Skip((index - 1) * size)
            .Take(size).ToListAsync();
        var count = await eventRepo.DbContext.Set<Event>().CountAsync();
        var total = (int)Math.Ceiling(count / (double)size);

        var eventResponse = _mapper.Map<List<EventResponseModel>>(events);
        return new BaseResponseModel<PaginatedList<EventResponseModel>>
        {
            Code = 200,
            Message = "Success",
            Data = new PaginatedList<EventResponseModel>
            {
                Items = eventResponse,
                PageIndex = index,
                TotalPages = total
            }
        };
    }

    
    public async Task<BaseResponseModel<IEnumerable<EventResponseModel>>> GetsAsync()
    {
        var eventRepo = _unitOfWork.Repository<Event>();
        
        var events = await eventRepo.DbContext.Set<Event>()
            .Include(e => e.Shelter)
            .Include(e => e.Donations)
            .ToListAsync();
        
        var eventResponse = _mapper.Map<List<EventResponseModel>>(events);

        if (events.Count() == 0)
        {
            return new BaseResponseModel<IEnumerable<EventResponseModel>>
            {
                Code = 404,
                Message = "No event found, return null as no data",
                Data = eventResponse
            };
        }

        return new BaseResponseModel<IEnumerable<EventResponseModel>>
        {
            Code = 200,
            Message = "Success",
            Data = eventResponse
        };
    }

    
    public async Task<BaseResponseModel<EventResponseModel>> GetAsync(Guid id)
    {
        var eventRepo = _unitOfWork.Repository<Event>();
        var isEvent = await eventRepo.FindAsync(id);
        if (isEvent is null)
        {
            return new BaseResponseModel<EventResponseModel>
            {
                Code = 404,
                Message = "Event not found",
                Data = null
            };
        }

        return new BaseResponseModel<EventResponseModel>
        {
            Code = 200,
            Message = "Success",
            Data = _mapper.Map<EventResponseModel>(isEvent)
        };
    }

    
    public async Task<BaseResponseModel<string>> CreateAsync(EventRequestModel4Create request)
    {
        var eventRepo = _unitOfWork.Repository<Event>();
        var newEvent = _mapper.Map<Event>(request);
        newEvent.EventId = Guid.NewGuid();
        try
        {
            await eventRepo.InsertAsync(newEvent, saveChanges: false);

            var isSaved = await _unitOfWork.SaveChangesAsync();
            if (isSaved == 0)
            {
                return new BaseResponseModel<string>
                {
                    Code = 501,
                    Message = "Failed to save event",
                    Data = null
                };
            }

            return new BaseResponseModel<string>
            {
                Code = 201,
                Message = "Created",
                Data = /* _mapper.Map<EventResponseModel>(newEvent)*/ null
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new BaseResponseModel<string>
            {
                Code = 500,
                Message = "Failed to save event, " + e.InnerException,
                Data = null
            };
        }
    }

    public async Task<BaseResponseModel<EventResponseModel>> UpdateAsync(Guid id, EventRequestModel4Update request)
    {
        var eventRepo = _unitOfWork.Repository<Event>();
        var isEvent = await eventRepo.FindAsync(id);
        if (isEvent is null)
        {
            return new BaseResponseModel<EventResponseModel>
            {
                Code = 404,
                Message = "Event not found",
                Data = null
            };
        }

        _mapper.Map(request, isEvent);

        try
        {
            await eventRepo.UpdateAsync(isEvent);
            var isSaved = await _unitOfWork.SaveChangesAsync();
            if (isSaved == 0)
            {
                return new BaseResponseModel<EventResponseModel>
                {
                    Code = 501,
                    Message = "Failed to save event",
                    Data = null
                };
            }

            return new BaseResponseModel<EventResponseModel>
            {
                Code = 201,
                Message = "Updated",
                Data = null
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new BaseResponseModel<EventResponseModel>
            {
                Code = 500,
                Message = "Failed to save event, " + e.InnerException,
                Data = null
            };
        }
    }
}