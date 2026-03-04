using Haceb.Demand.Services.IPort;
using Haceb.Demand.Services.RestClient;
using Haceb.Demand.Services.Services;
using Haceb.Demanda.Web.Mapper;

namespace Haceb.Demanda.Web.DI
{
    public static class IoCRegister
    {
        public static IServiceCollection AddRegistration(this IServiceCollection services, string conectionString = "")
        {
            AddRegisterServices(services);

            return services;
        }

        private static IServiceCollection AddRegisterServices(IServiceCollection services)
        {
            services.AddAutoMapper(cfg => cfg.AddProfile<MapperProfile>(), AppDomain.CurrentDomain.GetAssemblies());
            services.AddTransient<IDemandServices, DemandServices>();
            services.AddHttpClient<IApiClient, ApiClient>();
            return services;
        }
    }
}
