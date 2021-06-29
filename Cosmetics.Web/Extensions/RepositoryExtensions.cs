using Ceramics.BLL.Services.EmailSender;
using Cosmetics.BLL.Services.ApplicationUser;
using Cosmetics.BLL.Services.Cart;
using Cosmetics.BLL.Services.Catalog;
using Cosmetics.BLL.Services.Country;
using Cosmetics.BLL.Services.Currency;
using Cosmetics.BLL.Services.Language;
using Cosmetics.BLL.Services.Manufacture;
using Cosmetics.BLL.Services.Product;
using Cosmetics.BLL.Services.SettingPortal;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cosmetics.Web.Extensions
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<ILanguageService, LanguageService>();
            services.AddTransient<IManufactureService, ManufactureService>();
            services.AddTransient<ICurrencyService, CurrencyService>();
            services.AddTransient<ICartService, CartService>();
            services.AddTransient<ISettingPortalService, SettingPortalService>();
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<IApplicationUserService, ApplicationUserService>();
            services.AddTransient<IEmailSender, EmailService>();
            //services.AddScoped<Microsoft.AspNetCore.Identity.RoleManager<Microsoft.AspNetCore.Identity.IdentityRole>>();
            //services.AddIdentity();

            return services;
        }
    }
}
