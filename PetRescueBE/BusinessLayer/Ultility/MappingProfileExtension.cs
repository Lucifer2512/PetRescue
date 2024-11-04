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
            CreateMap<User, UserResponseModel>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.RoleName));
            CreateMap<UserRequestModel, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => Helper.HashPassword(src.Password)));
            CreateMap<UserRequestModelForUpdate, User>();
            CreateMap<Shelter, ShelterResponseModel>()
                .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.Users.Email));
            CreateMap<ShelterRequestModel, Shelter>();
            CreateMap<ShelterRequestModelForUpdate, Shelter>();
            CreateMap<DonationRequestModel,Donation>();
            CreateMap<Donation, DonationReponseModel>();
            CreateMap<AdoptionApplicationRequestModel, AdoptionApplication>();
            CreateMap<AdoptionApplication, AdoptionApplicationResponseModel>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.PetName, opt => opt.MapFrom(src => src.Pet.Name));
           
            #region Event families
            
            CreateMap<Event, EventResponseModel>().ReverseMap();
            CreateMap<EventRequestModel4Create, Event>().ReverseMap();
            CreateMap<EventRequestModel4Update, Event>().ReverseMap();
            CreateMap<Event, EventResponseModel>().ReverseMap();

            #endregion
        }
    }
}
