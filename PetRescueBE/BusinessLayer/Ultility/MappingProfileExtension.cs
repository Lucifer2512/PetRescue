using AutoMapper;
using BusinessLayer.Model.Request;
using BusinessLayer.Model.Response;
using BusinessLayer.Models.Request;
using BusinessLayer.Models.Response;
using DataAccessLayer.Entity;

namespace BusinessLayer.Utilities
{
    public class MappingProfileExtension : Profile
    {
        public MappingProfileExtension()
        {
            CreateMap<User, UserResponseModel>();
            CreateMap<UserRequestModel, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => Helper.HashPassword(src.Password)));
            CreateMap<UserRequestModelForUpdate, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => Helper.HashPassword(src.Password)));
            CreateMap<Shelter, ShelterResponseModel>();
            CreateMap<ShelterRequestModel, Shelter>();
            CreateMap<ShelterRequestModelForUpdate, Shelter>();
            CreateMap<DonationRequestModel,Donation>();
            CreateMap<Donation, DonationReponseModel>();
            CreateMap<AdoptionApplicationRequestModel, AdoptionApplication>();
            CreateMap<AdoptionApplication, AdoptionApplicationResponseModel>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.PetName, opt => opt.MapFrom(src => src.Pet.Name));
            #region Event families

            CreateMap<Event, EventResponseModel>();
            CreateMap<EventRequestModel4Create, Event>();
            CreateMap<EventRequestModel4Update, Event>();
            CreateMap<Event, EventResponseModel>();

            #endregion
        }
    }
}
