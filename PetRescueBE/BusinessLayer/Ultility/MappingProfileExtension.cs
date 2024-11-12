using AutoMapper;
using BusinessLayer.Model.Request;
using BusinessLayer.Model.Response;
using DataAccessLayer.Entity;

namespace BusinessLayer.Ultility
{
    public class MappingProfileExtension : Profile
    {
        public MappingProfileExtension()
        {
            CreateMap<User, UserResponseModel>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.RoleName));
            CreateMap<UserRequestModel, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => Helper.HashPassword(src.Password)));
            CreateMap<UserRequestModelForUpdate, User>()
                .ForMember(dest => dest.Image, opt =>
                {
                    opt.MapFrom(src => src.Image);
                    opt.Condition((src, dest) => src.Image != null);
                });
            CreateMap<Shelter, ShelterResponseModel>()
                .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.Users.Email));
            CreateMap<ShelterRequestModel, Shelter>();
            CreateMap<ShelterRequestModelForUpdate, Shelter>()
                .ForMember(dest => dest.Image, opt =>
                {
                    opt.MapFrom(src => src.Image);
                    opt.Condition((src, dest) => src.Image != null);
                });
            CreateMap<DonationRequestModel, Donation>();
            CreateMap<Donation, DonationReponseModel>();
            CreateMap<AdoptionApplicationRequestModel, AdoptionApplication>();
            CreateMap<AdoptionApplicationRequestModelForUpdate, AdoptionApplication>();
            CreateMap<AdoptionApplication, AdoptionApplicationResponseModel>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.PetName, opt => opt.MapFrom(src => src.Pet.Name));
            CreateMap<Pet, PetResponseModel>();
            CreateMap<PetAddRequestModel, Pet>();
            CreateMap<PetUpdateRequestModel, Pet>();
            #region Event families

            CreateMap<Event, EventResponseModel>()
                .ForMember(dest => dest.ShelterId,
                    opt => opt.MapFrom(src => src.Shelter!.ShelterId))
                .ReverseMap();
            CreateMap<EventRequestModel4Create, Event>().ReverseMap();
            CreateMap<EventRequestModel4Update, Event>()
                .ForMember(dest => dest.Image, opt =>
                {
                    opt.MapFrom(src => src.Image);
                    opt.Condition((src, dest) => src.Image != null);
                })
                .ReverseMap();
            CreateMap<Event, EventResponseModel>().ReverseMap();

            #endregion
        }
    }
}
