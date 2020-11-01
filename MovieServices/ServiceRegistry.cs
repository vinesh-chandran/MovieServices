using Microsoft.Extensions.DependencyInjection;
using MovieServices.BusinessService;
using MovieServices.BusinessService.Contracts;

namespace MovieServices
{
    public static class ServiceRegistry
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(IMovieService), typeof(MovieBusinessService));
            return services;
        }
    }
}
