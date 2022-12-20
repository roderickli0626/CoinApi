using CoinApi.Services.AuthService;
using CoinApi.Services.LanguageGUIService;
using CoinApi.Services.LanguageService;
using CoinApi.Services.SubstanceForGroupService;
using CoinApi.Services.SubstanceGroupService;
using CoinApi.Services.SubstanceGroupTextService;
using CoinApi.Services.SubstanceService;
using CoinApi.Services.SubstanceTextService;
using CoinApi.Services.UserService;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace CoinApi.Extensions
{
    public static class CoinApiExtension
    {
        public static IServiceCollection AddServiceConfig(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<ILanguageService, LanguageService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<ISubstanceGroupService, SubstanceGroupService>();
            services.AddTransient<ISubstanceForGroupService, SubstanceForGroupService>();
            services.AddTransient<ISubstanceGroupTextService, SubstanceGroupTextService>();
            services.AddTransient<ISubstanceTextService, SubstanceTextService>();
            services.AddTransient<ISubstanceService, SubstanceService>();
            services.AddTransient<ILanguageGUIService, LanguageGUIService>();

            return services;
        }
    }
}
