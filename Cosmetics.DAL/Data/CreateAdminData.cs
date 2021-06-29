using Cosmetics.DAL.Models.Configuration;
using Cosmetics.DAL.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace Cosmetics.DAL.Data
{
    public static class CreateAdminData
    {
        public async static Task CreateDataTask(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await context.Roles.AnyAsync())
                {
                    await context.CreateAsync(new IdentityRole
                    {
                        Name = "Admin"
                    });
                    await context.CreateAsync(new IdentityRole
                    {
                        Name = "User"
                    });
                }

                var userContext = services.GetRequiredService<UserManager<ApplicationUser>>();

                var admin = await userContext.FindByNameAsync("Admin");

                if (admin == null)
                {
                    ApplicationUser adminUser = new ApplicationUser
                    {
                        FirstName = "Admin",
                        LastName = "Cosmetics",
                        FullName = "Admin Cosmetics",
                        Email = $"admin@turkishart.store",
                        UserName = "Admin",
                        EmailConfirmed = true
                    };

                    await userContext.CreateAsync(adminUser, "Password!1");
                    await userContext.AddToRoleAsync(adminUser, "Admin");
                }

                List<Language> languages = new List<Language>();
                languages.Add(new Language() { Culture = "ru", LanguageCode = "ru", Name = "Русский", DisplayOrder = 0, Published = true });
                languages.Add(new Language() { Culture = "en", LanguageCode = "en", Name = "English", DisplayOrder = 1, Published = true });
                languages.Add(new Language() { Culture = "tr", LanguageCode = "tr", Name = "Türkçe", DisplayOrder = 2, Published = true });

                var _dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                foreach(var lng in languages)
                {
                    var language = await _dbContext.Languages.SingleOrDefaultAsync(s => s.Culture == lng.Culture);
                    if(language == null)
                    {
                        _dbContext.Languages.Add(lng);
                        await _dbContext.SaveChangesAsync();
                    }
                }

                var currency = await _dbContext.Currencies.SingleOrDefaultAsync(s => s.CurrencyCode == "USD");
                if(currency == null)
                {
                    Currency cur = new Currency
                    {
                        Name = "Dollar",
                        CurrencyCode = "USD",
                        Rate = 1,
                        Published = true,
                        DisplayOrder = 0
                    };
                    _dbContext.Currencies.Add(cur);
                    await _dbContext.SaveChangesAsync();
                }

                //var setting = await _dbContext.SettingPortals.FirstOrDefaultAsync();
                //if (setting == null)
                //{
                //    SettingPortal settingPortal = new SettingPortal
                //    {
                //        PhonePortal = "+*********",
                //        EmailPortal = "test@mail.com",
                //        AddressPortal = "Street"
                //    };

                //    _dbContext.SettingPortals.Add(settingPortal);
                //    await _dbContext.SaveChangesAsync();
                //}
            }
        }
    }
}
