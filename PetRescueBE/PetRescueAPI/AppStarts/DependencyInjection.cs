using BusinessLayer.Service.Implement;
using BusinessLayer.Service.Interface;
using BusinessLayer.Ultility;
using DataAccessLayer.Context;
using DataAccessLayer.Repository.Implement;
using DataAccessLayer.Repository.Interface;
using DataAccessLayer.UnitOfWork.Implement;
using DataAccessLayer.UnitOfWork.Interface;
using Microsoft.EntityFrameworkCore;

namespace PetRescueAPI.AppStarts
{
    public static class DependencyInjection
    {
        public static void InstallService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRouting(options =>
            {
                options.LowercaseUrls = true; ;
                options.LowercaseQueryStrings = true;
            });

            // Database Context
            services.AddDbContext<PetRescueDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            // Add db factory, unitofwork, generic repo
            services.AddScoped<Func<PetRescueDbContext>>((provider) => () => provider.GetService<PetRescueDbContext>());
            services.AddScoped<DbFactory>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // AutoMapper
            services.AddAutoMapper(typeof(MappingProfileExtension));

            // Other Service
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IShelterService, ShelterService>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IDonationService, DonationService>();
            services.AddScoped<IAdoptionApplicationService, AdoptionApplicationService>();

        }
    }
}
