using DAL.Data;
using DAL.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DAL.Helpers
{
    /// <summary>
    /// using this extension method to add DAL services to the IServiceCollection.
    /// its better for accessibility and organization
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDAL(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationContext>(option=>
            option.UseSqlServer(connectionString));
            services.AddScoped<IManageImagesService, ManageImagesService>();
            return services;
        }
    }
}
