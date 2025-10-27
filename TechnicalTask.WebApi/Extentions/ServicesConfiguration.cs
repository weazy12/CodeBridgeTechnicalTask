using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TechnicalTask.BLL;
using TechnicalTask.BLL.Behavior;
using TechnicalTask.BLL.Interfaces.Logging;
using TechnicalTask.BLL.Services.Logging;
using TechnicalTask.DAL.Data;
using TechnicalTask.DAL.Repositories.Interfaces.Base;
using TechnicalTask.DAL.Repositories.Realizations.Base;

namespace TechnicalTask.WebApi.Extentions
{
    public static class ServicesConfiguration
    {
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<TechnicalTaskDbContext>(options => { options.UseSqlServer(connectionString); });
        }

        public static void AddCustomServices(this IServiceCollection services)
        {

            services.AddAutoMapper(typeof(BLLAsemblyMarker).Assembly);

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(BLLAsemblyMarker).Assembly);
                cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
            });

            services.AddValidatorsFromAssemblyContaining<BLLAsemblyMarker>();

            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            services.AddScoped<ILoggerService, LoggerService>();

        }
    }
}
