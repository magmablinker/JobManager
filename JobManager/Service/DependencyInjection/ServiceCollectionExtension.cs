using JobManager.Service.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace JobManager.Service.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddJobManagerServices(this IServiceCollection services)
        {
            services.AddSingleton<IPasswordHasherService, PasswordHasherService>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ILanguageService, LanguageService>(); // TODO: Can this be made singleton?
            services.AddScoped<IRequestDataService, RequestDataService>();
            services.AddScoped<IJobOfferService, JobOfferService>();
            services.AddScoped<ICategoryService, CategoryService>();

            return services;
        }
    }
}
