using Homework_EfCore.Controllers;
using Homework_EfCore.Interfaces;
using Homework_EfCore.Services;
using Microsoft.EntityFrameworkCore;

namespace Homework_EfCore.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEfLibraryManager(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<MyDBContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            services.AddScoped<ILibraryService, LibraryService>();
            return services;
        }
    }
}
