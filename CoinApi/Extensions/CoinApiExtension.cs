using CoinApi.Helpers;
using CoinApi.Services.AuthService;
using CoinApi.Services.CSVService;
using CoinApi.Services.EmailService;
using CoinApi.Services.FileStorageService;
using CoinApi.Services.LanguageGUIService;
using CoinApi.Services.LanguageService;
using CoinApi.Services.MasterService;
using CoinApi.Services.ModuleService;
using CoinApi.Services.OrderService;
using CoinApi.Services.QuestionService;
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
            services.AddScoped<IMasterService, MasterService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IModuleService, ModuleService>();
            services.AddTransient<IFileStorageService, FileStorageService>();
            services.AddTransient<IQuestionService, QuestionService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<ILanguageGUIService, LanguageGUIService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<ExcelTypeHelper, ExcelTypeHelper>();
            services.AddSingleton<ExcelBaseHelper, ExcelBaseHelper>();
            services.AddSingleton<ICSVService, CSVService>();

            return services;
        }
    }
}
