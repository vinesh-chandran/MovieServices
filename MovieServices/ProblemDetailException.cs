using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace MovieServices
{
    //An extension to resgister general errors
    public static class ProblemDetailException
    {
        public static IServiceCollection AddProblemDetailsResponse(this IServiceCollection services)
        {
            return services.AddProblemDetails(x =>
            {
                x.IncludeExceptionDetails = (_, __) => false;
                x.Map<MovieException>(ex => new ProblemDetails
                {
                    Title = ex.ExceptionMessage,
                    Status = ex.ExceptionCode
                });
            });

        }
    }
}
