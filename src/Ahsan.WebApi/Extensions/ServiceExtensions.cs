using Ahsan.Data.IRepositories;
using Ahsan.Data.Repositories;
using Ahsan.Service.Interfaces;
using Ahsan.Service.Services;

namespace Ahsan.WebApi.Extensions;

public static class ServiceExtensions
{
    public static void AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
    }
}
