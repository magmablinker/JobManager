using JobManager.Repository.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace JobManager.Repository.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddJobManagerRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IJobOfferRepository, JobOfferRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IJobOfferCategoryRepository, JobOfferCategoryRepository>();

            return services;
        }
    }
}
